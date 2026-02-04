namespace AssetData.Parser.Catalog;

public sealed class cAssetProperty : AssetCatalog
{
    protected override void Build()
    {
        Struct("cAssetProperty", 0xbc,
            Field("name", DataType.Char, 0x4, 0x50),
            Field("value", DataType.UInt32, 0x58, 0x50),
            Field("key", DataType.UInt32, 0x0), // TODO: add DataType.GUID
            Field("key", DataType.UInt32, 0x54) // TODO: add DataType.GUID
        );
    }
}
