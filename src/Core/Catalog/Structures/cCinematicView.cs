namespace AssetData.Parser.Catalog;

public sealed class cCinematicView : AssetCatalog
{
    protected override void Build()
    {
        Struct("cCinematicView", 0x28,
            Field("position", DataType.Vector3, 0x8),
            Field("orientation", DataType.Vector4, 0xc), // TODO: add cSPQuaternion hash correctly instead of cSPVector4
            Field("fov", DataType.Float, 0x1c),
            Field("targetTime", DataType.Float, 0x20),
            Field("delayTime", DataType.Float, 0x24)
        );
    }
}
