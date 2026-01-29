/*
 * Reverse engineered from `AssetData::cMapCameraData`.
 * 
 * Ghidra Analysis Guide:
 * - Field Names: Identified by string literals passed to `HashFunctionn` (e.g., "name", "aspect", "near").
 * - Offsets: Found in hardcoded integer assignments adjacent to the name registration (e.g., `_DAT_... = 0x40`).
 * - Default Values: Passed as raw hex arguments to `ContainerStrategy::Get(HEX)`.
 *   Note: These hex values are IEEE 754 float representations (e.g., 0x42340000 converts to 45.0f for FOV).
 */
namespace ReCap.Parser.Catalog;

public sealed class cMapCameraData : AssetCatalog
{
    protected override void Build()
    {
        Struct("cMapCameraData", 0x54,
            Field("name", DataType.Char, 0x0, 0x40),
            Field("fov", DataType.Float, 0x40), // 0x42340000
            Field("aspect", DataType.Float, 0x44), // 0x3fe38e39
            Field("near", DataType.Float, 0x48), // 0x40a00000
            Field("far", DataType.Float, 0x4c), // 0x43480000
            Field("show_bounds", DataType.Bool, 0x50),
            Field("show_pip", DataType.Bool, 0x51)
        );
    }
}