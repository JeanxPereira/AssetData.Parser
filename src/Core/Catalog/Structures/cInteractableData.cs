namespace ReCap.Parser.Catalog;

public sealed class Cinematic : AssetCatalog
{
    protected override void Build()
    {
        Struct("Cinematic", 8,
            ArrayStruct("view", "cCinematicView", 0) // 0x28
        );
    }
}
