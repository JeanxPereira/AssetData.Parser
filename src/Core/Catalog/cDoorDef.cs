namespace ReCap.Parser.Catalog;

public sealed class cDoorDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("cDoorDef", 24,
            NStruct("graphicsState_open", "cNewGfxState", 0)
        );
    }
}