using System.Text.RegularExpressions;
using System.Xml;
using ReCap.Parser;
using ReCap.Parser.CLI.Commands;

namespace ReCap.Parser.CLI;

/// <summary>
/// CLI for ReCap.Parser with DBPF support.
/// </summary>
public static partial class Program
{
    // Pattern for catalog files: catalog_N.bin
    [GeneratedRegex(@"^catalog_\d+$", RegexOptions.IgnoreCase)]
    private static partial Regex CatalogPattern();
    
    public static int Main(string[] args)
    {
        if (args.Length == 0)
        {
            PrintUsage();
            return 1;
        }

        if (args.Length > 0 && args[0] == "wiki-gen")
        {
            string wikiOutputDir = args.Length > 1 ? args[1] : "./wiki-output";
            WikiCatalog.Run(wikiOutputDir);
            
            return 0;
        }

        // Parse arguments
        string? inputFile = null;
        string? dbpfPackage = null;
        string? assetName = null;
        string? registryDir = null;
        string? outputDir = null;
        bool outputXml = false;
        bool verbose = false;
        bool listAssets = false;
        string? filterType = null;
        
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-d" or "--dbpf":
                    if (i + 1 < args.Length)
                        dbpfPackage = args[++i];
                    break;
                case "-a" or "--asset":
                    if (i + 1 < args.Length)
                        assetName = args[++i];
                    break;
                case "-r" or "--registries":
                    if (i + 1 < args.Length)
                        registryDir = args[++i];
                    break;
                case "-o" or "--output":
                    if (i + 1 < args.Length)
                        outputDir = args[++i];
                    break;
                case "--xml":
                    outputXml = true;
                    break;
                case "-v" or "--verbose":
                    verbose = true;
                    break;
                case "-l" or "--list":
                    listAssets = true;
                    break;
                case "-t" or "--type":
                    if (i + 1 < args.Length)
                        filterType = args[++i];
                    break;
                case "-h" or "--help":
                    PrintUsage();
                    return 0;
                default:
                    if (!args[i].StartsWith('-'))
                        inputFile = args[i];
                    break;
            }
        }
        
        try
        {
            // Mode 1: DBPF package
            if (!string.IsNullOrEmpty(dbpfPackage))
            {
                return HandleDbpf(dbpfPackage, assetName, registryDir, outputDir, verbose, listAssets, filterType);
            }
            
            // Mode 2: Single file
            if (!string.IsNullOrEmpty(inputFile))
            {
                return HandleSingleFile(inputFile, outputDir, outputXml, verbose);
            }
            
            Console.Error.WriteLine("Error: No input specified.");
            PrintUsage();
            return 1;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            if (verbose)
                Console.Error.WriteLine(ex.StackTrace);
            return 1;
        }
    }
    
    /// <summary>
    /// Determine asset type from name, handling special patterns like catalog_N.bin
    /// </summary>
    private static (string baseName, string? typeName) ParseAssetName(string assetName)
    {
        // Remove .bin extension if present
        var name = assetName.EndsWith(".bin", StringComparison.OrdinalIgnoreCase) 
            ? assetName[..^4] 
            : assetName;
        
        // Check for catalog pattern: catalog_N -> Catalog type
        if (CatalogPattern().IsMatch(name))
        {
            return (name, "Catalog");
        }
        
        // Standard format: name.Type
        var parts = name.Split('.', 2);
        if (parts.Length > 1)
        {
            return (parts[0], parts[1]);
        }
        
        // No type extension - return null for type
        return (name, null);
    }
    
    private static int HandleDbpf(string dbpfPath, string? assetName, string? registryDir, 
        string? outputDir, bool verbose, bool listAssets, string? filterType)
    {
        if (!File.Exists(dbpfPath))
        {
            Console.Error.WriteLine($"Error: DBPF file not found: {dbpfPath}");
            return 1;
        }
        
        using var dbpf = new DbpfReader(dbpfPath);
        
        if (verbose)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($">>> Opening: {Path.GetFileName(dbpfPath)}");
            Console.ResetColor();
        }
        
        // Load registries if provided
        if (!string.IsNullOrEmpty(registryDir))
        {
            dbpf.LoadRegistries(registryDir);
            if (verbose)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"    Registries: {registryDir}");
                Console.ResetColor();
            }
        }
        
        if (verbose)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"    Entries: {dbpf.Entries.Count}");
            Console.ResetColor();
        }
        
        // Mode: List assets
        if (listAssets)
        {
            if (!string.IsNullOrEmpty(filterType))
            {
                foreach (var (name, _) in dbpf.ListAssetsByType(filterType))
                    Console.WriteLine(name);
            }
            else
            {
                foreach (var name in dbpf.ListAssets())
                    Console.WriteLine(name);
            }
            return 0;
        }
        
        // Mode: Extract and parse specific asset
        if (!string.IsNullOrEmpty(assetName))
        {
            var data = dbpf.GetAsset(assetName);
            if (data == null)
            {
                Console.Error.WriteLine($"Error: Asset not found: {assetName}");
                return 1;
            }
            
            // Parse asset name to get type
            var (baseName, typeName) = ParseAssetName(assetName);
            
            if (string.IsNullOrEmpty(typeName))
            {
                Console.Error.WriteLine($"Error: Cannot determine asset type from name: {assetName}");
                Console.Error.WriteLine($"       Try using the full name with extension (e.g., 'default.AffixTuning')");
                Console.Error.WriteLine($"       Or for catalog files: 'catalog_131.bin' or 'catalog_131'");
                return 1;
            }
            
            var service = new AssetService();
            var fileType = service.GetFileType(typeName);
            
            if (fileType == null)
            {
                Console.Error.WriteLine($"Error: Unknown asset type: {typeName}");
                return 1;
            }
            
            var root = service.Parser.Parse(data, fileType.RootStruct, fileType.HeaderSize);
            
            if (verbose)
                PrintTree(root, 0);
            
            // Output XML if requested
            if (!string.IsNullOrEmpty(outputDir))
            {
                Directory.CreateDirectory(outputDir);
                var outputPath = Path.Combine(outputDir, baseName + ".xml");
                
                var xml = AssetSerializer.ToXml(root);
                var settings = new XmlWriterSettings { Indent = true };
                using var writer = XmlWriter.Create(outputPath, settings);
                xml.WriteTo(writer);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"    {assetName} -> {Path.GetFileName(outputPath)}");
                Console.ResetColor();
            }
            
            return 0;
        }
        
        // No specific asset requested - just show info
        Console.WriteLine($"DBPF: {Path.GetFileName(dbpfPath)}");
        Console.WriteLine($"Entries: {dbpf.Entries.Count}");
        Console.WriteLine("Use -l to list assets, or -a <n> to parse a specific asset.");
        return 0;
    }
    
    private static int HandleSingleFile(string inputPath, string? outputDir, bool outputXml, bool verbose)
    {
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found: {inputPath}");
            return 1;
        }
        
        var service = new AssetService();
        var root = service.LoadFile(inputPath);
        
        if (verbose || !outputXml)
            PrintTree(root, 0);
        
        if (outputXml || !string.IsNullOrEmpty(outputDir))
        {
            var xml = AssetSerializer.ToXml(root);
            
            if (!string.IsNullOrEmpty(outputDir))
            {
                Directory.CreateDirectory(outputDir);
                var outputPath = Path.Combine(outputDir, 
                    Path.GetFileNameWithoutExtension(inputPath) + ".xml");
                
                var settings = new XmlWriterSettings { Indent = true };
                using var writer = XmlWriter.Create(outputPath, settings);
                xml.WriteTo(writer);
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Written: {outputPath}");
                Console.ResetColor();
            }
            else
            {
                // Output to stdout
                var settings = new XmlWriterSettings { Indent = true };
                using var writer = XmlWriter.Create(Console.Out, settings);
                xml.WriteTo(writer);
            }
        }
        
        return 0;
    }
    
    private static void PrintTree(AssetNode node, int indent)
    {
        var prefix = new string(' ', indent * 2);
        Console.Write(prefix);
        
        // Type (Cyan)
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(node.Kind.ToString());
        
        // Separator
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(".");
        
        // Name (White)
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(node.Name);
        
        // Value
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write("(");
        
        // Value color based on type
        switch (node)
        {
            case StringNode sn:
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(sn.Value);
                break;
            case NumberNode nn:
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(nn.DisplayValue);
                break;
            case BooleanNode bn:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(bn.DisplayValue);
                break;
            case EnumNode en:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(en.DisplayValue);
                break;
            case ArrayNode an:
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(an.Children.Count);
                break;
            case StructNode sn:
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(sn.TypeName);
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(node.DisplayValue);
                break;
        }
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(")");
        Console.ResetColor();
        Console.WriteLine();
        
        foreach (var child in node.Children)
            PrintTree(child, indent + 1);
    }
    
    private static void PrintUsage()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("ReCap.Parser");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Darkspore Binary Asset Parser\n");
        Console.ResetColor();
        
        Console.WriteLine(@"Usage:
  ReCap.Parser <file>                     Parse single asset file
  ReCap.Parser -d <package> [options]     Parse from DBPF package

Single File Options:
  <file>              Input asset file (.noun, .phase, etc.)
  --xml               Output as XML
  -o <dir>            Output directory
  -v, --verbose       Verbose output

DBPF Package Options:
  -d, --dbpf <file>   DBPF/DBBF package file
  -a, --asset <n>     Asset to extract (e.g., 'default.AffixTuning', 'catalog_131.bin')
  -r, --registries    Registry directory for name resolution
  -l, --list          List all assets in package
  -t, --type <ext>    Filter by type extension (with -l)
  -o <dir>            Output directory for XML
  -v, --verbose       Show parsed tree

Examples:");
        
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  ReCap.Parser creature.noun --xml -o output/");
        Console.WriteLine("  ReCap.Parser -d AssetData_Binary.package -l");
        Console.WriteLine("  ReCap.Parser -d AssetData_Binary.package -l -t noun");
        Console.WriteLine("  ReCap.Parser -d AssetData_Binary.package -a default.AffixTuning -r registries -v");
        Console.WriteLine("  ReCap.Parser -d AssetData_Binary.package -a catalog_131.bin -r registries -v");
        Console.WriteLine("  ReCap.Parser -d AssetData_Binary.package -a ZelemBoss.phase -r registries -o output/");
        Console.ResetColor();
    }
}
