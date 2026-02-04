namespace AssetData.Parser.Catalog;

public sealed class NonPlayerClass : AssetCatalog
{
    protected override void Build()
    {
        Struct("NonPlayerClass", 0x7c,
            Field("testingOnly", DataType.Bool, 0x00),
            Field("creatureType", DataType.Enum, 0x04),
            Field("mpClassEffect", DataType.Asset, 0x08),
            Field("mpClassAttributes", DataType.Asset, 0x0C),
            Field("name", DataType.cLocalizedAssetString, 0x10, 0x14),
            Field("challengeValue", DataType.Int, 0x24),
            ArrayEnum("dropType", "NonPlayerClass.dropType", 0x28),
            Field("dropDelay", DataType.Float, 0x34),
            Field("aggroRange", DataType.Float, 0x38),
            Field("alertRange", DataType.Float, 0x3C),
            Field("dropAggroRange", DataType.Float, 0x40),
            Field("mNPCType", DataType.Enum, 0x44),
            Field("npcRank", DataType.Int, 0x48),
            Field("targetable", DataType.Bool, 0x4C),
            Field("description", DataType.cLocalizedAssetString, 0x50, 0x14),
            Field("playerCountHealthScale", DataType.Float, 0x64),
            ArrayStruct("longDescription", "cLongDescription", 0x68),
            ArrayStruct("eliteAffix", "cEliteAffix", 0x70),
            Field("playerPet", DataType.Bool, 0x78)
        );
    }
}
