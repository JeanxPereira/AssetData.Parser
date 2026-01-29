namespace ReCap.Parser.Catalog;

/// <summary>
/// AI Definition - creature AI behavior tree.
/// Size: 640 bytes
/// </summary>
public sealed class AIDefinitionCatalog : AssetCatalog
{
    protected override void Build()
    {
        Struct("AIDefinition", 640,
            ArrayStruct("ainode", "cAINode", 0),
            Key("deathAbility", 512)
        );
    }
}
