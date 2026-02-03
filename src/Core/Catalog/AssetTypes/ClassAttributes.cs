namespace ReCap.Parser.Catalog;

public sealed class ClassAttributes : AssetCatalog
{
    protected override void Build()
    {
        Struct("ClassAttributes", 0x58,
            Field("baseHealth", DataType.Float, 0x0),
            Field("baseMana", DataType.Float, 0x4),
            Field("baseStrength", DataType.Float, 0x8),
            Field("baseDexterity", DataType.Float, 0xc),
            Field("baseMind", DataType.Float, 0x10),
            Field("basePhysicalDefense", DataType.Float, 0x14),
            Field("baseMagicalDefense", DataType.Float, 0x18),
            Field("baseEnergyDefense", DataType.Float, 0x1c),
            Field("baseCritical", DataType.Float, 0x20),
            Field("baseCombatSpeed", DataType.Float, 0x24),
            Field("baseNonCombatSpeed", DataType.Float, 0x28),
            Field("baseStealthDetection", DataType.Float, 0x2c),
            Field("baseMovementSpeedBuff", DataType.Float, 0x30),
            Field("maxHealth", DataType.Float, 0x34),
            Field("maxMana", DataType.Float, 0x38),
            Field("maxStrength", DataType.Float, 0x3c),
            Field("maxDexterity", DataType.Float, 0x40),
            Field("maxMind", DataType.Float, 0x44),
            Field("maxPhysicalDefense", DataType.Float, 0x48),
            Field("maxMagicalDefense", DataType.Float, 0x4c),
            Field("maxEnergyDefense", DataType.Float, 0x50),
            Field("maxCritical", DataType.Float, 0x54)
        );
    }
}
