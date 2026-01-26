namespace ReCap.Parser.Catalog;

public sealed class CrystalDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("CrystalDef", 32,
            Field("modifier", DataType.Key, 0),
            Field("type", DataType.Key, 16),
            Field("rarity", DataType.Key, 20)
        );
    }
}