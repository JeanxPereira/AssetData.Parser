namespace AssetData.Parser.Catalog;

public sealed class cNewGfxState : AssetCatalog
{
    protected override void Build()
    {
        Struct("cNewGfxState", 40,
            Field("prefab", DataType.Asset, 0),
            Field("model", DataType.Key, 16),
            Field("animation", DataType.Key, 32)
        );
    }
}
