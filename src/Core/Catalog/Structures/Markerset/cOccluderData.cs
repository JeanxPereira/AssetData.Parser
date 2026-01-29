namespace ReCap.Parser.Catalog;

public sealed class cOccluderData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cOccluderData", 0x2c,
            Field("name", DataType.Char, 0x0, 0x20),
            Field("width", DataType.Int, 0x20),
            Field("height", DataType.Int, 0x24),
            Field("active", DataType.Bool, 0x28)
        );
    }
}
