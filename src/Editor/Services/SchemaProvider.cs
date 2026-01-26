// =============================================================================
// DEPRECATED - Schema information now embedded in AssetNode
// =============================================================================
// 
// The new architecture embeds type information directly in AssetNode:
// - StructNode.TypeName contains the struct type name
// - EnumNode.EnumType contains the enum type name  
// - ArrayNode.ElementType contains the element type name
// - NumberNode.OriginalType contains the numeric type
//
// Schema lookup is now done via AssetService:
// - assetService.GetStructSchema(typeName)
// - assetService.GetEnumSchema(enumName)
//
// This file can be safely deleted.
// =============================================================================

using ReCap.Parser;

namespace ReCap.Parser.Editor.Services
{
    [Obsolete("Schema info is now embedded in AssetNode. Use AssetService for schema lookup.")]
    public static class SchemaProvider
    {
        /// <summary>
        /// Get struct schema from the asset service.
        /// </summary>
        public static StructDefinition? GetStructSchema(string typeName)
        {
            return ServiceLocator.Get<AssetService>().GetStructSchema(typeName);
        }
        
        /// <summary>
        /// Get enum schema from the asset service.
        /// </summary>
        public static EnumDefinition? GetEnumSchema(string enumName)
        {
            return ServiceLocator.Get<AssetService>().GetEnumSchema(enumName);
        }
    }
}
