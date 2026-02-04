namespace AssetData.Parser.Catalog;

public sealed class SpaceshipSpawnPointDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("SpaceshipSpawnPointDef", 4,
            Field("index", DataType.Int, 0)
        );
    }
}