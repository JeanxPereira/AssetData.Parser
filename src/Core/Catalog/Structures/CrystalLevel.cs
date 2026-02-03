namespace ReCap.Parser.Catalog;

public sealed class CrystalLevel : AssetCatalog
{
    protected override void Build()
    {
        Struct("CrystalLevel", 0x8,
            Field("offset", DataType.Int, 0x0),
            Field("probability", DataType.Float, 0x4)
        );
    }
}