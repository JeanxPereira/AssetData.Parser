namespace AssetData.Parser.Catalog;

public sealed class CrystalDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("CrystalDef", 0x18,
            Field("modifier", DataType.Key, 0),
            Field("type", DataType.Enum, 0x10),
            Field("rarity", DataType.Enum, 0x14)
        );
    }
}