namespace ReCap.Parser.Catalog;

public sealed class AffixTuning : AssetCatalog
{
    protected override void Build()
    {
        Struct("AffixTuning", 24,
            Array("positiveChance", DataType.UInt, 0),
            Array("minorChance", DataType.UInt, 8),
            Array("majorChance", DataType.UInt, 16)
        );
    }
}
