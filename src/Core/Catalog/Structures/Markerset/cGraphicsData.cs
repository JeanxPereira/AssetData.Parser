namespace AssetData.Parser.Catalog;

public sealed class cGraphicsData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cGraphicsData", 0x518,
            Field("camera_far_clip", DataType.Float, 0x0),
            Field("shadow_dir", DataType.Vector3, 0x4),
            Field("shadow_transparency", DataType.Float, 0x10),
            Field("shadow_zbias", DataType.Float, 0x14),
            Field("shadow_camera_z_fade", DataType.Float, 0x18),
            
            Field("shadow_view_left", DataType.Float, 0x1c),
            Field("shadow_view_top", DataType.Float, 0x20),
            Field("shadow_view_right", DataType.Float, 0x24),
            Field("shadow_view_bottom", DataType.Float, 0x28),
            Field("shadow_view_height", DataType.Float, 0x2c),
            Field("shadow_view_near", DataType.Float, 0x30),
            Field("shadow_view_far", DataType.Float, 0x34),
            
            Field("shadow_cull_left", DataType.Float, 0x38),
            Field("shadow_cull_top", DataType.Float, 0x3c),
            Field("shadow_cull_right", DataType.Float, 0x40),
            Field("shadow_cull_bottom", DataType.Float, 0x44),
            Field("shadow_cull_near", DataType.Float, 0x48),
            Field("shadow_cull_far", DataType.Float, 0x4c),
            
            Field("bloom", DataType.Float, 0x50),
            Field("brightness", DataType.Float, 0x54),
            Field("contrast", DataType.Float, 0x58),
            Field("saturation", DataType.Float, 0x5c),
            Field("hue", DataType.Float, 0x60),
            Field("color_adjust_texture", DataType.Char, 0x64, 0x40),

            Field("wind_dir", DataType.Vector2, 0xa4),
            Field("wind_str", DataType.Float, 0xac),
            
            Field("cloud_shadow_texture", DataType.Char, 0xb0, 0x40),
            Field("cloud_shadow_direction", DataType.Vector2, 0xf0),
            Field("cloud_shadow_tile", DataType.Vector2, 0xf8),
            Field("cloud_shadow_rate", DataType.Float, 0x100),
            Field("cloud_shadow_start", DataType.Float, 0x104),
            Field("cloud_shadow_end", DataType.Float, 0x108),
            Field("cloud_shadow_alpha", DataType.Float, 0x10c),
            
            Field("fog_color", DataType.Vector3, 0x110),
            Field("fog_density", DataType.Float, 0x11c),
            Field("fog_amplitude", DataType.Float, 0x120),
            Field("fog_ground_start", DataType.Float, 0x124),
            Field("fog_ground_end", DataType.Float, 0x128),
            Field("fog_anim_rate", DataType.Float, 0x12c),
            // "int" fog_strip_length but labeled 0x130
            Field("fog_strip_length", DataType.Int, 0x130), 
            Field("fog_dir", DataType.Vector2, 0x134),
            Field("fog_rate", DataType.Float, 0x13c),
            Field("fog_scale", DataType.Vector2, 0x140),
            Field("fog_texture", DataType.Char, 0x148, 0x40),

            Field("player_diffuse", DataType.Float, 0x188),
            Field("player_specular", DataType.Float, 0x18c),
            Field("player_desaturate", DataType.Float, 0x190),
            Field("player_emissive_level", DataType.Float, 0x194),

            Field("creature_diffuse", DataType.Float, 0x198),
            Field("creature_specular", DataType.Float, 0x19c),
            Field("creature_desaturate", DataType.Float, 0x1a0),
            Field("creature_emissive_level", DataType.Float, 0x1a4),

            Field("flora_diffuse", DataType.Float, 0x1a8),
            Field("flora_specular", DataType.Float, 0x1ac),
            Field("flora_desaturate", DataType.Float, 0x1b0),
            Field("flora_emissive_level", DataType.Float, 0x1b4),

            Field("mineral_diffuse", DataType.Float, 0x1b8),
            Field("mineral_specular", DataType.Float, 0x1bc),
            Field("mineral_desaturate", DataType.Float, 0x1c0),
            Field("mineral_emissive_level", DataType.Float, 0x1c4),

            Field("toonCenterMin", DataType.Float, 0x1c8),
            Field("toonAdjacentMin", DataType.Float, 0x1cc),
            Field("toonCornerMin", DataType.Float, 0x1d0),
            Field("toonCenterMax", DataType.Float, 0x1d4),
            Field("toonAdjacentMax", DataType.Float, 0x1d8),
            Field("toonCornerMax", DataType.Float, 0x1dc),

            IStruct("water0", "cWaterSimData", 0x1e0),
            IStruct("water1", "cWaterSimData", 0x2a8),
            IStruct("water2", "cWaterSimData", 0x370),
            IStruct("water3", "cWaterSimData", 0x438),

            Field("camera_blend", DataType.Bool, 0x500),
            Field("color_blend", DataType.Bool, 0x501),
            Field("shadow_blend", DataType.Bool, 0x502),
            Field("bloom_blend", DataType.Bool, 0x503),
            Field("wind_blend", DataType.Bool, 0x504),
            Field("cloud_blend", DataType.Bool, 0x505),
            Field("fog_blend", DataType.Bool, 0x506),
            Field("levels_blend", DataType.Bool, 0x507),
            Field("global", DataType.Bool, 0x508),

            Field("radius", DataType.Float, 0x50c),
            Field("inner_radius", DataType.Float, 0x510),
            Field("display_volume", DataType.Bool, 0x514)
        );
    }
}