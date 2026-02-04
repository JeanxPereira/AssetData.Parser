namespace AssetData.Parser.Catalog;

public sealed class cGambitDefinition : AssetCatalog
{
    protected override void Build()
    {
        Struct("cGambitDefinition", 52,
            Field("condition", DataType.Key, 12),
            ArrayStruct("conditionProps", "cAssetProperty", 16, countOffset: 4),
            Field("ability", DataType.Key, 36),
            ArrayStruct("abilityProps", "cAssetProperty", 40, countOffset: 4),
            Field("randomizeCooldown", DataType.Bool, 48)
        );
    }
}
