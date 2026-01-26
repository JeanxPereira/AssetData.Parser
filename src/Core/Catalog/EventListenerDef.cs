namespace ReCap.Parser.Catalog;

public sealed class EventListenerDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("EventListenerDef", 8,
            ArrayStruct("listener", "EventListenerData", 0)
        );
    }
}