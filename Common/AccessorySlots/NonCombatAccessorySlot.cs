using System.Collections.Generic;
using System.Linq;
using DoomBubblesMod.Common.Configs;

namespace DoomBubblesMod.Common.AccessorySlots;

public abstract class NonCombatAccessorySlot : ModAccessorySlot
{
    public override string FunctionalTexture => "DoomBubblesMod/Assets/Textures/UI/Toolbelt";

    public override bool CanAcceptItem(Item checkItem, AccessorySlotType context) => context switch
    {
        AccessorySlotType.FunctionalSlot => AppliesTo(checkItem),
        AccessorySlotType.VanitySlot => AppliesTo(checkItem),
        _ => true
    };

    public static bool AppliesTo(Item item) =>
        item.vanity || GetInstance<ServerConfig>().NonCombatAccessories.Any(definition => definition.Type == item.type);

    public override bool ModifyDefaultSwapSlot(Item item, int accSlotToSwapTo) => AppliesTo(item);

    public override void OnMouseHover(AccessorySlotType context) => Main.hoverItemName = context switch
    {
        AccessorySlotType.FunctionalSlot => "Non-Combat Accessory",
        AccessorySlotType.VanitySlot => "Social Non-Combat Accessory",
        _ => Main.hoverItemName
    };

    public override void ApplyEquipEffects()
    {
        var prefix = FunctionalItem.prefix;
        FunctionalItem.SetDefaults(FunctionalItem.type);
        base.ApplyEquipEffects();
        FunctionalItem.Prefix(prefix);
    }

    public static IEnumerable<int> AllDefaultItems => Fishing.Concat(Misc).Concat(Construction).Concat(Informational)
        .Concat(MusicBoxes).Concat(GolfBalls).Concat(AllowedMovementAccessories);

    private static readonly int[] Informational =
    [
        ItemID.CopperWatch, ItemID.TinWatch, ItemID.SilverWatch, ItemID.TungstenWatch, ItemID.GoldWatch,
        ItemID.PlatinumWatch, ItemID.DepthMeter, ItemID.Compass, ItemID.Radar, ItemID.LifeformAnalyzer,
        ItemID.TallyCounter, ItemID.MetalDetector, ItemID.Stopwatch, ItemID.DPSMeter, ItemID.FishermansGuide,
        ItemID.WeatherRadio, ItemID.Sextant, ItemID.GPS, ItemID.CopperWatch, ItemID.REK, ItemID.GoblinTech,
        ItemID.FishFinder, ItemID.PDA, ItemID.MechanicalLens, ItemID.LaserRuler
    ];

    private static readonly int[] Construction =
    [
        ItemID.Toolbelt, ItemID.Toolbox, ItemID.PaintSprayer, ItemID.ExtendoGrip, ItemID.PortableCementMixer,
        ItemID.BrickLayer, ItemID.ArchitectGizmoPack, ItemID.ActuationAccessory, ItemID.AncientChisel
    ];

    private static readonly int[] Fishing =
    [
        ItemID.HighTestFishingLine, ItemID.AnglerEarring, ItemID.TackleBox, ItemID.AnglerTackleBag,
        ItemID.LavaFishingHook, ItemID.LavaproofTackleBag
    ];

    private static readonly int[] Misc =
    [
        ItemID.ClothierVoodooDoll, ItemID.CoinRing, ItemID.DiscountCard, ItemID.FlowerBoots, ItemID.GoldRing,
        ItemID.GreedyRing, ItemID.CordageGuide, ItemID.GuideVoodooDoll, ItemID.JellyfishNecklace, ItemID.LuckyCoin,
        ItemID.DontStarveShaderItem, ItemID.SpectreGoggles, ItemID.TreasureMagnet
    ];

    private static readonly int[] AllowedMovementAccessories =
    [
        ItemID.ArcticDivingGear, ItemID.ClimbingClaws, ItemID.DivingGear, ItemID.Flipper, ItemID.FlameWakerBoots,
        ItemID.IceSkates, ItemID.FloatingTube, ItemID.JellyfishDivingGear, ItemID.LavaCharm, ItemID.MoltenCharm,
        ItemID.NeptunesShell, ItemID.ObsidianWaterWalkingBoots, ItemID.ShoeSpikes, ItemID.PortableStool, 
        ItemID.TigerClimbingGear
    ];

    public static IEnumerable<int> MusicBoxes =>
        ItemID.Search.Names
            .Where(s => s.StartsWith("MusicBox"))
            .Select(s => ItemID.Search.GetId(s));

    public static IEnumerable<int> GolfBalls =>
        ItemID.Search.Names
            .Where(s => s.StartsWith("GolfBall"))
            .Select(s => ItemID.Search.GetId(s));
}

public class NonCombatAccessorySlot1 : NonCombatAccessorySlot
{
    public override bool IsEnabled() => true;
}

public class NonCombatAccessorySlot2 : NonCombatAccessorySlot
{
    public override bool IsEnabled() => Player.extraAccessory && Main.expertMode;
}