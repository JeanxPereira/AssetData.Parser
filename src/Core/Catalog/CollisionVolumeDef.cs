namespace ReCap.Parser.Catalog;

public sealed class CollisionVolumeDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("CollisionVolumeDef", 20,
            EnumField("shape", "CollisionShape", 0),
            Field("boxWidth", DataType.Float, 4),
            Field("boxHeight", DataType.Float, 8),
            Field("boxDepth", DataType.Float, 12),
            Field("sphereRadius", DataType.Float, 16)
        );
    }
}