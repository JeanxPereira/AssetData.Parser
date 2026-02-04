namespace AssetData.Parser.Catalog;

public sealed class cSwitchDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("cSwitchDef", 24,
            NStruct("graphicsState_unpressed", "cNewGfxState", 0),
            NStruct("graphicsState_pressing", "cNewGfxState", 4),
            NStruct("graphicsState_pressed", "cNewGfxState", 8)
        );
    }
}