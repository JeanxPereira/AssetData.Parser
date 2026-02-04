namespace AssetData.Parser.Catalog;

public sealed class LocomotionTuning : AssetCatalog
{
    protected override void Build()
    {
        Struct("LocomotionTuning", 12,
            Field("acceleration", DataType.Float, 0),
            Field("deceleration", DataType.Float, 4),
            Field("turnRate", DataType.Float, 8)
        );
    }
}