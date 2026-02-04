// =============================================================================
// DEPRECATED - No longer needed with direct Core API
// =============================================================================
// 
// The XML bridge has been eliminated. The Editor now calls the Core library
// directly via AssetService, which returns AssetNode objects.
//
// Old flow (slow):  Binary -> Core CLI -> XML File -> Editor reads XML -> Nodes
// New flow (fast):  Binary -> Core API -> AssetNode (direct binding)
//
// This file can be safely deleted.
// =============================================================================

namespace AssetData.Parser.Editor.Services
{
    [Obsolete("Use AssetService from Core library directly. XML bridge eliminated.")]
    public static class CoreXmlBridge
    {
        [Obsolete("Use MainViewModel.LoadFileAsync() which calls AssetService directly")]
        public static Task<System.Xml.Linq.XDocument?> ParseToXmlAsync(string inputPath)
        {
            throw new NotSupportedException(
                "XML bridge has been removed. Use AssetService.LoadFile() instead for direct parsing.");
        }
    }
}
