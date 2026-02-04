namespace AssetData.Parser.Catalog;

public sealed class TestAsset : AssetCatalog
{
    protected override void Build()
    {
        Struct("TestAsset", 8,
            Field("foo", DataType.Int, 0),
            Field("bar", DataType.Int, 0)
        );
    }
}
