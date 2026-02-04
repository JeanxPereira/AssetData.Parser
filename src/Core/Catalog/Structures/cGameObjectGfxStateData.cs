namespace AssetData.Parser.Catalog;

public sealed class cGameObjectGfxStateData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cGameObjectGfxStateData", 56,
            Field("name", DataType.Key, 12),
            Field("model", DataType.Key, 28),
            Field("prefab", DataType.Key, 48),
            Field("animation", DataType.Key, 44),
            Field("animationLoops", DataType.Key, 52)
        );
    }
}
