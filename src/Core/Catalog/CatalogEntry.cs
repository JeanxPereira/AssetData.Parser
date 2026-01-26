namespace ReCap.Parser.Catalog;

public sealed class CatalogEntry : AssetCatalog
{
    protected override void Build()
    {
        Struct("CatalogEntry", 40,
            Field("assetNameWType", DataType.CharPtr, 0),
            Field("compileTime", DataType.Int64, 8),
            Field("version", DataType.Int, 16),
            Field("typeCrc", DataType.UInt, 20),
            Field("dataCrc", DataType.UInt, 24),
            Field("sourceFileNameWType", DataType.CharPtr, 28),
            Array("tags", DataType.CharPtr, 32, countOffset: 4)
        );
    }
}
