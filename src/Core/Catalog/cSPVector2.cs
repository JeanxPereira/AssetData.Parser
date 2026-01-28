namespace ReCap.Parser.Catalog;

/// <summary>
/// 2D Vector type - 8 bytes (2 floats).
/// </summary>
public sealed class cSPVector2Catalog : AssetCatalog
{
    protected override void Build()
    {
        Struct("cSPVector2", 8,
            Float("x", 0),
            Float("y", 4)
        );
    }
}
