using System;
using System.Collections.Generic;
using System.Linq;

namespace ReCap.Parser;

/// <summary>
/// Darkspore asset data types. Hash values are FNV-1a (case-insensitive).
/// </summary>
public enum DataType : uint
{
    // Primitives - direct value in header
    Bool    = 0x68FE5F59,
    Int     = 0x1F886EB0,
    UInt    = 0x54CC76D5,
    Float   = 0x4EDCD7A9,
    Enum    = 0x096339A2,
    Int64   = 0x071568E6,
    UInt64  = 0xEE28814F,
    
    // Dynamic - indicator in header, data in blob if != 0
    Key     = 0x46842E82,
    Char    = 0xF6C8069D,   // inline char[] buffer (size in BufferSize)
    CharPtr = 0x19E2690D,   // null-terminated string in blob
    Asset   = 0x9C617503,
    
    // Containers
    Array    = 0x555CCDF4,  // [hasValue:4][count:4] in header
    Nullable = 0x71AB5182,  // [hasValue:4] in header
    
    // Inline struct
    Struct   = 0x00000008
}

public static class DataTypeExtensions
{
    public static bool IsPrimitive(this DataType type) => type switch
    {
        DataType.Bool or DataType.Int or DataType.UInt or 
        DataType.Float or DataType.Enum or DataType.Int64 or 
        DataType.UInt64 => true,
        _ => false
    };
    
    public static bool IsDynamic(this DataType type) => type switch
    {
        DataType.Key or DataType.Char or DataType.CharPtr or 
        DataType.Asset => true,
        _ => false
    };
}

/// <summary>
/// Field definition within a struct.
/// </summary>
public sealed record FieldDefinition(
    string Name,
    DataType Type,
    int Offset,
    string? ElementType = null,
    int CountOffset = 4,
    int BufferSize = 0,
    string? EnumType = null        // For Enum fields: name of the enum definition
)
{
    public bool IsStructArray => Type == DataType.Array && ElementType != null && 
                                  !Enum.TryParse<DataType>(ElementType, true, out _);
}

/// <summary>
/// Struct definition with fields.
/// </summary>
public sealed class StructDefinition
{
    public string Name { get; }
    public int Size { get; }
    public IReadOnlyList<FieldDefinition> Fields { get; }
    
    public StructDefinition(string name, int size, params FieldDefinition[] fields)
    {
        Name = name;
        Size = size;
        Fields = fields.OrderBy(f => f.Offset).ToList();
    }
}

/// <summary>
/// File type registration info (auto-generated from struct name and size).
/// </summary>
public sealed record FileTypeInfo(string Extension, string RootStruct, int HeaderSize);

/// <summary>
/// Enum definition with value-to-name mappings.
/// </summary>
public sealed class EnumDefinition
{
    public string Name { get; }
    private readonly Dictionary<uint, string> _values = new();
    private readonly Dictionary<string, uint> _names = new(StringComparer.OrdinalIgnoreCase);
    
    public EnumDefinition(string name) => Name = name;
    
    public void Add(string name, uint value)
    {
        _values[value] = name;
        _names[name] = value;
    }
    
    public string? GetName(uint value) => _values.GetValueOrDefault(value);
    public uint? GetValue(string name) => _names.TryGetValue(name, out var v) ? v : null;
    
    /// <summary>
    /// Format value as "Name" if known, or "0xHEX" if not.
    /// </summary>
    public string Format(uint value)
    {
        if (_values.TryGetValue(value, out var name))
            return $"{name}, 0x{value:X8}";
        return $"0x{value:X8}";
    }
}

/// <summary>
/// Base class for asset type catalogs. Override Build() to define structs and enums.
/// </summary>
public abstract class AssetCatalog
{
    private readonly Dictionary<string, StructDefinition> _structs = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, EnumDefinition> _enums = new(StringComparer.OrdinalIgnoreCase);
    
    protected AssetCatalog() => Build();
    
    protected abstract void Build();
    
    // Struct registration
    protected void Struct(string name, int size, params FieldDefinition[] fields)
        => _structs[name] = new StructDefinition(name, size, fields);
    
    // Enum registration
    protected EnumBuilder Enum(string name)
    {
        var def = new EnumDefinition(name);
        _enums[name] = def;
        return new EnumBuilder(def);
    }
    
    // Field definition helpers
    protected static FieldDefinition Field(string name, DataType type, int offset)
        => new(name, type, offset);
    
    /// <summary>
    /// Define an enum field with type mapping for named values.
    /// </summary>
    protected static FieldDefinition EnumField(string name, string enumType, int offset)
        => new(name, DataType.Enum, offset, EnumType: enumType);
    
    protected static FieldDefinition CharBuffer(string name, int offset, int bufferSize)
        => new(name, DataType.Char, offset, BufferSize: bufferSize);
    
    protected static FieldDefinition Array(string name, DataType elementType, int offset, int countOffset = 4)
        => new(name, DataType.Array, offset, elementType.ToString(), countOffset);
    
    protected static FieldDefinition ArrayStruct(string name, string structType, int offset, int countOffset = 4)
        => new(name, DataType.Array, offset, structType, countOffset);
    
    protected static FieldDefinition IStruct(string name, string structType, int offset)
        => new(name, DataType.Struct, offset, structType);
    
    protected static FieldDefinition NStruct(string name, string structType, int offset)
        => new(name, DataType.Nullable, offset, structType);
    
    // Getters
    public StructDefinition? GetStruct(string name) => _structs.GetValueOrDefault(name);
    public EnumDefinition? GetEnum(string name) => _enums.GetValueOrDefault(name);
    
    public FileTypeInfo? GetFileType(string extension)
    {
        var ext = extension.TrimStart('.').ToLowerInvariant();
        var structDef = _structs.GetValueOrDefault(ext);
        if (structDef == null) return null;
        return new FileTypeInfo(ext, structDef.Name, structDef.Size);
    }
    
    public IEnumerable<string> StructNames => _structs.Keys;
    public IEnumerable<string> EnumNames => _enums.Keys;
}

/// <summary>
/// Fluent builder for enum definitions.
/// </summary>
public sealed class EnumBuilder
{
    private readonly EnumDefinition _def;
    
    internal EnumBuilder(EnumDefinition def) => _def = def;
    
    public EnumBuilder Value(string name, uint value)
    {
        _def.Add(name, value);
        return this;
    }
    
    /// <summary>
    /// Add value using FNV-1a hash of name (case-insensitive).
    /// </summary>
    public EnumBuilder Hash(string name)
    {
        _def.Add(name, FnvHash(name));
        return this;
    }
    
    private static uint FnvHash(string s)
    {
        uint hash = 0x811C9DC5;
        foreach (char c in s.ToLowerInvariant())
        {
            hash *= 0x1000193;
            hash ^= (byte)c;
        }
        return hash;
    }
}
