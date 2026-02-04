namespace AssetData.Parser.Catalog;

public sealed class cWaterData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cWaterData", 0x1b0,
            Field("size", DataType.Vector2, 0x0),
            Field("reflection", DataType.Char, 0x8, 0x40),
            Field("mask", DataType.Char, 0x48, 0x40),
            Field("normal_mask", DataType.Char, 0x88, 0x40),
            Field("splash_effect", DataType.Char, 0xC8, 0x40),
            Field("foam_effect", DataType.Char, 0x108, 0x40),
            Field("diffuseTint", DataType.Vector3, 0x148),
            Field("specularTint", DataType.Vector3, 0x154),
            Field("depthFogColor", DataType.Vector3, 0x160),
            Field("tile", DataType.Vector2, 0x16c),
            Field("normalLevel", DataType.Float, 0x174),
            Field("specExponent", DataType.Float, 0x178),
            Field("fresnel_bias", DataType.Float, 0x17c),
            Field("fresnel_power", DataType.Float, 0x180),
            Field("refract_level", DataType.Float, 0x184),
            Field("reflect_level", DataType.Float, 0x188),
            Field("depth_fog", DataType.Float, 0x18c),
            Field("refract_cue_bias", DataType.Float, 0x190),
            Field("refract_cue_scale", DataType.Float, 0x194),
            Field("reflect_cue_bias", DataType.Float, 0x198),
            Field("reflect_cue_scale", DataType.Float, 0x19c),
            Field("normal_cue_bias", DataType.Float, 0x1a0),
            Field("normal_cue_scale", DataType.Float, 0x1a4),
            Field("simulation", DataType.Int, 0x1a8),
            Field("soft_edges", DataType.Bool, 0x1ac),
            Field("interactive", DataType.Bool, 0x1ad),
            Field("enable", DataType.Bool, 0x1ae),
            Field("display_volume", DataType.Bool, 0x1af)
        );
    }
}
