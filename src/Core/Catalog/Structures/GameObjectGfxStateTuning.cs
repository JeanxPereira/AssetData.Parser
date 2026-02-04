namespace AssetData.Parser.Catalog;

public sealed class GameObjectGfxStateTuning : AssetCatalog
{
    protected override void Build()
    {
        Struct("GameObjectGfxStateTuning", 24,
            Field("name", DataType.Key, 12),
            Field("animationStartTime", DataType.Float, 16),
            Field("animationRate", DataType.Float, 20)
        );
    }
}