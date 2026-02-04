namespace AssetData.Parser.Catalog;

public sealed class cLocomotionData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cLocomotionData", 0x290,
            Field("lobStartTime", DataType.UInt64, 0xd8),
            Field("lobPrevSpeedModifier", DataType.UInt64, 0xe0),
            IStruct("lobParams", "cLobParams", 0xe4),
            IStruct("mProjectileParams", "cProjectileParams", 0x44),
            Field("mGoalFlags", DataType.UInt32, 0x144),
            Field("mGoalPosition", DataType.Vector3, 0x148),
            Field("mPartialGoalPosition", DataType.Vector3, 0x154),
            Field("mFacing", DataType.Vector3, 0x178),
            Field("mExternalLinearVelocity", DataType.Vector3, 0x184),
            Field("mExternalForce", DataType.Vector3, 0x190),
            Field("mAllowedStopDistance", DataType.Float, 0x19c),
            Field("mDesiredStopDistance", DataType.Float, 0x1a0),
            Field("mTargetObjectId", DataType.ObjId, 0x90),
            Field("mTargetPosition", DataType.Vector3, 0x1ac),
            Field("mExpectedGeoCollision", DataType.Vector3, 0x84),
            Field("mInitialDirection", DataType.Vector3, 0x9c),
            Field("mOffset", DataType.Vector3, 0x138),
            Field("reflectedLastUpdate", DataType.Int, 0x8)
        );
    }
}
