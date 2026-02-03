namespace ReCap.Parser.Catalog;

public sealed class cEliteAffix : AssetCatalog
{
    protected override void Build()
    {
        Struct("cEliteAffix", 0xc,
            Field("mpNPCAffix", DataType.Asset, 0x0),
            Field("minDifficulty", DataType.Int, 0x4),
            Field("maxDifficulty", DataType.Int, 0x8)
        );
    }
}
