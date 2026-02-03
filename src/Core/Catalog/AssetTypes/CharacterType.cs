namespace ReCap.Parser.Catalog;

public sealed class CharacterType : AssetCatalog
{
    protected override void Build()
    {
        Struct("CharacterType", 0x7c,
            Field("BaseResistance_Technology", DataType.Float, 0x0),
            Field("BaseResistance_Spacetime", DataType.Float, 0xc),
            Field("BaseResistance_Life", DataType.Float, 0x18),
            Field("BaseResistance_Elements", DataType.Float, 0x24),
            Field("BaseResistance_Supernatural", DataType.Float, 0x30),
            Field("DamageMultiplier_Technology", DataType.Float, 0x3c),
            Field("DamageMultiplier_Spacetime", DataType.Float, 0x48),
            Field("DamageMultiplier_Life", DataType.Float, 0x54),
            Field("DamageMultiplier_Elements", DataType.Float, 0x60),
            Field("DamageMultiplier_Supernatural", DataType.Float, 0x6c),
            Field("UIColor", DataType.UInt32, 0x78)
        );
    }
}
