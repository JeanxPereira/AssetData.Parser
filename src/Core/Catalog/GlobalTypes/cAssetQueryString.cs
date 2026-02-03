namespace ReCap.Parser.Catalog;

public sealed class cAssetQueryString : AssetCatalog
{
    protected override void Build()
    {
        Struct("cAssetQueryString", 0x4,
            Field("query", DataType.CharPtr, 0x0)
        );
    }
}
