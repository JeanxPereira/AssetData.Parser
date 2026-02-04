namespace AssetData.Parser.Catalog;

public sealed class ChainLevels : AssetCatalog
{
    protected override void Build()
    {
        Struct("ChainLevel", 72,
            Field("Unk3", DataType.Asset, 0x0),
            Field("Unk4", DataType.Key, 0x14),
            Field("Unk5", DataType.Key, 0x24),
            Field("Unk6", DataType.Key, 0x34),
            Field("Unk7", DataType.Asset, 0x44)
        );
        Struct("ChainLevels", 24,
            ArrayStruct("UnkChainLevel", "Chainlevel", 0x0),
            ArrayStruct("UnkChainLevel2", "Chainlevel", 0x8),
            Field("Unk1", DataType.Float, 0x10),
            Field("Unk2", DataType.UInt32, 0x14)
        );
    }
}
