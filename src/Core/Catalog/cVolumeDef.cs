namespace ReCap.Parser.Catalog;

public sealed class cVolumeDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("cVolumeDef", 28,
            EnumField("shape", "cVolumeDefShape", 0),
            Field("boxWidth", DataType.Float, 4),
            Field("boxLength", DataType.Float, 8),
            Field("boxHeight", DataType.Float, 12),
            Field("sphereRadius", DataType.Float, 16),
            Field("capsuleHeight", DataType.Float, 20),
            Field("capsuleRadius", DataType.Float, 24)
        );
    }
}