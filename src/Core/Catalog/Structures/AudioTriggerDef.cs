namespace AssetData.Parser.Catalog;

public sealed class AudioTriggerDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("AudioTriggerDef", 32,
            EnumField("type", "type", 0),
            Field("sound", DataType.Key, 16),
            Field("bIs3D", DataType.Bool, 20),
            Field("retrigger", DataType.Bool, 21),
            Field("hardStop", DataType.Bool, 22),
            Field("isVoiceover", DataType.Bool, 23),
            Field("voiceLifetime", DataType.Float, 24),
            NStruct("triggerVolume", "TriggerVolumeDef", 28)
        );
    }
}