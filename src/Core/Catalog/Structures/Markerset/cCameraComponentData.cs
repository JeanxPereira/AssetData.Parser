namespace AssetData.Parser.Catalog;

public sealed class cCameraComponentData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cCameraComponentData", 0x14,
            Field("azimuth", DataType.Float, 0x0),
            Field("elevation", DataType.Float, 0x4),
            Field("distance", DataType.Float, 0x8),
            Field("transitionRate", DataType.Float, 0xc),
            Field("duration", DataType.Float, 0x10)
        );
    }
}
