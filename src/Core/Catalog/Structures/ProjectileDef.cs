namespace AssetData.Parser.Catalog;

public sealed class ProjectileDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("ProjectileDef", 12,
            NStruct("creatureCollisionVolume", "CollisionVolumeDef", 0),
            NStruct("otherCollisionVolume", "CollisionVolumeDef", 4),
            EnumField("targetType", "ProjectileDef.targetType", 8)
        );
    }
}