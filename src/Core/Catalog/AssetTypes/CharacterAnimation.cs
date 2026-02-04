namespace AssetData.Parser.Catalog;

public sealed class CharacterAnimation : AssetCatalog
{
    protected override void Build()
    {
        Struct("CharacterAnimation", 0x2a4,
            Field("gaitOverlay", DataType.UInt32, 0x50),
            Field("ignoreGait", DataType.Bool, 0x54),
            Field("morphology", DataType.Key, 0x64),
            Field("preAggroIdleAnimState", DataType.Key, 0x74),
            Field("idleAnimState", DataType.Key, 0x84),
            Field("lobbyIdleAnimState", DataType.Key, 0x94),
            Field("specialIdleAnimState", DataType.Key, 0xa4),
            Field("walkStopState", DataType.Key, 0xb4),
            Field("victoryIdleAnimState", DataType.Key, 0xc4),
            Field("combatIdleAnimState", DataType.Key, 0xd4),
            Field("moveAnimState", DataType.Key, 0xe4),
            Field("combatMoveAnimState", DataType.Key, 0x50),
            Field("deathAnimState", DataType.Key, 0x104),
            Field("aggroAnimState", DataType.Key, 0x114),
            Field("aggroAnimDuration", DataType.Float, 0x118),
            Field("subsequentAggroAnimState", DataType.Key, 0x128),
            Field("subsequentAggroAnimDuration", DataType.Float, 0x12c),
            Field("enterPassiveIdleAnimState", DataType.Key, 0x13c),
            Field("enterPassiveIdleAnimDuration", DataType.Key, 0x140),
            Field("danceEmoteAnimState", DataType.Key, 0x150),
            Field("tauntEmoteAnimState", DataType.Key, 0x160),
            Field("meleeDeathAnimState", DataType.Key, 0x170),
            Field("meleeCritDeathAnimState", DataType.Key, 0x180),
            Field("meleeCritKnockbackDeathAnimState", DataType.Key, 0x190),
            Field("cyberCritDeathAnimState", DataType.Key, 0x1a0),
            Field("cyberCritKnockbackDeathAnimState", DataType.Key, 0x1b0),
            Field("plasmaCritDeathAnimState", DataType.Key, 0x1c0),
            Field("plasmaCritKnockbackDeathAnimState", DataType.Key, 0x1d0),
            Field("bioCritDeathAnimState", DataType.Key, 0x1e0),
            Field("bioCritKnockbackDeathAnimState", DataType.Key, 0x1f0),
            Field("necroCritDeathAnimState", DataType.Key, 0x200),
            Field("necroCritKnockbackDeathAnimState", DataType.Key, 0x210),
            Field("spacetimeCritDeathAnimState", DataType.Key, 0x220),
            Field("spacetimeCritKnockbackDeathAnimState", DataType.Key, 0x230),
            Field("bodyFadeAnimState", DataType.Key, 0x240),
            Field("randomAbility1AnimState", DataType.Key, 0x250),
            Field("randomAbility2AnimState", DataType.Key, 0x260),
            Field("randomAbility3AnimState", DataType.Key, 0x270),
            Field("overlay1AnimState", DataType.Key, 0x280),
            Field("overlay2AnimState", DataType.Key, 0x290),
            Field("overlay3AnimState", DataType.Key, 0x2a0)
        );
    }
}