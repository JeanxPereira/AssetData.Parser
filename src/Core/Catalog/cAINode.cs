namespace ReCap.Parser.Catalog;

public sealed class cAINode : AssetCatalog
{
    protected override void Build()
    {
        Struct("cAINode", 28,
            Field("mpPhaseData", DataType.Asset, 0),
            Field("mpConditionData", DataType.Asset, 4),
            Field("nodeX", DataType.Int, 12),
            Field("nodeY", DataType.Int, 16),
            Array("output", DataType.Int, 20)
        );
    }
}