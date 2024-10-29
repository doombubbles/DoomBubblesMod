using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DoomBubblesMod.Common.Players;
using ReLogic.Utilities;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;

namespace DoomBubblesMod.Utils;

public static class Extensions
{
    public static void SymphonicDamage(this Player player, Func<float, float> func)
    {
        player.SetThoriumProperty("symphonicDamage", func);
    }

    public static void RadiantDamage(this Player player, Func<float, float> func)
    {
        SetThoriumProperty(player, "radiantBoost", func);
    }

    public static void SymphonicCrit(this Player player, Func<int, int> func)
    {
        SetThoriumProperty(player, "symphonicCrit", func);
    }

    public static void RadiantCrit(this Player player, Func<int, int> func)
    {
        SetThoriumProperty(player, "radiantCrit", func);
    }

    public static void SetThoriumProperty<T>(this Player player, string name, Func<T, T> func)
    {
        if (ThoriumMod == null)
        {
            return;
        }

        var thoriumPlayer = player.GetModPlayer(ThoriumMod.Find<ModPlayer>("ThoriumPlayer"));

        var fieldInfo = thoriumPlayer.GetType().GetField(name);
        if (fieldInfo == null)
        {
            return;
        }

        var value = (T) fieldInfo.GetValue(thoriumPlayer);
        value = func.Invoke(value);
        fieldInfo.SetValue(thoriumPlayer, value);
    }

    public static T GetThoriumProperty<T>(this Player player, string name)
    {
        if (ThoriumMod == null)
        {
            return default;
        }

        var thoriumPlayer = player.GetModPlayer(ThoriumMod.Find<ModPlayer>("ThoriumPlayer"));

        var fieldInfo = thoriumPlayer.GetType().GetField(name);
        if (fieldInfo == null)
        {
            return default;
        }

        return (T) fieldInfo.GetValue(thoriumPlayer);
    }

    public static void SetThoriumProperty<T>(this Projectile projectile, string name, Func<T, T> func)
    {
        if (ThoriumMod == null)
        {
            return;
        }

        var thoriumProjectile = projectile.ModProjectile;
        if (thoriumProjectile == null)
        {
            return;
        }

        var fieldInfo = thoriumProjectile.GetType().GetField(name);
        if (fieldInfo == null)
        {
            return;
        }

        var value = (T) fieldInfo.GetValue(thoriumProjectile);
        value = func.Invoke(value);
        fieldInfo.SetValue(thoriumProjectile, value);
    }

    public static T GetThoriumProperty<T>(this Projectile projectile, string name)
    {
        if (ThoriumMod == null)
        {
            return default;
        }

        var thoriumProjectile = projectile.ModProjectile;
        if (thoriumProjectile == null)
        {
            return default;
        }

        var fieldInfo = thoriumProjectile.GetType().GetField(name);
        if (fieldInfo == null)
        {
            return default;
        }

        return (T) fieldInfo.GetValue(thoriumProjectile);
    }

    public static void SetThoriumProperty<T>(this Item item, string name, Func<T, T> func)
    {
        if (ThoriumMod == null)
        {
            return;
        }

        var thoriumItem = item.ModItem;
        if (thoriumItem == null)
        {
            return;
        }

        var fieldInfo = thoriumItem.GetType().GetField(name);
        if (fieldInfo == null)
        {
            return;
        }

        var value = (T) fieldInfo.GetValue(thoriumItem);
        value = func.Invoke(value);
        fieldInfo.SetValue(thoriumItem, value);
    }

    public static T GetThoriumProperty<T>(this Item item, string name)
    {
        if (ThoriumMod == null)
        {
            return default;
        }

        var thoriumItem = item.ModItem;
        if (thoriumItem == null)
        {
            return default;
        }

        var fieldInfo = thoriumItem.GetType().GetField(name);
        if (fieldInfo == null)
        {
            return default;
        }

        return (T) fieldInfo.GetValue(thoriumItem);
    }

    public static DoomBubblesPlayer GetDoomBubblesPlayer(this Player player)
    {
        return player.GetModPlayer<DoomBubblesPlayer>();
    }

    public static string SoundPath(this Mod mod, string assetPath) => $"{mod.Name}/Assets/Sounds/{assetPath}";

    public static SoundStyle Sound(this Mod mod, string assetPath) => new(mod.SoundPath(assetPath));

