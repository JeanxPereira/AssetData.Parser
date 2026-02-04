namespace AssetData.Parser.Catalog;

public sealed class SpawnPointDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("SpawnPointDef", 8,
            EnumField("sectionType", "SpawnPointDef.sectionType", 0),
            Field("activatesSpike", DataType.Bool, 4)
        );
    }
}
