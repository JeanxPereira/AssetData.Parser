using System.Reflection;
using System.Text;

namespace ReCap.Parser;

/// <summary>
/// Sequential blob data reader. Cursor advances automatically as data is read.
/// Optimized with Span for performance.
/// </summary>
public sealed class BlobReader
{
    private readonly byte[] _data;
    private readonly int _blobStart;
    private int _cursor;
    
    public BlobReader(byte[] data, int headerSize)
    {
        _data = data;
        _blobStart = headerSize;
        _cursor = headerSize;
    }
    
    public int Position => _cursor;
    public ReadOnlySpan<byte> Data => _data;
    
    public string ReadString()
    {
        int start = _cursor;
        while (_cursor < _data.Length && _data[_cursor] != 0)
            _cursor++;
        
        var result = Encoding.UTF8.GetString(_data.AsSpan(start, _cursor - start));
        if (_cursor < _data.Length) _cursor++;
        return result;
    }
    
    public int ReserveArray(int elementSize, int count)
    {
        int start = _cursor;
        _cursor += elementSize * count;
        return start;
    }
    
    public int ReserveStruct(int structSize)
    {
        int start = _cursor;
        _cursor += structSize;
        return start;
    }
    
    public bool HasData(int requiredBytes = 1) => _cursor + requiredBytes <= _data.Length;
}

/// <summary>
/// Darkspore binary asset parser. Returns AssetNode tree directly for optimal performance.
/// No XML intermediate step - direct object model for MVVM binding.
/// </summary>
public sealed class AssetParser
{
    private readonly Dictionary<string, StructDefinition> _globalStructs = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, EnumDefinition> _globalEnums = new(StringComparer.OrdinalIgnoreCase);

    private byte[] _data = null!;
    private BlobReader _blob = null!;
    
    /// <summary>Enable console logging for debugging.</summary>
    public bool EnableLogging { get; set; }
    
    /// <summary>Show binary offsets in logs.</summary>
    public bool ShowOffsets { get; set; }
    
    /// <summary>Get all registered struct definitions (for schema inspection).</summary>
    public IReadOnlyDictionary<string, StructDefinition> Structs => _globalStructs;
    
    /// <summary>Get all registered enum definitions (for schema inspection).</summary>
    public IReadOnlyDictionary<string, EnumDefinition> Enums => _globalEnums;
    
    /// <summary>Get file type by extension. Infers from struct with matching name.</summary>
    public FileTypeInfo? GetFileType(string extension)
    {
        var ext = extension.TrimStart('.').ToLowerInvariant();
        var structDef = _globalStructs.GetValueOrDefault(ext);
        if (structDef == null) return null;
        return new FileTypeInfo(ext, structDef.Name, structDef.Size);
    }
    
    public IEnumerable<string> SupportedTypes => _globalStructs.Keys;
    
    public AssetParser() 
    {
        InitializeCatalogs();
    }
    
