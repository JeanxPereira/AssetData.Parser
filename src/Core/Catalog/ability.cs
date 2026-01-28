namespace ReCap.Parser.Catalog;

/// <summary>
/// Ability definition - creature/player abilities.
/// Size: 488 bytes (0x1E8)
/// </summary>
public sealed class AbilityCatalog : AssetCatalog
{
    protected override void Build()
    {
        Struct("ability", 488,
            // Script namespace
            CharBuffer("namespace", 0x50, 10),
            Bool("requiresAgent", 0x5a),
            Int("allowsMovement", 0x5c),
            
            // Stack settings
            Int("maxStackCount", 0x130),
            
            // Behavior flags
            Bool("noGlobalCooldown", 0x150),
            Bool("shouldPursue", 0x151),
            Bool("finishOnDeath", 0x152),
            Bool("alwaysUseCursorPos", 0x153),
            Bool("channelled", 0x154),
            Bool("showChannelBar", 0x155),
            UInt32("minimumChannelTimeMs", 0x158),
            Bool("faceTargetOnCreate", 0x15c),
            
            // Damage/healing info  
            UInt32("descriptors", 0x160),
            UInt32("debuffDescriptors", 0x164),
            UInt32("damageType", 0x168),
            UInt32("damageSource", 0x16c),
            Float("damageCoefficient", 0x170),
            Float("healingCoefficient", 0x174),
            Int("modifierPriority", 0x178),
            Bool("deactivateOnInterrupt", 0x17c),
            
            // Status effects
            Key("statusAnimation", 0x180),
            Asset("statusHeadEffect", 0x184),
            Asset("statusBodyEffect", 0x188),
            Asset("statusFootEffect", 0x18c),
            
            // Types
            Int("activationType", 0x190),
            Int("deactivationType", 0x194),
            Int("interfaceType", 0x198),
            Int("cooldownType", 0x19c),
            Int("targetType", 0x1a0),
            
            // UI & localization
            Key("reticleEffect", 0x74),
            Key("icon", 0x78),
            Key("localizedGroup", 0x7c),
            Key("localizedName", 0x80),
            Key("localizedDescription", 0x84),
            Key("localizedShortDescription", 0xa4),
            Key("localizedOverdriveDescription", 0xa8),
            Key("lootGroup", 0xac),
            Key("lootDescription", 0xb0),
            
            // Cooldowns
            Float("cooldown", 0xd0),
            Float("cooldownVariance", 0xf0),
            Float("range", 0x110),
            
            // Events & properties
            Int("handledEvents", 0x1a4),
            // IStruct("properties", "tAssetPropertyVector", 0x1a8), // TODO: 16 bytes
            
            // Scaling
            Int("scalingAttribute", 0x1b8),
            Int("primaryAttributeStat", 0x1bc),
            Key("primaryAttributeStatDelegate", 0x1c0),
            Float("manaCoefficient", 0x1c4),
            
            // Persistence
            Bool("saveOnDehydrate", 0x1e4),
            Bool("rejectable", 0x1e5)
        );
    }   
}
