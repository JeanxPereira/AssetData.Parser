namespace AssetData.Parser.Catalog;

public sealed class cSplineCameraData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cSplineCameraData", 0x84,
            IStruct("node", "cSplineCameraNodeBaseData", 0x0),
            Field("duration", DataType.Float, 0x80)
        );
    }
}
