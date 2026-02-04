namespace AssetData.Parser.Catalog;

public sealed class CombatEvent : AssetCatalog
{
    protected override void Build()
    {
        Struct("CombatEvent", 16,
            Field("flags", DataType.UInt16, 0x0),
            Field("deltaHealth", DataType.Float, 0x4),
            Field("absorbedAmount", DataType.Float, 0x8),
            Field("targetID", DataType.ObjId, 0xc),
            Field("sourceID", DataType.ObjId, 0x10),
            Field("abilityID", DataType.ObjId, 0x14),
            Field("damageDirection", DataType.Vector3, 0x18),
            Field("integerHpChange", DataType.Int32, 0x24)
        );
    }
}