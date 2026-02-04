namespace AssetData.Parser.Catalog;

public sealed class cAnimatorData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cAnimatorData", 0x10,
            Field("animator_name", DataType.CharPtr, 0x0),
            Field("rate", DataType.Float, 0x4),
            Field("delay", DataType.Float, 0x8),
            Field("track", DataType.CharPtr, 0xC)
        );
    }
}
