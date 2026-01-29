namespace ReCap.Parser.Catalog;

public sealed class InteractableDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("InteractableDef", 72,
            Field("numUsesAllowed", DataType.Int, 0),
            Field("interactableAbility", DataType.Key, 16),
            Field("startInteractEvent", DataType.Key, 32),
            Field("endInteractEvent", DataType.Key, 48),
            Field("optionalInteractEvent", DataType.Key, 64),
            Field("challengeValue", DataType.Int, 68)
        );
    }
}
