namespace AssetData.Parser.Catalog;

public sealed class EventListenerData : AssetCatalog
{
    protected override void Build()
    {
        Struct("EventListenerData", 40,
            Field("event", DataType.Key, 0),
            Field("callback", DataType.Key, 28),
            Field("luaCallback", DataType.CharPtr, 36)
        );
    }
}