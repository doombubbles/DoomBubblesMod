using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DoomBubblesMod.Common.AccessorySlots;
using DoomBubblesMod.Utils;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace DoomBubblesMod.Common.Configs;

public class ServerConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [DefaultValue(true)] public bool DisableDamageVariance;

    [DefaultValue(true)] public bool AutoTorchGod;

    [DefaultValue(true)] public bool ProportionateLuckyCoin;

    [DefaultValue(false)] public bool SorcerersStoneOP;

    [DefaultValue(true)] [ReloadRequired] public bool ClickerAccessories;

    [DefaultValue(true)] [ReloadRequired] public bool ClickerRightClick;

    public List<ItemDefinition> NonCombatAccessories =
        NonCombatAccessorySlot.AllDefaultItems.Select(i => new ItemDefinition(i)).ToList();

    public List<ItemDefinition> WormholeSicknessItems =
    [
        new(ItemID.WormholePotion),
        new(ItemID.PotionOfReturn),
    ];

    public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
    {
        if (Main.player[whoAmI].IsServerOwner())
        {
            message = NetworkText.FromKey(GetInstance<DoomBubblesMod>().GetLocalizationKey("NetworkText.Success"));
            return true;
        }

        message = NetworkText.FromKey(GetInstance<DoomBubblesMod>().GetLocalizationKey("NetworkText.NotAllowed"));
        return false;
    }

    public override bool NeedsReload(ModConfig pendingConfig) => false;
}