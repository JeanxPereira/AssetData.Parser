namespace ReCap.Parser.Catalog;

public sealed class CrystalDropDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("CrystalDropDef", 0x10,
            Field("minLevel", DataType.Int, 0x0),
            Field("maxLevel", DataType.Int, 0x4),
            Field("weight", DataType.Int, 0x8),
            Field("mpNoun", DataType.Asset, 0xc)
        );
    }
}