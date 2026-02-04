namespace AssetData.Parser.Catalog;

public sealed class cVolumeDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("cVolumeDef", 0x1c,
            EnumField("shape", "cVolumeDefShape", 0x0),
            Field("boxWidth", DataType.Float, 0x4),
            Field("boxLength", DataType.Float, 0x8),
            Field("boxHeight", DataType.Float, 0xc),
            Field("sphereRadius", DataType.Float, 0x10),
            Field("capsuleHeight", DataType.Float, 0x14),
            Field("capsuleRadius", DataType.Float, 0x18)
        );
    }
}
