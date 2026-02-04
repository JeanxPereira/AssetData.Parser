using AssetData.Parser;

namespace AssetData.Parser.Editor.Services;

/// <summary>
/// Extracts asset key suggestions from an AssetNode tree.
/// Used for autocomplete in asset path fields.
/// </summary>
public static class KeyAssetSuggestionsService
{
    /// <summary>
    /// Extract all unique asset paths from the node tree.
    /// </summary>
    public static IEnumerable<string> Extract(IEnumerable<AssetNode> roots)
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var root in roots) 
            Walk(root, set);
        return set.OrderBy(x => x);
    }
    
    /// <summary>
    /// Extract asset paths from a single node tree.
    /// </summary>
    public static IEnumerable<string> Extract(AssetNode root)
    {
        var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        Walk(root, set);
        return set.OrderBy(x => x);
    }

    private static void Walk(AssetNode node, HashSet<string> set)
    {
        // Extract asset paths from StringNodes marked as Asset kind
        if (node is StringNode sn && sn.NodeKind == AssetNodeKind.Asset)
        {
            if (!string.IsNullOrWhiteSpace(sn.Value))
                set.Add(sn.Value);
        }
        
        // Recurse into children
        foreach (var child in node.Children) 
            Walk(child, set);
    }
}
