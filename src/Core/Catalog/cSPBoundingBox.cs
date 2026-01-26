namespace ReCap.Parser.Catalog;

public sealed class cSPBoundingBox : AssetCatalog
{
    protected override void Build()
    {
        Struct("cSPBoundingBox", 24,
            IStruct("min", "cSPVector3", 0),
            IStruct("max", "cSPVector3", 12)
        );
    }
}
