namespace ReCap.Parser.Catalog;

public sealed class cAgentBlackboard : AssetCatalog
{
    protected override void Build()
    {
        Struct("cAgentBlackboard", 0x598,
            Field("mTarget", DataType.ObjId, 0x550),
            Field("mbInCombat", DataType.Bool, 0x554),
            Field("mStealthType", DataType.UInt8, 0x556),
            Field("mbTargetable", DataType.Bool, 0x578),
            Field("mNumAttackers", DataType.UInt32, 0x57c)
        );
    }
}
