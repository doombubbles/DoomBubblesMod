using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using DoomBubblesMod.Common.AccessorySlots;
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
        new(ItemID.WormholePotion)
    ];

    public bool OnlyWormholeSicknessDuringBosses = true;

    public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref NetworkText message)
    {
        var success = false;
        try
        {
            if (ModLoader.TryGetMod("MagicStorage", out var magicStorage) &&
                magicStorage.TryFind("OperatorPlayer", out ModPlayer modPlayer))
            {
                success = (bool) modPlayer.GetType()
                    .GetField("hasOp", BindingFlags.Public | BindingFlags.Instance)!
                    .GetValue(Main.player[whoAmI].GetModPlayer(modPlayer))!;
            }

            if (Main.countsAsHostForGameplay[whoAmI])
            {
                success = true;
            }

            return success;
        }
        finally
        {
            message = NetworkText.FromKey(GetInstance<DoomBubblesMod>()
                .GetLocalizationKey(success ? "NetworkText.Success" : "NetworkText.NotAllowed"));
        }
    }

    public override bool NeedsReload(ModConfig pendingConfig) => false;
}