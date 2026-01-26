namespace ReCap.Parser.Catalog;

public sealed class TriggerVolumeComponentDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("TriggerVolumeComponentDef", 4,
            NStruct("triggerVolume", "TriggerVolumeDef", 0)
        );
    }
}