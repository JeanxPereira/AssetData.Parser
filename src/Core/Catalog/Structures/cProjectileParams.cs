namespace ReCap.Parser.Catalog;

public sealed class cProjectileParams : AssetCatalog
{
    protected override void Build()
    {
        Struct("cProjectileParams", 0x290,
            Field("mSpeed", DataType.Float, 0x0),
            Field("mAcceleration", DataType.Float, 0x4),
            Field("mJinkInfo", DataType.UInt32, 0x8),
            Field("mRange", DataType.Float, 0xc),
            Field("mSpinRate", DataType.Float, 0x10),
            Field("mDirection", DataType.Vector3, 0x14),
            Field("mProjectileFlags", DataType.UInt8, 0x20),
            Field("mHomingDelay", DataType.Float, 0x24),
            Field("mTurnRate", DataType.Float, 0x28),
            Field("mTurnAcceleration", DataType.Float, 0x2c),
            Field("mEccentricity", DataType.Float, 0x34),
            Field("mPiercing", DataType.Bool, 0x30),
            Field("mIgnoreGroundCollide", DataType.Bool, 0x31),
            Field("mIgnoreCreatureCollide", DataType.Bool, 0x32),
            Field("mCombatantSweepHeight", DataType.Bool, 0x38)
        );
    }
}
