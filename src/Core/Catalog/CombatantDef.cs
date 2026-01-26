namespace ReCap.Parser.Catalog;

public sealed class CombatantDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("CombatantDef", 16,
            Field("deathEvent", DataType.Key, 12)
        );
    }
}