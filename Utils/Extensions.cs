using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DoomBubblesMod.Common.Players;
using ReLogic.Utilities;
using Terraria.Audio;
using Terraria.GameContent.Creative;

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
        if (DoomBubblesMod.ThoriumMod == null)
        {
            return;
        }

        var thoriumPlayer = player.GetModPlayer(DoomBubblesMod.ThoriumMod.Find<ModPlayer>("ThoriumPlayer"));

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
        if (DoomBubblesMod.ThoriumMod == null)
        {
            return default;
        }

        var thoriumPlayer = player.GetModPlayer(DoomBubblesMod.ThoriumMod.Find<ModPlayer>("ThoriumPlayer"));

        var fieldInfo = thoriumPlayer.GetType().GetField(name);
        if (fieldInfo == null)
        {
            return default;
        }

        return (T) fieldInfo.GetValue(thoriumPlayer);
    }

    public static void SetThoriumProperty<T>(this Projectile projectile, string name, Func<T, T> func)
    {
        if (DoomBubblesMod.ThoriumMod == null)
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
        if (DoomBubblesMod.ThoriumMod == null)
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
        if (DoomBubblesMod.ThoriumMod == null)
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
        if (DoomBubblesMod.ThoriumMod == null)
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

    public static void SetResearchAmount(this Item item, int amount)
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[item.type] = amount;
    }

    public static void HandleCustomPacket(this Mod mod, BinaryReader reader, int whoAmI)
    {
        var type = reader.ReadInt32();
        var customPacket = GetContent<CustomPacket>().FirstOrDefault(packet => packet.Type == type);
        if (customPacket != null)
        {
            customPacket.Receive(reader, whoAmI);
        }
        else
        {
            mod.Logger.Warn($"Couldn't find ModPacket with type {type}");
        }
    }

    /// <summary>
    /// Gets the element of the array at the given index, or null/default if the index is negative or out of bounds
    /// </summary>
    public static T GetOrDefault<T>(this T[] array, int index) =>
        index < 0 || index >= array.Length ? default : array[index];

    public static string SoundPath(this Mod mod, string assetPath) => $"{mod.Name}/Assets/Sounds/{assetPath}";

    public static SoundStyle Sound(this Mod mod, string assetPath) => new(mod.SoundPath(assetPath));

    public static ActiveSound GetSound(this SlotId slotId) =>
        slotId.IsValid && SoundEngine.TryGetActiveSound(slotId, out var sound) ? sound : null;

    public static SlotId Volume(this SlotId slotId, float volume)
    {
        if (slotId.IsValid && SoundEngine.TryGetActiveSound(slotId, out var sound))
        {
            sound.Volume = volume;
        }

        return slotId;
    }

    public static SlotId Volume(this SlotId slotId, double volume) => slotId.Volume((float) volume);

    private static void InsertSpecifically(this List<TooltipLine> lines, TooltipLine tooltipLine,
        Func<TooltipPlacement, bool> func)
    {
        var index = lines.FindIndex(line =>
            Enum.TryParse(line.Name, out TooltipPlacement placement) && func(placement));
        if (index == -1)
        {
            index = lines.Count;
        }

        lines.Insert(index, tooltipLine);
    }

    /// <summary>
    /// Inserts a TooltipLine at the latest position that is before the given placement or anything that comes after it
    /// </summary>
    public static void InsertBefore(this List<TooltipLine> lines, TooltipPlacement justBefore, TooltipLine tooltipLine)
        => lines.InsertSpecifically(tooltipLine, placement => placement >= justBefore);

    /// <summary>
    /// Inserts a TooltipLine at the latest position that's still before anything that comes after the given placement
    /// </summary>
    public static void InsertAfter(this List<TooltipLine> lines, TooltipPlacement justAfter, TooltipLine tooltipLine)
        => lines.InsertSpecifically(tooltipLine, placement => placement > justAfter);

    public static int GetIndex(this List<TooltipLine> lines, TooltipPlacement placement) =>
        lines.FindIndex(line => line.Name == placement.ToString());

    public static ref int FrameCount(this Projectile projectile) => ref Main.projFrames[projectile.type];

    public static Player Owner(this Projectile projectile) =>
        projectile.owner >= 255 ? null : Main.player[projectile.owner];
}