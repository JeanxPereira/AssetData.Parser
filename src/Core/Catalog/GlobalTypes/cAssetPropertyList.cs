namespace AssetData.Parser.Catalog;

public sealed class cAssetPropertyList : AssetCatalog
{
    protected override void Build()
    {
        Struct("cAssetPropertyList", 0x8,
            ArrayStruct("mpAssetProperties", "cAssetProperty", 0x0)
        );
    }
}
