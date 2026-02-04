namespace AssetData.Parser.Catalog;

public sealed class cCombatantData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cCombatantData", 0x70,
            Field("mHitPoints", DataType.Float, 0x8),
            Field("mManaPoints", DataType.Vector4, 0x44)
        );
    }
}
