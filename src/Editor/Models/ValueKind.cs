// =============================================================================
// DEPRECATED - Moved to Core library as AssetNodeKind
// =============================================================================
// 
// Use AssetData.Parser.AssetNodeKind from the Core library instead.
// This file can be safely deleted.
// =============================================================================

namespace AssetData.Parser.Editor.Models
{
    [Obsolete("Use AssetData.Parser.AssetNodeKind from Core library")]
    public enum ValueKind
    {
        String = AssetData.Parser.AssetNodeKind.String,
        Number = AssetData.Parser.AssetNodeKind.Number,
        Bool = AssetData.Parser.AssetNodeKind.Bool,
        Struct = AssetData.Parser.AssetNodeKind.Struct,
        Array = AssetData.Parser.AssetNodeKind.Array
    }
}
