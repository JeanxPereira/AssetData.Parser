namespace ReCap.Parser.Catalog;

public sealed class cLootData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cLootData", 0x80,
            Field("crystalLevel", DataType.Int, 0x0),
            Field("mLootItem.id", DataType.UInt64, 0x10),
            Field("mLootItem.rigblockAsset", DataType.Asset, 0x18),
            Field("mLootItem.suffixAssetId", DataType.UInt32, 0x1c),
            Field("mLootItem.prefixAssetId1", DataType.UInt32, 0x20),
            Field("mLootItem.prefixAssetId2", DataType.UInt32, 0x24),
            Field("mLootItem.itemLevel", DataType.Int, 0x28),
            Field("mLootItem.rarity", DataType.UInt64, 0x2c),
            Field("mLootInstanceId", DataType.UInt64, 0x40),
            Field("mDNAAmount", DataType.Float, 0x48)
        );
    }
}
