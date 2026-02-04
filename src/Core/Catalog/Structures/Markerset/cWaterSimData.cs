namespace AssetData.Parser.Catalog;

public sealed class cWaterSimData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cWaterSimData", 0xc8,
            Field("water_pos", DataType.Vector2, 0x0),
            Field("water_pos_vari", DataType.Vector2, 0x8),
            Field("water_size", DataType.Vector2, 0x10),
            Field("water_size_vari", DataType.Vector2, 0x18),
            Field("water_angle", DataType.Float, 0x20),
            Field("water_angle_vari", DataType.Float, 0x24),
            Field("water_intensity", DataType.Float, 0x28),
            Field("water_intensity_vari", DataType.Float, 0x2c),
            Field("water_freq", DataType.Float, 0x30),
            Field("water_wave_speed", DataType.Float, 0x38),
            Field("water_dampening", DataType.Float, 0x3c),
            Field("water_normal_scale", DataType.Float, 0x40),
            Field("water_brush", DataType.Char, 0x44, 0x40),
            Field("water_mask", DataType.Char, 0x84, 0x40),
            Field("water_blend", DataType.Bool, 0xc4)
        );
    }
}
