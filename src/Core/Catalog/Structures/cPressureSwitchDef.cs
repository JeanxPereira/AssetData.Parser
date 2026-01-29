namespace ReCap.Parser.Catalog;

public sealed class cPressureSwitchDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("cPressureSwitchDef", 24,
            NStruct("graphicsState_unpressed", "cNewGfxState", 0),
            NStruct("graphicsState_pressing", "cNewGfxState", 4),
            NStruct("graphicsState_pressed", "cNewGfxState", 8),
            IStruct("volume", "cVolumeDef", 12)
        );
    }
}