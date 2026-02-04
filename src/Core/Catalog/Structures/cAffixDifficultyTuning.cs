namespace AssetData.Parser.Catalog;

public sealed class cAffixDifficultyTuning : AssetCatalog
{
    protected override void Build()
    {
        Struct("cAffixDifficultyTuning", 0x18,
            Field("minAffixes", DataType.Int, 0x0),
            Field("maxAffixes", DataType.Int, 0x4),
            Field("chanceToSpawn", DataType.Float, 0x8),
            Field("specialMinAffixes", DataType.Int, 0xc),
            Field("specialMaxAffixes", DataType.Int, 0x10),
            Field("specialChanceToSpawn", DataType.Float, 0x14)
        );
    }
}
