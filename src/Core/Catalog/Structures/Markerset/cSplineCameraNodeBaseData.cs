namespace AssetData.Parser.Catalog;

public sealed class cSplineCameraNodeBaseData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cSplineCameraNodeBaseData", 0x80,
            Field("name", DataType.Char, 0x0, 0x20),
            Field("fov", DataType.Float, 0x20),
            Field("near", DataType.Float, 0x24),
            Field("far", DataType.Float, 0x28),
            Field("knot", DataType.Int, 0x2c),
            Field("wait", DataType.Float, 0x30),
            Field("skipable", DataType.Bool, 0x34),
            Field("message", DataType.Char, 0x35, 0x4b)
        );
    }
}