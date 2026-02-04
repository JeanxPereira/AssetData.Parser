namespace AssetData.Parser.Catalog;

public sealed class cGameObjectGfxStates : AssetCatalog
{
    protected override void Build()
    {
        Struct("cGameObjectGfxStates", 8,
            ArrayStruct("state", "cGameObjectGfxStateData", 0)
        );
    }
}
