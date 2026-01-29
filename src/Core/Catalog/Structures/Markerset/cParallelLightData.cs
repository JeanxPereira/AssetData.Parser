namespace ReCap.Parser.Catalog;

public sealed class cParallelLightData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cParallelLightData", 0x54,
            Field("diffuse_color", DataType.Vector3, 0x18),
            Field("diffuse_lamp_power", DataType.Float, 0x24),
            Field("specular_lamp_power", DataType.Float, 0x28),
            Field("fill_diffuse", DataType.Vector3, 0x2c),
            Field("fill_diffuse_power", DataType.Vector3, 0x38),
            Field("fill_specular_power", DataType.Vector3, 0x3c),
            Field("fill_diffuse_power", DataType.Vector3, 0x38),
            Field("has_spec", DataType.Bool, 0x40),
            Field("enable", DataType.Bool, 0x41),
            EnumField("type", "cParallelLightData.type", 0x44),
            Field("radius", DataType.Float, 0x48),
            Field("inner_radius", DataType.Float, 0x4c),
            EnumField("shadowed", "cParallelLightData.shadowed", 0x50)
        );
    }
}
