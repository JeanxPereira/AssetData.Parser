namespace ReCap.Parser.Catalog;

public sealed class Enums : AssetCatalog
{
    protected override void Build()
    {
        Enum("NounType")
            .Value("None", 0x00000000)
            .Value("Creature", 0x06C27D00)
            .Value("Vehicle", 0x78BDDF27)
            .Value("Obstacle", 0x02ADB47A)
            .Value("SpawnPoint", 0xD9EAF104)
            .Value("PathPoint", 0x9D99F2FA)
            .Value("Trigger", 0x3CE46113)
            .Value("PointLight", 0x935555CB)
            .Value("SpotLight", 0xBCD9673B)
            .Value("LineLight", 0x3D58111D)
            .Value("ParallelLight", 0x8B1018A4)
            .Value("HemisphereLight", 0xE615AFDB)
            .Value("Animator", 0xF90527D6)
            .Value("Animated", 0xF3051E56)
            .Value("GraphicsControl", 0xE9A24895)
            .Value("Material", 0xE6640542)
            .Value("Flora", 0x5BDCEC35)
            .Value("LevelshopObject", 0xA12B2C18)
            .Value("Terrain", 0x151DB008)
            .Value("Weapon", 0xE810D505)
            .Value("Building", 0xC710B6E9)
            .Value("Handle", 0xADB0A86B)
            .Value("HealthOrb", 0x75B43FF2)
            .Value("ManaOrb", 0xF402465F)
            .Value("ResurrectOrb", 0xC035AAAD)
            .Value("Movie", 0x4927FA7F)
            .Value("Loot", 0x292FEA33)
            .Value("PlaceableEffect", 0x383A0A75)
            .Value("LuaJob", 0xA2908A12)
            .Value("AbilityObject", 0x485FC991)
            .Value("LevelExitPoint", 0x087E8047)
            .Value("Decal", 0x4D7784B8)
            .Value("Water", 0x9E3C3DFA)
            .Value("Grass", 0xFD3D2ED9)
            .Value("Door", 0x6FEDAE4D)
            .Value("Crystal", 0xCD482419)
            .Value("Interactable", 0x0977AF61)
            .Value("Projectile", 0x253F6F5C)
            .Value("DestructibleOrnament", 0x0013FBB4)
            .Value("MapCamera", 0xFBFB36D0)
            .Value("Occluder", 0x071FD3D4)
            .Value("SplineCamera", 0x6CB99FFF)
            .Value("SplineCameraNode", 0xFD487097)
            .Value("BossPortal", 0xC1B461BC);
        
        Enum("PhysicsType")
            .Value("none", 0x00fe1404)
            .Value("box", 0x00feed5c)
            .Value("sphere", 0x00fe3444)
            .Value("capsule", 0x010281dc)
            .Value("hull", 0x010281d4)
            .Value("mesh", 0x010281cc);
        
        Enum("TriggerShape")
            .Value("sphere", 0)
            .Value("box", 1)
            .Value("capsule", 2);
        
        Enum("ProjectileDef.targetType")
            .Value("enemies", 0)
            .Value("allies", 1)
            .Value("any", 2)
            .Value("none", 3);
        
        Enum("CollisionShape")
            .Value("sphere", 0)
            .Value("box", 1);
        
        Enum("cVolumeDefShape")
            .Value("game object", 0)
            .Value("sphere", 1)
            .Value("box", 2)
            .Value("capsule", 3);
        
        Enum("type")
            .Value("OnMyPlayerEnter", 0)
            .Value("OnAnyPlayerEnter", 1);
        
        Enum("targetType")
            .Value("OnMyPlayerEnter", 0)
            .Value("OnAnyPlayerEnter", 1);
        
        Enum("triggerActivationType")
            .Value("each object", 0)
            .Value("each player", 1)
            .Value("any player", 2)
            .Value("all players", 3);
            
        Enum("LocomotionType")
            .Value("default", 0)
            .Value("pathfinding", 0)
            .Value("projectile", 1)
            .Value("ballistic", 2)
            .Value("homing_projectile", 3)
            .Value("lobbed", 4)
            .Value("orbit_owner", 5)
            .Value("none", 6)
            .Value("ground_roll", 7);

        Enum("spawnTeamId")
            .Value("npc", 0)
            .Value("team 1", 1)
            .Value("team 2", 2)
            .Value("team 3", 3)
            .Value("team 4", 4)
            .Value("team 5", 5)
            .Value("team 6", 6)
            .Value("team 7", 7)
            .Value("team 8", 8);

        Enum("gfxPickMethod")
            .Value("cylinder", 0)
            .Value("creature bodies", 1)
            .Value("box", 2);

        Enum("phaseType")
            .Value("prioritizedList", 0)
            .Value("sequential", 1)
            .Value("random", 2);

        Enum("SpawnPointDef.sectionType")
            .Value("A", 0)
            .Value("B", 1)
            .Value("C", 2)
            .Value("Any", 3);

        Enum("presetExtents")
            .Value("none", 0)
            .Value("critter vertical", 1)
            .Value("critter horizontal", 2)
            .Value("minion vertical", 3)
            .Value("minion horizontal", 4)
            .Value("elite minion vertical", 5)
            .Value("elite minion horizontal", 6)
            .Value("player tempest", 7)
            .Value("player ravager", 8)
            .Value("player sentinel", 9)
            .Value("lieutenant vertical", 10)
            .Value("lieutenant horizontal", 11)
            .Value("elite lieutenant vertical", 12)
            .Value("elite lieutenant horizontal", 13)
            .Value("captain vertical", 14)
            .Value("captain horizontal", 15)
            .Value("boss vertical", 16)
            .Value("boss horizontal", 17);

        Enum("LevelType")
            .Value("plasma", 3)
            .Value("necro", 4)
            .Value("bio", 2)
            .Value("cyber", 0)
            .Value("chrono", 1)
            .Value("generic", 5);

        Enum("Markerset.condition")
            .Value("hordeLevelsOnly", 0)
            .Value("bossLevelsOnly", 1);

        Enum("cLabsMarker.navMeshSetting")
            .Value("from definition", 0)
            .Value("include", 1)
            .Value("exclude", 2);

        Enum("cLabsMarker.type")
            .Value("global0", 0)
            .Value("global1", 1)
            .Value("global2", 2)
            .Value("local0", 3)
            .Value("local1", 4)
            .Value("local2", 5)
            .Value("character0", 6)
            .Value("character1", 7);

        Enum("cLabsMarker.shadowed")
            .Value("not_shadowed", 0)
            .Value("yes_shadowed", 1)
            .Value("inv_shadowed", 2);

        Enum("NonPlayerClass.creatureType")
            .Value("technology", 0)
            .Value("spacetime", 1)
            .Value("life", 2)
            .Value("elements", 3)
            .Value("supernatural", 4)
            .Value("generic", 5);

        Enum("NonPlayerClass.mNPCType")
            .Value("minion", 0)
            .Value("special", 1)
            .Value("boss", 2)
            .Value("destructible", 3)
            .Value("interactable", 4)
            .Value("agent", 5)
            .Value("victim", 6)
            .Value("captain", 7);

        Enum("NonPlayerClass.dropType")
            .Value("none", 1)
            .Value("orbs", 2)
            .Value("catalysts", 4)
            .Value("loot", 8)
            .Value("dna", 10);

        Enum("cGfxComponentDef.gfxType")
            .Value("spore model", 0)
            .Value("swarm effect", 1)
            .Value("creature", 2)
            .Value("building", 3)
            .Value("vehicle", 4)
            .Value("flor", 5)
            .Value("labs model", 6);

        Enum("CrystalDef.type")
            .Value("red", 3)
            .Value("blue", 0)
            .Value("gold", 2)
            .Value("green", 4)
            .Value("wild", 1);

        Enum("CrystalDef.rarity")
            .Value("common", 0)
            .Value("rare", 1)
            .Value("epic", 2);

        Enum("cHarpointInfo.type")
            .Value("none", 0)
            .Value("bottom", 1)
            .Value("center", 2)
            .Value("top", 3)
            .Value("body part", 4)
            .Value("left side body part", 5)
            .Value("right side body part", 6)
            .Value("local offset", 7)
            .Value("rightmost body part", 8);

        Enum("cHardpointInfo.bodyCap")
            .Value("base", 0x0)
            .Value("root", 0x746F6F72)
            .Value("limb", 0x626D696C)
            .Value("foot", 0x746F6F66)
            .Value("spine", 0x6E697073)
            .Value("pseudoFoot", 0x74667370)
            .Value("mouth", 0x74756F6D)
            .Value("grasper", 0x70737267)
            .Value("noStretch", 0x7274736E)
            .Value("weapon", 0x7772636C)
            .Value("eye", 0x00657965);
            // .Value("ear", 0x00726165)
    }
}
