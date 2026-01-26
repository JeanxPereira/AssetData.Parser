// =============================================================================
// DEPRECATED - No longer needed with direct Core API
// =============================================================================
// 
// XmlToNodes conversion is no longer necessary because the Core library
// now returns AssetNode objects directly via AssetService.
//
// The parsed binary data is immediately available as an observable tree
// that can be bound directly to the MVVM UI.
//
// This file can be safely deleted.
// =============================================================================

namespace ReCap.Parser.Editor.Services
{
    [Obsolete("Core now returns AssetNode directly. No XML conversion needed.")]
    public static class XmlToNodes
    {
        [Obsolete("Use AssetService.LoadFile() which returns AssetNode directly")]
        public static List<ReCap.Parser.AssetNode> FromXDocument(System.Xml.Linq.XDocument doc)
        {
            throw new NotSupportedException(
                "XML conversion has been removed. Core returns AssetNode directly.");
        }
    }
}
