namespace AssetData.Parser.Catalog;

public sealed class TriggerVolumeEvents : AssetCatalog
{
    protected override void Build()
    {
        Struct("TriggerVolumeEvents", 32,
            Field("onEnterEvent", DataType.Key, 12),
            Field("onExitEvent", DataType.Key, 28)
        );
    }
}