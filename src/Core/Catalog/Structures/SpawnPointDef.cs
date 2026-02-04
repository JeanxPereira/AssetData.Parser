namespace AssetData.Parser.Catalog;

public sealed class SpawnTriggerDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("SpawnTriggerDef", 28,
            NStruct("triggerVolume", "TriggerVolumeDef", 0),
            Field("deathEvent", DataType.Key, 16),
            Field("challengeOverride", DataType.Int, 20),
            Field("waveOverride", DataType.Int, 24)
        );
    }
}
