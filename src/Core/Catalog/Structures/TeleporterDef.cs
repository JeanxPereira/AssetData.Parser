namespace AssetData.Parser.Catalog;

public sealed class TeleporterDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("TeleporterDef", 12,
            Field("destinationMarkerId", DataType.UInt32, 0),
            NStruct("triggerVolume", "TriggerVolumeDef", 4),
            Field("deferTriggerCreation", DataType.Bool, 8)
        );
    }
}