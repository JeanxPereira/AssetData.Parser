namespace AssetData.Parser.Catalog;

public sealed class AffixTuning : AssetCatalog
{
    protected override void Build()
    {
        Struct("AffixTuning", 24,
            Array("positiveChance", DataType.UInt32, 0),
            Array("minorChance", DataType.UInt32, 8),
            Array("majorChance", DataType.UInt32, 16)
        );
    }
}
