namespace ReCap.Parser.Catalog;

public sealed class cLongDescription : AssetCatalog
{
    protected override void Build()
    {
        Struct("cLongDescription", 0x14,
            Field("description", DataType.cLocalizedAssetString, 0x0)
        );
    }
}
