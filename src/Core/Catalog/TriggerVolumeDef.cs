namespace ReCap.Parser.Catalog;

public sealed class TriggerVolumeDef : AssetCatalog
{
    protected override void Build()
    {
        Struct("TriggerVolumeDef", 136,
            Field("onEnter", DataType.Key, 12),
            Field("onExit", DataType.Key, 28),
            Field("onStay", DataType.Key, 44),
            NStruct("events", "TriggerVolumeEvents", 48),
            Field("useGameObjectDimensions", DataType.Bool, 52),
            Field("isKinematic", DataType.Bool, 53),
            EnumField("shape", "TriggerShape", 56),
            IStruct("offset", "cSPVector3", 60),
            Field("timeToActivate", DataType.Float, 72),
            Field("persistentTimer", DataType.Bool, 76),
            Field("triggerOnceOnly", DataType.Bool, 77),
            Field("triggerIfNotBeaten", DataType.Bool, 78),
            EnumField("triggerActivationType", "TriggerActivationType", 80),
            Field("luaCallbackOnEnter", DataType.CharPtr, 84),
            Field("luaCallbackOnExit", DataType.CharPtr, 88),
            Field("luaCallbackOnStay", DataType.CharPtr, 92),
            Field("boxWidth", DataType.Float, 96),
            Field("boxLength", DataType.Float, 100),
            Field("boxHeight", DataType.Float, 104),
            Field("sphereRadius", DataType.Float, 108),
            Field("capsuleHeight", DataType.Float, 112),
            Field("capsuleRadius", DataType.Float, 116),
            Field("serverOnly", DataType.Bool, 120)
        );
    }
}
