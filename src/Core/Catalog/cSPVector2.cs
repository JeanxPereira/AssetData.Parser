namespace ReCap.Parser.Catalog;

public sealed class cSPVector2 : AssetCatalog
{
    protected override void Build()
    {
        Struct("cSPVector2", 8,
            Field("x", DataType.Float, 0),
            Field("y", DataType.Float, 4)
        );
    }
}
