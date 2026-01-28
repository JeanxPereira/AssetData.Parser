namespace ReCap.Parser.Catalog;

public sealed class cSPBoundingBoxCatalog : AssetCatalog
{
    protected override void Build()
    {
        Struct("cSPBoundingBox", 24,
            Vector3("min", 0),
            Vector3("max", 12)
        );
    }
}
