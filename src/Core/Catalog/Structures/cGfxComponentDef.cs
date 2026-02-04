namespace AssetData.Parser.Catalog;

public sealed class cGfxComponentDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("cGfxComponentDef", 0x14,
            Field("gfxType", DataType.Enum, 0x0),
            Field("gfxKey", DataType.Key, 0x10)
        );
    }
}
