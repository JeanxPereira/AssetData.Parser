namespace ReCap.Parser.Catalog;

public sealed class SharedComponentData : AssetCatalog
{
    protected override void Build()
    {
        Struct("SharedComponentData", 40,
            NStruct("audioTrigger", "AudioTriggerDef", 0),
            NStruct("teleporter", "TeleporterDef", 4),
            NStruct("eventListenerDef", "EventListenerDef", 8),
            NStruct("spawnPointDef", "SpawnPointDef", 16),
            NStruct("spawnTrigger", "SpawnTriggerDef", 12),
            NStruct("interactable", "InteractableDef", 20),
            NStruct("defaultGfxState", "GameObjectGfxStateTuning", 24),
            NStruct("combatant", "CombatantDef", 28),
            NStruct("triggerComponent", "TriggerVolumeComponentDef", 32),
            NStruct("spaceshipSpawnPoint", "SpaceshipSpawnPointDef", 36)
        );
    }
}