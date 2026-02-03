namespace ReCap.Parser.Catalog;

public sealed class cAIDirector : AssetCatalog
{
    protected override void Build()
    {
        Struct("cAIDirector", 0x78,
            Field("mbBossSpawned", DataType.Bool, 0xd),
            Field("mbBossHorde", DataType.Bool, 0xe),
            Field("mbCaptainSpawned", DataType.Bool, 0xf),
            Field("mbBossComplete", DataType.Bool, 0x10),
            Field("mbHordeSpawned", DataType.Bool, 0x48c),
            Field("mBossId", DataType.ObjId, 0x14),
            Field("mActiveHordeWaves", DataType.Int, 0x47c)
        );
    }
}
