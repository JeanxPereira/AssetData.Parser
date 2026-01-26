namespace ReCap.Parser.Catalog;

public sealed class cSPVector3 : AssetCatalog
{
    protected override void Build()
    {
        Struct("cSPVector3", 16,
            Field("x", DataType.Float, 0),
            Field("y", DataType.Float, 4),
            Field("z", DataType.Float, 8)
        );
    }
}
