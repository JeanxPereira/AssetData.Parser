namespace ReCap.Parser.Catalog;

public sealed class cCameraComponent : AssetCatalog
{
    protected override void Build()
    {
        Struct("cCameraComponent", 0x1c,
            Field("azimuth", DataType.Float, 0x8),
            Field("elevation", DataType.Float, 0xc),
            Field("distance", DataType.Float, 0x10),
            Field("transitionRate", DataType.Float, 0x14),
            Field("duration", DataType.Float, 0x18)
        );
    }
}
