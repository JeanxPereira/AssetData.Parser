namespace AssetData.Parser.Catalog;

public sealed class cLobParams : AssetCatalog
{
    protected override void Build()
    {
        Struct("cLobParams", 0x54,
            Field("planeDirLinearParam", DataType.Float, 0x48),
            Field("upLinearParam", DataType.Float, 0x4c),
            Field("upQuadraticParam", DataType.Float, 0x50),
            Field("lobUpDir", DataType.Vector3, 0x18),
            Field("planeDir", DataType.Vector3, 0x3c),
            Field("bounceNum", DataType.Int, 0x30),
            Field("bounceRestitution", DataType.Float, 0x34),
            Field("groundCollisionOnly", DataType.Bool, 0x38),
            Field("stopBounceOnCreatures", DataType.Bool, 0x39)
        );
    }
}
