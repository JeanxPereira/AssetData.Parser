namespace AssetData.Parser.Catalog;

public sealed class cInteractableData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cInteractableData", 0x34,
            Field("mNumTimesUsed", DataType.Int, 0x8),
            Field("mNumUsesAllowed", DataType.Int, 0xc),
            Field("mInteractableAbility", DataType.UInt32, 0x14)
        );
    }
}
