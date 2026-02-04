// =============================================================================
// DEPRECATED - No longer needed with direct Core API
// =============================================================================
// 
// Serialization to XML is now handled by AssetSerializer in the Core library.
// Use AssetSerializer.ToXml() or AssetService.ExportXml() instead.
//
// This file can be safely deleted.
// =============================================================================

namespace AssetData.Parser.Editor.Services
{
    [Obsolete("Use AssetData.Parser.AssetSerializer.ToXml() from Core library")]
    public static class NodesToXml
    {
        [Obsolete("Use AssetSerializer.ToXml() from Core library")]
        public static System.Xml.Linq.XDocument ToXDocument(AssetData.Parser.AssetNode root)
        {
            // Delegate to new Core implementation
            return AssetData.Parser.AssetSerializer.ToXml(root);
        }
    }
}
