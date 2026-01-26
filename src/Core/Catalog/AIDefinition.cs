namespace ReCap.Parser.Catalog;

public sealed class AIDefinition : AssetCatalog
{
    protected override void Build()
    {
        Struct("AIDefinition", 640,
            ArrayStruct("ainode", "cAINode", 0),
            Field("deathAbility", DataType.Key, 512)
        );
    }
}
