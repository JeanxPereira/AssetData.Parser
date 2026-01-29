namespace ReCap.Parser.Catalog;

public sealed class Phase : AssetCatalog
{
    protected override void Build()
    {
        Struct("Phase", 16,
            ArrayStruct("gambit", "cGambitDefinition", 0, countOffset: 4),
            EnumField("phaseType", "phaseType", 8),
            Field("startNode", DataType.Bool, 12)
        );
    }
}
