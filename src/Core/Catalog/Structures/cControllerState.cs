namespace AssetData.Parser.Catalog;

public sealed class cControllerState : AssetCatalog
{
    protected override void Build()
    {
        Struct("cControllerState", 0x20,
            Field("holdPosition", DataType.Bool, 0x0),
            Field("moveDirection", DataType.Vector3, 0x4)
        );
    }
}
