using System.Globalization;
using System.Xml.Linq;

namespace ReCap.Parser;

/// <summary>
/// Serializes AssetNode trees to various formats.
/// </summary>
public static class AssetSerializer
{
    /// <summary>
    /// Serialize an AssetNode tree to XDocument (for compatibility/export).
    /// </summary>
    public static XDocument ToXml(AssetNode root)
    {
        var doc = new XDocument();
        var element = SerializeNode(root);
        doc.Add(element);
        return doc;
    }
    
    /// <summary>
    /// Serialize an AssetNode tree to XML string.
    /// </summary>
    public static string ToXmlString(AssetNode root, bool indent = true)
    {
        var doc = ToXml(root);
        return doc.ToString(indent ? SaveOptions.None : SaveOptions.DisableFormatting);
    }
    
    private static XElement SerializeNode(AssetNode node)
    {
        var element = new XElement(SanitizeName(node.Name));
        
        switch (node)
        {
            case StructNode structNode:
                foreach (var child in structNode.Children)
                    element.Add(SerializeNode(child));
                break;
                
            case ArrayNode arrayNode:
                foreach (var child in arrayNode.Children)
                {
                    var entry = new XElement("entry");
                    if (child is StructNode)
                    {
                        foreach (var grandChild in child.Children)
                            entry.Add(SerializeNode(grandChild));
                    }
                    else
                    {
                        entry.Value = child.DisplayValue;
                    }
                    element.Add(entry);
                }
                break;
                
            case StringNode stringNode:
                element.Value = stringNode.Value;
                break;
                
            case NumberNode numberNode:
                element.Value = numberNode.OriginalType switch
                {
                    NumericType.Float => numberNode.Value.ToString("G9", CultureInfo.InvariantCulture),
                    NumericType.Int64 or NumericType.UInt64 => ((long)numberNode.Value).ToString(CultureInfo.InvariantCulture),
                    _ => ((int)numberNode.Value).ToString(CultureInfo.InvariantCulture)
                };
                break;
                
            case BooleanNode boolNode:
                element.Value = boolNode.Value ? "true" : "false";
                break;
                
            case EnumNode enumNode:
                element.Value = string.IsNullOrEmpty(enumNode.ResolvedName)
                    ? $"0x{enumNode.RawValue:X8}"
                    : $"{enumNode.ResolvedName}, 0x{enumNode.RawValue:X8}";
                break;
                
            case NullNode:
                // Empty element for null
                break;
        }
        
        return element;
    }
    
    private static string SanitizeName(string name)
    {
        // Remove array index brackets for XML element names
        if (name.StartsWith('[') && name.EndsWith(']'))
            return "entry";
        return name;
    }
}

/// <summary>
/// High-level API for the Editor to interact with the Core.
/// Thread-safe, caches parser instance.
/// </summary>
public sealed class AssetService
{
    private readonly Lazy<AssetParser> _parser = new(() => new AssetParser());
    
    /// <summary>Get the parser instance (lazy-initialized).</summary>
    public AssetParser Parser => _parser.Value;
    
    /// <summary>
    /// Load an asset file and return the parsed node tree.
    /// </summary>
    public AssetNode LoadFile(string filePath)
    {
        return Parser.ParseFile(filePath);
    }
    
    /// <summary>
    /// Load an asset from a stream.
    /// </summary>
    public AssetNode Load(Stream stream, string rootStructName, int headerSize)
    {
        return Parser.Parse(stream, rootStructName, headerSize);
    }
    
    /// <summary>
    /// Load an asset from a byte array.
    /// </summary>
    public AssetNode Load(byte[] data, string rootStructName, int headerSize)
    {
        return Parser.Parse(data, rootStructName, headerSize);
    }
    
    /// <summary>
    /// Get file type info for an extension.
    /// </summary>
    public FileTypeInfo? GetFileType(string extension)
    {
        return Parser.GetFileType(extension);
    }
    
    /// <summary>
    /// Get all supported file extensions.
    /// </summary>
    public IEnumerable<string> SupportedExtensions => Parser.SupportedTypes;
    
    /// <summary>
    /// Export an asset node tree to XML.
    /// </summary>
    public void ExportXml(AssetNode root, string outputPath)
    {
        var xml = AssetSerializer.ToXmlString(root);
        File.WriteAllText(outputPath, xml);
    }
    
    /// <summary>
    /// Get schema information for a struct type.
    /// </summary>
    public StructDefinition? GetStructSchema(string typeName)
    {
        return Parser.Structs.GetValueOrDefault(typeName);
    }
    
    /// <summary>
    /// Get enum definition for resolving values.
    /// </summary>
    public EnumDefinition? GetEnumSchema(string enumName)
    {
        return Parser.Enums.GetValueOrDefault(enumName);
    }
    
    /// <summary>
    /// Flatten an asset tree to a list (for search/iteration).
    /// </summary>
    public List<AssetNode> Flatten(AssetNode root)
    {
        var result = new List<AssetNode>();
        FlattenRecursive(root, result);
        return result;
    }
    
    private void FlattenRecursive(AssetNode node, List<AssetNode> result)
    {
        result.Add(node);
        foreach (var child in node.Children)
            FlattenRecursive(child, result);
    }
}
