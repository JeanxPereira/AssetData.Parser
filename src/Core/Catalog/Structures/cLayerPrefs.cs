namespace AssetData.Parser.Catalog;

public sealed class cLayerPrefs : AssetCatalog
{
    protected override void Build()
    {
        Struct("cLayerPrefs", 0x8,
            Field("layerName", DataType.CharPtr, 0x0),
            Field("locked", DataType.CharPtr, 0x4),
            Field("hidden", DataType.CharPtr, 0x5)
        );
    }
}
