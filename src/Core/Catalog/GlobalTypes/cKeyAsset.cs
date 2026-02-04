namespace AssetData.Parser.Catalog;

public sealed class cKeyAsset : AssetCatalog
{
    protected override void Build()
    {
        Struct("cKeyAsset", 0x10,
            Field("key", DataType.Key, 0x0)
        );
    }
}
