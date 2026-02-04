namespace AssetData.Parser.Catalog;

public sealed class cAnimatedData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cAnimatedData", 0x4,
            Field("animator", DataType.UInt32, 0x0)
            // Field("animator", DataType.UInt32, 0x4),
            // Field("animator", DataType.UInt32, 0x8)
        );
    }
}