    public static ActiveSound GetSound(this SlotId slotId) =>
        slotId.IsValid && SoundEngine.TryGetActiveSound(slotId, out var sound) ? sound : null;

    /*public static SlotId Volume(this SlotId slotId, float volume)
    {
        if (slotId.IsValid && SoundEngine.TryGetActiveSound(slotId, out var sound))
        {
            sound.Volume = volume;
        }

        return slotId;
    }

    public static SlotId Volume(this SlotId slotId, double volume) => slotId.Volume((float) volume);*/

    private static void InsertSpecifically(this List<TooltipLine> lines, TooltipLine tooltipLine,
        Func<TooltipPlacement, bool> func)
    {
        var index = lines.FindIndex(line =>
            Enum.TryParse(line.Name, out TooltipPlacement placement) && func(placement));
        if (index == -1)
        {
            // If all else fails, stick it at the end
            index = lines.Count;
        }

        lines.Insert(index, tooltipLine);
    }

    /// <summary>
    /// Inserts a TooltipLine directly before the specified vanilla line
    /// <br/>
    /// If the specified vanilla line isn't present in the tooltip, it will be placed where that line would've been
    /// </summary>
    public static void InsertBefore(this List<TooltipLine> lines, TooltipPlacement justBefore, TooltipLine tooltipLine)
        => lines.InsertSpecifically(tooltipLine, placement => placement >= justBefore);

    /// <summary>
    /// Inserts a TooltipLine directly after the specified vanilla line
    /// <br/>
    /// If the specified vanilla line isn't present in the tooltip, it will be placed where that line would've been
    /// </summary>
    public static void InsertAfter(this List<TooltipLine> lines, TooltipPlacement justAfter, TooltipLine tooltipLine)
        => lines.InsertSpecifically(tooltipLine, placement => placement > justAfter);

    public static int GetIndex(this List<TooltipLine> lines, TooltipPlacement placement) =>
        lines.FindIndex(line => line.Name == placement.ToString());

    public static TooltipLine GetLine(this IEnumerable<TooltipLine> lines, TooltipPlacement placement) =>
        lines.FirstOrDefault(line => line.Name == placement.ToString());

    public static TooltipLine GetTooltip(this IEnumerable<TooltipLine> lines, int index) =>
        lines.FirstOrDefault(line => line.Name == $"Tooltip{index}");

    public static ref int FrameCount(this Projectile projectile) => ref Main.projFrames[projectile.type];

    public static Player Owner(this Projectile projectile) =>
        projectile.owner >= 255 ? null : Main.player[projectile.owner];


    public static bool RemoveLine(this List<TooltipLine> lines, TooltipPlacement placement) =>
        lines.RemoveAll(line => line.Name == placement.ToString()) > 0;


    public static string ToNameFormat(this string s) => Regex.Replace(
        s,
        "(?<!^)([A-Z][a-z]|(?<=[a-z])[A-Z])",
        " $1",
        RegexOptions.Compiled).Trim();

    public static bool IsServerOwner(this Player player)
    {
        return Main.netMode == NetmodeID.MultiplayerClient
            ? Netplay.Connection.Socket.GetRemoteAddress().IsLocalHost()
            : Main.player.Any(p =>
                p == player &&
                p.active &&
                Netplay.Clients[p.whoAmI].State == 10 &&
                Netplay.Clients[p.whoAmI].Socket.GetRemoteAddress().IsLocalHost());
    }

    public static ModPrefix ModPrefix(this Item item) => PrefixLoader.GetPrefix(item.prefix);


    public static T GetDescendent<T>(this ItemLoot itemLoot) =>
        itemLoot.GetDescendents<T>().FirstOrDefault();

    public static IEnumerable<T> GetDescendents<T>(this ItemLoot itemLoot) => 
        itemLoot.Get().OfType<T>()
            .Concat(itemLoot.Get().SelectMany(itemDropRule => itemDropRule.GetDescendents<T>()));

    public static T GetDescendent<T>(this IItemDropRule dropRule) =>
        dropRule.GetDescendents<T>().FirstOrDefault();

    public static IEnumerable<T> GetDescendents<T>(this IItemDropRule dropRule) =>
        dropRule.ChainedRules.Select(attempt => attempt.RuleToChain).OfType<T>()
            .Concat(dropRule.ChainedRules.SelectMany(attempt => attempt.RuleToChain.GetDescendents<T>()));
}