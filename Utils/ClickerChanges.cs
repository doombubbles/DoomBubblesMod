using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClickerClass;
using ClickerClass.Items;
using ClickerClass.Prefixes.ClickerPrefixes;
using ClickerClass.Utilities;
using DoomBubblesMod.Common.Configs;

namespace DoomBubblesMod.Utils;

[ExtendsFromMod("ClickerClass"), JITWhenModsEnabled("ClickerClass")]
public class ClickerChangesGlobalItem : GlobalItem
{
    #region Clickers as Accessories

    private bool equippedInAccessories;

    public override bool InstancePerEntity => true;

    public override bool AppliesToEntity(Item entity, bool lateInstantiation) =>
        GetInstance<ServerConfig>().ClickerAccessories && ClickerSystem.IsClickerWeapon(entity);

    public override void SetDefaults(Item item)
    {
        item.accessory = true;
        item.hasVanityEffects = true;
    }

    public override bool AltFunctionUse(Item item, Player player) => GetInstance<ServerConfig>().ClickerRightClick;

    public override void UpdateInventory(Item item, Player player)
    {
        equippedInAccessories = false;
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        equippedInAccessories = true;

        var clickerPlayer = player.GetClickerPlayer();
        clickerPlayer.EnableClickEffect(item.GetGlobalItem<ClickerItemCore>().itemClickEffects);

        var prefix = item.ModPrefix();
        switch (prefix)
        {
            case Elite or Amateur:
                clickerPlayer.clickerRadius += .3f;
                clickerPlayer.clickerBonus++;
                break;
            case Pro or Novice:
                clickerPlayer.clickerRadius += .2f;
                break;
            case Laggy:
                clickerPlayer.clickerRadius -= .2f;
                break;
            case Disconnected:
                clickerPlayer.clickerRadius -= .3f;
                clickerPlayer.clickerBonus--;
                break;
        }
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (tooltips.GetLine(TooltipPlacement.Equipable) is TooltipLine equipable)
        {
            equipable.Text = "Equipable to gain Click Effect";
        }

        var clickerItem = item.GetGlobalItem<ClickerItemCore>();
        var clickerPlayer = Main.LocalPlayer.GetClickerPlayer();

        var ownEffects = clickerItem.itemClickEffects.ToDictionary(s => s,
            s => ClickerSystem.IsClickEffect(s, out var effect) ? "ClickEffect_" + effect.UniqueName : "");
        if (equippedInAccessories)
        {
            tooltips.RemoveLine(TooltipPlacement.Damage);
            tooltips.RemoveLine(TooltipPlacement.CritChance);
            tooltips.RemoveLine(TooltipPlacement.Speed);
            tooltips.RemoveLine(TooltipPlacement.Knockback);
            tooltips.RemoveLine(TooltipPlacement.PrefixDamage);
            tooltips.RemoveLine(TooltipPlacement.PrefixCritChance);

            tooltips.RemoveAll(line => line.Name.StartsWith("ClickEffect_") && !ownEffects.ContainsValue(line.Name));

            foreach (var (effect, tooltipName) in ownEffects)
            {
                var line = tooltips.FirstOrDefault(line => line.Name == tooltipName);
                if (line != null)
                {
                    var realTotal = clickerPlayer.GetClickAmountTotal(new ClickerItemCore(), effect);
                    line.Text = Regex.Replace(line.Text, @"\d+ clicks:", $"{realTotal} clicks:");
                }
            }
        }
    }

    #endregion

    #region Clicker Right Clicks

    public override void Load()
    {
        Terraria.On_Player.KeyDoubleTap += OnPlayerOnKeyDoubleTap;
        Terraria.On_Player.ItemCheck_CheckCanUse += PlayerOnItemCheck_CheckCanUse;
    }

    private static bool PlayerOnItemCheck_CheckCanUse(Terraria.On_Player.orig_ItemCheck_CheckCanUse orig, Player player,
        Item item)
    {
        if (ClickerSystem.IsClickerWeapon(player.HeldItem) && GetInstance<ServerConfig>().ClickerRightClick)
        {
            player.altFunctionUse = 0;
        }

        return orig(player, item);
    }

    private static void OnPlayerOnKeyDoubleTap(Terraria.On_Player.orig_KeyDoubleTap orig, Player player, int dir)
    {
        orig(player, dir);
        if (dir == (Main.ReversedUpDownArmorSetBonuses ? 1 : 0) &&
            GetInstance<ServerConfig>().ClickerRightClick &&
            ClickerSystem.IsClickerWeapon(player.HeldItem, out var clickerItem))
        {
            var altFunctionUse = player.altFunctionUse;
            player.altFunctionUse = 2;
            clickerItem.UseItem(player.HeldItem, player);
            player.altFunctionUse = altFunctionUse;
        }
    }

    #endregion
}

[ExtendsFromMod("ClickerClass"), JITWhenModsEnabled("ClickerClass")]
public class ClickerChangesPlayer : ModPlayer
{
    public override void UpdateEquips()
    {
        if (!ClickerClass.ClickerClass.AimAssistKey.GetAssignedKeys().Any())
        {
            Player.GetClickerPlayer().accAimbotModule2Toggle = Main.SmartCursorWanted;
        }
    }
}