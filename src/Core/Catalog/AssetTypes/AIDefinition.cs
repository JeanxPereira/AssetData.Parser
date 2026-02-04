namespace AssetData.Parser.Catalog;

public sealed class AIDefinition : AssetCatalog
{
    protected override void Build()
    {
        Struct("AIDefinition", 640,
            ArrayStruct("ainode", "cAINode", 0),
            Key("deathAbility", 512)
        );
    }
}
