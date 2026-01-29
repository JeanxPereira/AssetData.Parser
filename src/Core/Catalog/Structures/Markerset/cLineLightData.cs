namespace ReCap.Parser.Catalog;

public sealed class cLineLightData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cLineLightData", 0x54,
            Field("diffuse_color", DataType.Vector3, 0x0),
            Field("diffuse_lamp_power", DataType.Float, 0xc),
            Field("specular_lamp_power", DataType.Float, 0x10),
            Field("inner_radius", DataType.Float, 0x14),
            Field("radius", DataType.Float, 0x18),
            Field("length", DataType.Float, 0x1c),
            Field("gobo", DataType.Key, 0x2c),
            Field("frames", DataType.Int, 0x2c),
            Field("has_spec", DataType.Bool, 0x3c),
            Field("enable", DataType.Bool, 0x31),
            Field("show_volume", DataType.Bool, 0x32),
            Field("wind_blown", DataType.Bool, 0x34),
            Field("wind_pivot_pos", DataType.Vector3, 0xc),
            Field("wind_pivot_rot", DataType.Vector3, 0x44),
            Field("wind_flex", DataType.Float, 0x50)
        );
    }
}
