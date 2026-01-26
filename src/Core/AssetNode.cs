using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReCap.Parser;

/// <summary>
/// Base class for all parsed asset nodes. Observable for MVVM binding.
/// </summary>
public abstract class AssetNode : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private AssetNode? _parent;
    
    /// <summary>Field/property name from the asset definition.</summary>
    public string Name
    {
        get => _name;
        set => SetField(ref _name, value);
    }
    
    /// <summary>Parent node in the hierarchy.</summary>
    public AssetNode? Parent
    {
        get => _parent;
        internal set => _parent = value;
    }
    
    /// <summary>Child nodes (for struct/array types).</summary>
    public ObservableCollection<AssetNode> Children { get; } = [];
    
    /// <summary>The data type of this node.</summary>
    public abstract AssetNodeKind Kind { get; }
    
    /// <summary>Original binary offset (for debugging/editing).</summary>
    public int BinaryOffset { get; init; }
    
    /// <summary>Display string for the value.</summary>
    public abstract string DisplayValue { get; }
    
    /// <summary>Whether this node can be edited.</summary>
    public virtual bool IsEditable => true;
    
    /// <summary>Add a child node, setting parent reference.</summary>
    public void AddChild(AssetNode child)
    {
        child._parent = this;
        Children.Add(child);
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    
    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? name = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(name);
        if (name != nameof(DisplayValue)) OnPropertyChanged(nameof(DisplayValue));
        return true;
    }
}

/// <summary>
/// Node type classification for UI rendering.
/// </summary>
public enum AssetNodeKind
{
    Struct,
    Array,
    String,
    Number,
    Boolean,
    Enum,
    Asset,  // Asset reference (key path)
    Null    // Nullable with no value
}

/// <summary>
/// Struct container node.
/// </summary>
public sealed class StructNode : AssetNode
{
    public override AssetNodeKind Kind => AssetNodeKind.Struct;
    
    /// <summary>Struct type name from the catalog.</summary>
    public string TypeName { get; init; } = string.Empty;
    
    public override string DisplayValue => $"[{TypeName}]";
    public override bool IsEditable => false;
}

/// <summary>
/// Array container node.
/// </summary>
public sealed class ArrayNode : AssetNode
{
    public override AssetNodeKind Kind => AssetNodeKind.Array;
    
    /// <summary>Element type name.</summary>
    public string ElementType { get; init; } = string.Empty;
    
    public override string DisplayValue => $"[{Children.Count} items]";
    public override bool IsEditable => false;
}

/// <summary>
/// String value node (char*, key, asset reference).
/// </summary>
public sealed class StringNode : AssetNode
{
    private string _value = string.Empty;
    private AssetNodeKind _kind = AssetNodeKind.String;
    
    /// <summary>
    /// The kind of string node (String or Asset).
    /// </summary>
    public override AssetNodeKind Kind => _kind;
    
    /// <summary>
    /// Set the kind during initialization (String or Asset).
    /// </summary>
    public AssetNodeKind NodeKind
    {
        get => _kind;
        init => _kind = value;
    }
    
    public string Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }
    
    public override string DisplayValue => Value;
}

/// <summary>
/// Numeric value node (int, uint, float, int64, uint64).
/// </summary>
public sealed class NumberNode : AssetNode
{
    private double _value;
    private NumberFormat _format = NumberFormat.Decimal;
    
    public override AssetNodeKind Kind => AssetNodeKind.Number;
    
    public double Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }
    
    /// <summary>Original numeric type for proper serialization.</summary>
    public NumericType OriginalType { get; init; } = NumericType.Int32;
    
    /// <summary>Display format preference.</summary>
    public NumberFormat Format
    {
        get => _format;
        set => SetField(ref _format, value);
    }
    
    public override string DisplayValue => Format switch
    {
        NumberFormat.Hex => $"0x{(long)Value:X}",
        NumberFormat.Float => Value.ToString("G9"),
        _ => OriginalType switch
        {
            NumericType.Float => Value.ToString("G9"),
            NumericType.Int64 or NumericType.UInt64 => ((long)Value).ToString(),
            _ => ((int)Value).ToString()
        }
    };
}

/// <summary>
/// Boolean value node.
/// </summary>
public sealed class BooleanNode : AssetNode
{
    private bool _value;
    
    public override AssetNodeKind Kind => AssetNodeKind.Boolean;
    
    public bool Value
    {
        get => _value;
        set => SetField(ref _value, value);
    }
    
    public override string DisplayValue => Value ? "true" : "false";
}

/// <summary>
/// Enum value node with resolved name.
/// </summary>
public sealed class EnumNode : AssetNode
{
    private uint _rawValue;
    private string _resolvedName = string.Empty;
    
    public override AssetNodeKind Kind => AssetNodeKind.Enum;
    
    /// <summary>Enum type name from catalog.</summary>
    public string EnumType { get; init; } = string.Empty;
    
    public uint RawValue
    {
        get => _rawValue;
        set
        {
            if (SetField(ref _rawValue, value))
                OnPropertyChanged(nameof(DisplayValue));
        }
    }
    
    public string ResolvedName
    {
        get => _resolvedName;
        set => SetField(ref _resolvedName, value);
    }
    
    public override string DisplayValue => string.IsNullOrEmpty(ResolvedName) 
        ? $"0x{RawValue:X8}" 
        : $"{ResolvedName} (0x{RawValue:X8})";
}

/// <summary>
/// Null/empty node for nullable structs without value.
/// </summary>
public sealed class NullNode : AssetNode
{
    public override AssetNodeKind Kind => AssetNodeKind.Null;
    public override string DisplayValue => "(null)";
    public override bool IsEditable => false;
}

/// <summary>
/// Original numeric type for proper serialization.
/// </summary>
public enum NumericType
{
    Int32,
    UInt32,
    Int64,
    UInt64,
    Float
}

/// <summary>
/// Display format for numbers.
/// </summary>
public enum NumberFormat
{
    Decimal,
    Hex,
    Float
}
