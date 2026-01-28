namespace ReCap.Parser.Catalog;

public sealed class cAssetProperty : AssetCatalog
{
    protected override void Build()
    {
        Struct("cAssetProperty", 188,
            Field("GUID", DataType.UInt32, 0),
            CharBuffer("name", 4, 80),
            Field("type", DataType.UInt32, 84),
            CharBuffer("value", 88, 80)
        );
    }
}
