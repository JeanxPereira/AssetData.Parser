namespace ReCap.Parser.Catalog;

public sealed class cGrassData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cGrassData", 0x124,
            Field("diffuse", DataType.Char, 0x0, 0x40),
            Field("normal", DataType.Char, 0x40, 0x40),
            Field("mat", DataType.Char, 0x80, 0x40),
            Field("diffuseTint", DataType.Vector3, 0xc0),
            Field("specularTint", DataType.Vector3, 0xcc),
            Field("size", DataType.Vector3, 0xd8),
            Field("offset", DataType.Vector2, 0xe4),
            Field("tile", DataType.Vector2, 0xec),
            Field("normalLevel", DataType.Float, 0xf4),
            Field("flexibility", DataType.Float, 0xf8),
            Field("glowLevel", DataType.Float, 0xfc),
            Field("emissiveLevel", DataType.Float, 0x100),
            Field("density", DataType.Float, 0x104),
            Field("posVari", DataType.Float, 0x108),
            Field("heightVari", DataType.Float, 0x10c),
            Field("width", DataType.Float, 0x110),
            Field("bend", DataType.Float, 0x114),
            Field("bendVari", DataType.Float, 0x118),
            Field("seed", DataType.Float, 0x11c),
            Field("cast_shadows", DataType.Bool, 0x120),
            Field("enable", DataType.Bool, 0x121),
            Field("display_volume", DataType.Bool, 0x122)
        );
    }
}
