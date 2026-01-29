namespace ReCap.Parser.Catalog;

/// <summary>
/// Asset catalog root structure.
/// Contains an array of all compiled assets in the package.
/// File pattern: catalog_%d.bin
/// </summary>
public sealed class Catalog : AssetCatalog
{
    protected override void Build()
    {
        Struct("Catalog", 8,
            ArrayStruct("entries", "CatalogEntry", 0, countOffset: 4)
        );
    }
}
