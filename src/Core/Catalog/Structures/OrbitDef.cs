namespace AssetData.Parser.Catalog;

public sealed class OrbitDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("OrbitDef", 12,
            Field("orbitHeight", DataType.Float, 0),
            Field("orbitRadius", DataType.Float, 4),
            Field("orbitSpeed", DataType.Float, 8)
        );
    }
}