    private void InitializeCatalogs()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var catalogTypes = assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(AssetCatalog)) && !t.IsAbstract);

        foreach (var type in catalogTypes)
        {
            var catalog = (AssetCatalog)Activator.CreateInstance(type)!;
            MergeCatalog(catalog);
        }
    }

    private void MergeCatalog(AssetCatalog catalog)
    {
        var flags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        
        var structs = typeof(AssetCatalog)
            .GetField("_structs", flags)
            ?.GetValue(catalog) as Dictionary<string, StructDefinition>;
        
        var enums = typeof(AssetCatalog)
            .GetField("_enums", flags)
            ?.GetValue(catalog) as Dictionary<string, EnumDefinition>;

        if (structs != null)
        {
            foreach (var kvp in structs)
            {
                if (!_globalStructs.ContainsKey(kvp.Key))
                    _globalStructs[kvp.Key] = kvp.Value;
            }
        }
        
        if (enums != null)
        {
            foreach (var kvp in enums)
            {
                if (!_globalEnums.ContainsKey(kvp.Key))
                    _globalEnums[kvp.Key] = kvp.Value;
            }
        }
    }

    /// <summary>
    /// Parse a binary asset file and return the root AssetNode.
    /// This is the main API - returns objects directly, no XML!
    /// </summary>
    public AssetNode ParseFile(string filePath)
    {
        var extension = Path.GetExtension(filePath);
        var fileType = GetFileType(extension)
            ?? throw new ArgumentException($"Unknown file type: {extension}");
        
        var data = File.ReadAllBytes(filePath);
        return Parse(data, fileType.RootStruct, fileType.HeaderSize);
    }
    
    /// <summary>
    /// Parse binary data and return the root AssetNode.
    /// </summary>
    public AssetNode Parse(byte[] data, string rootStructName, int headerSize)
    {
        if (!_globalStructs.TryGetValue(rootStructName, out var structDef))
            throw new ArgumentException($"Unknown struct: {rootStructName}");
        
        _data = data;
        _blob = new BlobReader(data, headerSize);
        
        var root = new StructNode
        {
            Name = rootStructName.ToLowerInvariant(),
            TypeName = structDef.Name,
            BinaryOffset = 0
        };
        
        ParseStruct(root, structDef, baseOffset: 0);
        return root;
    }
    
    /// <summary>
    /// Parse binary data from a stream.
    /// </summary>
    public AssetNode Parse(Stream stream, string rootStructName, int headerSize)
    {
        using var ms = new MemoryStream();
        stream.CopyTo(ms);
        return Parse(ms.ToArray(), rootStructName, headerSize);
    }
    
    private void ParseStruct(AssetNode parent, StructDefinition structDef, int baseOffset)
    {
        foreach (var field in structDef.Fields)
        {
            var node = ParseField(field, baseOffset + field.Offset, structDef.Name);
            if (node != null)
                parent.AddChild(node);
        }
    }
    
    private AssetNode? ParseField(FieldDefinition field, int offset, string parentStructName)
    {
        try
        {
            return field.Type switch
            {
                // Primitives
                DataType.Bool => new BooleanNode
                {
                    Name = field.Name,
                    Value = ReadUInt32(offset) != 0,
                    BinaryOffset = offset
                },
                
                DataType.Int => new NumberNode
                {
                    Name = field.Name,
                    Value = ReadInt32(offset),
                    OriginalType = NumericType.Int32,
                    BinaryOffset = offset
                },
                
                DataType.UInt => new NumberNode
                {
                    Name = field.Name,
                    Value = ReadUInt32(offset),
                    OriginalType = NumericType.UInt32,
                    BinaryOffset = offset
                },
                
                DataType.Float => new NumberNode
                {
                    Name = field.Name,
                    Value = ReadFloat(offset),
                    OriginalType = NumericType.Float,
                    Format = NumberFormat.Float,
                    BinaryOffset = offset
                },
                
                DataType.Int64 => new NumberNode
                {
                    Name = field.Name,
                    Value = ReadInt64(offset),
                    OriginalType = NumericType.Int64,
                    BinaryOffset = offset
                },
                
                DataType.UInt64 => new NumberNode
                {
                    Name = field.Name,
                    Value = ReadUInt64(offset),
                    OriginalType = NumericType.UInt64,
                    BinaryOffset = offset
                },
                
                DataType.Enum => ParseEnumField(field, offset),
                
                // Dynamic types
                DataType.Key => ParseKeyField(field, offset),
                DataType.Char => ParseCharField(field, offset),
                DataType.CharPtr => ParseCharPtrField(field, offset),
                DataType.Asset => ParseAssetField(field, offset),
                
                // Containers
                DataType.Array => ParseArrayField(field, offset),
                DataType.Nullable => ParseNullableField(field, offset),
                DataType.Struct => ParseInlineStructField(field, offset),
                
                _ => null
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"Error parsing '{field.Name}' in '{parentStructName}' at 0x{offset:X}: {ex.Message}", ex);
        }
    }
    
    private EnumNode ParseEnumField(FieldDefinition field, int offset)
    {
        var rawValue = ReadUInt32(offset);
        string? resolvedName = null;
        
        if (field.EnumType != null && _globalEnums.TryGetValue(field.EnumType, out var enumDef))
            resolvedName = enumDef.GetName(rawValue);
        
        return new EnumNode
        {
            Name = field.Name,
            EnumType = field.EnumType ?? "",
            RawValue = rawValue,
            ResolvedName = resolvedName ?? "",
            BinaryOffset = offset
        };
    }
    
    private AssetNode? ParseKeyField(FieldDefinition field, int offset)
    {
        uint indicator = ReadUInt32(offset);
        if (indicator == 0) return null;
        
        return new StringNode
        {
            Name = field.Name,
            Value = _blob.ReadString(),
            NodeKind = AssetNodeKind.Asset,
            BinaryOffset = offset
        };
    }
    
    private AssetNode? ParseCharField(FieldDefinition field, int offset)
    {
        if (field.BufferSize > 0)
        {
            return new StringNode
            {
                Name = field.Name,
                Value = ReadInlineString(offset, field.BufferSize),
                NodeKind = AssetNodeKind.String,
                BinaryOffset = offset
            };
        }
        
        uint indicator = ReadUInt32(offset);
        if (indicator == 0) return null;
        
        return new StringNode
        {
            Name = field.Name,
            Value = _blob.ReadString(),
            NodeKind = AssetNodeKind.String,
            BinaryOffset = offset
        };
    }
    
    private AssetNode? ParseCharPtrField(FieldDefinition field, int offset)
    {
        uint indicator = ReadUInt32(offset);
        if (indicator == 0) return null;
        
        return new StringNode
        {
            Name = field.Name,
            Value = _blob.ReadString(),
            NodeKind = AssetNodeKind.String,
            BinaryOffset = offset
        };
    }
    
    private AssetNode? ParseAssetField(FieldDefinition field, int offset)
    {
        uint indicator = ReadUInt32(offset);
        if (indicator == 0) return null;
        
        return new StringNode
        {
            Name = field.Name,
            Value = _blob.ReadString(),
            NodeKind = AssetNodeKind.Asset,
            BinaryOffset = offset
        };
    }
    
    private ArrayNode ParseArrayField(FieldDefinition field, int offset)
    {
        uint hasValue = ReadUInt32(offset);
        int count = ReadInt32(offset + field.CountOffset);
        
        var arrayNode = new ArrayNode
        {
            Name = field.Name,
            ElementType = field.ElementType ?? "unknown",
            BinaryOffset = offset
        };
        
        if (hasValue == 0 || count <= 0)
            return arrayNode;
        
        if (count > 1_000_000)
            throw new InvalidOperationException($"Array '{field.Name}' has invalid count: {count}");

        // Check if element type is a struct
        if (_globalStructs.TryGetValue(field.ElementType!, out var elementStructDef))
        {
            int arrayStart = _blob.ReserveArray(elementStructDef.Size, count);
            
            for (int i = 0; i < count; i++)
            {
                int elemOffset = arrayStart + (i * elementStructDef.Size);
                var entry = new StructNode
                {
                    Name = $"[{i}]",
                    TypeName = field.ElementType!,
                    BinaryOffset = elemOffset
                };
                ParseStruct(entry, elementStructDef, elemOffset);
                arrayNode.AddChild(entry);
            }
            return arrayNode;
        }
        
        // Must be a primitive or dynamic type
        if (!Enum.TryParse<DataType>(field.ElementType, true, out var elemType))
            throw new InvalidOperationException($"Unknown element type: {field.ElementType}");
        
        // Handle dynamic types (strings) - each has a 4-byte indicator in the header array
        if (elemType.IsDynamic())
        {
            // Reserve space for indicator array (4 bytes per element)
            int indicatorStart = _blob.ReserveArray(4, count);
            
            for (int i = 0; i < count; i++)
            {
                int indicatorOffset = indicatorStart + (i * 4);
                uint indicator = ReadUInt32(indicatorOffset);
                
                AssetNode entry;
                if (indicator != 0)
                {
                    entry = new StringNode
                    {
                        Name = $"[{i}]",
                        Value = _blob.ReadString(),
                        NodeKind = elemType == DataType.Asset ? AssetNodeKind.Asset : AssetNodeKind.String,
                        BinaryOffset = indicatorOffset
                    };
                }
                else
                {
                    entry = new StringNode
                    {
                        Name = $"[{i}]",
                        Value = "",
                        NodeKind = AssetNodeKind.String,
                        BinaryOffset = indicatorOffset
                    };
                }
                arrayNode.AddChild(entry);
            }
            return arrayNode;
        }
        
        // Handle primitive types
        int elementSize = GetPrimitiveSize(elemType);
        int arrayStart2 = _blob.ReserveArray(elementSize, count);
        
        for (int i = 0; i < count; i++)
        {
            int elemOffset = arrayStart2 + (i * elementSize);
            var entry = CreatePrimitiveNode($"[{i}]", elemType, elemOffset, field.EnumType);
            arrayNode.AddChild(entry);
        }
        
        return arrayNode;
    }
    
    private AssetNode? ParseNullableField(FieldDefinition field, int offset)
    {
        uint hasValue = ReadUInt32(offset);
        if (hasValue == 0)
        {
            return new NullNode
            {
                Name = field.Name,
                BinaryOffset = offset
            };
        }
        
        if (!_globalStructs.TryGetValue(field.ElementType!, out var structDef))
            throw new InvalidOperationException($"Unknown struct for nullable: {field.ElementType}");
        
        int structStart = _blob.ReserveStruct(structDef.Size);
        
        var node = new StructNode
        {
            Name = field.Name,
            TypeName = field.ElementType!,
            BinaryOffset = structStart
        };
        
        ParseStruct(node, structDef, structStart);
        return node;
    }
    
    private StructNode ParseInlineStructField(FieldDefinition field, int offset)
    {
        if (!_globalStructs.TryGetValue(field.ElementType!, out var structDef))
            throw new InvalidOperationException($"Unknown struct for inline: {field.ElementType}");
        
        var node = new StructNode
        {
            Name = field.Name,
            TypeName = field.ElementType!,
            BinaryOffset = offset
        };
        
        ParseStruct(node, structDef, offset);
        return node;
    }
    
    private AssetNode CreatePrimitiveNode(string name, DataType type, int offset, string? enumType) => type switch
    {
        DataType.Bool => new BooleanNode
        {
            Name = name,
            Value = ReadUInt32(offset) != 0,
            BinaryOffset = offset
        },
        DataType.Int => new NumberNode
        {
            Name = name,
            Value = ReadInt32(offset),
            OriginalType = NumericType.Int32,
            BinaryOffset = offset
        },
        DataType.UInt => new NumberNode
        {
            Name = name,
            Value = ReadUInt32(offset),
            OriginalType = NumericType.UInt32,
            BinaryOffset = offset
        },
        DataType.Float => new NumberNode
        {
            Name = name,
            Value = ReadFloat(offset),
            OriginalType = NumericType.Float,
            Format = NumberFormat.Float,
            BinaryOffset = offset
        },
        DataType.Enum => new EnumNode
        {
            Name = name,
            EnumType = enumType ?? "",
            RawValue = ReadUInt32(offset),
            BinaryOffset = offset
        },
        DataType.Int64 => new NumberNode
        {
            Name = name,
            Value = ReadInt64(offset),
            OriginalType = NumericType.Int64,
            BinaryOffset = offset
        },
        DataType.UInt64 => new NumberNode
        {
            Name = name,
            Value = ReadUInt64(offset),
            OriginalType = NumericType.UInt64,
            BinaryOffset = offset
        },
        _ => throw new InvalidOperationException($"Not a primitive type: {type}")
    };
    
    // Binary readers using Span for performance
    private int ReadInt32(int offset) => BitConverter.ToInt32(_data.AsSpan(offset, 4));
    private uint ReadUInt32(int offset) => BitConverter.ToUInt32(_data.AsSpan(offset, 4));
    private long ReadInt64(int offset) => BitConverter.ToInt64(_data.AsSpan(offset, 8));
    private ulong ReadUInt64(int offset) => BitConverter.ToUInt64(_data.AsSpan(offset, 8));
    private float ReadFloat(int offset) => BitConverter.ToSingle(_data.AsSpan(offset, 4));
    
    private string ReadInlineString(int offset, int bufferSize)
    {
        int end = offset;
        int maxEnd = Math.Min(offset + bufferSize, _data.Length);
        while (end < maxEnd && _data[end] != 0)
            end++;
        return Encoding.UTF8.GetString(_data.AsSpan(offset, end - offset));
    }
    
    private static int GetPrimitiveSize(DataType type) => type switch
    {
        DataType.Bool => 4,
        DataType.Int => 4,
        DataType.UInt => 4,
        DataType.Float => 4,
        DataType.Enum => 4,
        DataType.Int64 => 8,
        DataType.UInt64 => 8,
        _ => throw new InvalidOperationException($"Not a primitive type: {type}")
    };
}
