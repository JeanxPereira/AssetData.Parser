namespace ReCap.Parser.Catalog;

public sealed class cPointLightData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cPointLightData", 0x54,
            Field("diffuse_color", DataType.Vector3, 0x0),
            Field("diffuse_lamp_power", DataType.Float, 0xc),
            Field("specular_lamp_power", DataType.Float, 0x10),
            Field("inner_radius", DataType.Float, 0x14),
            Field("radius", DataType.Float, 0x18),
            Field("gobo", DataType.Key, 0x28),
            Field("frames", DataType.Int, 0x2c),
            Field("has_spec", DataType.Bool, 0x30),
            Field("enable", DataType.Bool, 0x31),
            Field("show_volume", DataType.Bool, 0x32),
            Field("wind_blown", DataType.Bool, 0x34),
            Field("wind_pivot_pos", DataType.Vector3, 0x38),
            Field("wind_pivot_rot", DataType.Vector3, 0x44),
            Field("wind_flex", DataType.Float, 0x50)
        );
    }
}
