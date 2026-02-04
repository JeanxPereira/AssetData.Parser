namespace AssetData.Parser.Catalog;

public sealed class Affix : AssetCatalog
{
    protected override void Build()
    {
        Struct("affix", 0x6c,
            Field("namespace", DataType.Char, 0x10, 0x50),
            Field("localizedText", DataType.UInt32, 0x60),
            Field("objective", DataType.UInt32, 0x64),
            Field("handledEvents", DataType.UInt32, 0x68)
        );
    }   
}
