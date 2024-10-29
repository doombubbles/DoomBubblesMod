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

    [DefaultValue(true)]
    [Label("Disable Damage Variance")]
    [Tooltip("Disables the default +/- 15% usually applied to all damage")]
    public bool DisableDamageVariance;

    [DefaultValue(true)] [Label("Automatic Torch God's Favor")] [Tooltip("Self explanatory")]
    public bool AutoTorchGod;

    [DefaultValue(true)]
    [Label("Proportionate Lucky Coin")]
    [Tooltip("Makes the Lucky Coin effect give money proportionate to the damage dealt.")]
    public bool ProportionateLuckyCoin;

    [DefaultValue(false)]
    [Label("Sorcerer's Stone Effect Affects Health Too")]
    [Tooltip(
        "Toggle's whether the Sorcerer's Stone and other items that inherit its effect will improve both life and mana regen, or just mana regen.")]
    public bool SorcerersStoneOP;

    [DefaultValue(true)]
    [Label("Clicker Accessories")]
    [Tooltip(
        "If playing with the Clicker Class mod, lets you use Clickers also as accessories to gain their click effect.")]
    [ReloadRequired]
    public bool ClickerAccessories;

    [DefaultValue(true)]
    [Label("Clicker Right Click")]
    [Tooltip(
        "If playing with the Clicker Class mod, lets you also use Right Click to activate Clickers.\nMakes Motherboard and Mice armor effects activate like Vortex armor.")]
    [ReloadRequired]
    public bool ClickerRightClick;


    [Label("Non-Combat Accessories")]
    [Tooltip("The list of accessories that will be allowed for Non-Combat Accessory slots.")]
    public List<ItemDefinition> NonCombatAccessories =
        NonCombatAccessorySlot.AllDefaultItems.Select(i => new ItemDefinition(i)).ToList();

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