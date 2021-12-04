using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DoomBubblesMod.Common.Players;
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


    public static void AttackSpeed(this Player player, Func<float, float> func)
    {
        SetThoriumProperty(player, "attackSpeed", func);
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

    public static int ToInt(this float f)
    {
        return (int) Math.Round(f);
    }


    public static void Deconstruct<T1, T2>(this KeyValuePair<T1, T2> dic, out T1 first, out T2 second)
    {
        first = dic.Key;
        second = dic.Value;
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
        var customPacket = mod.GetContent<CustomPacket>().FirstOrDefault(packet => packet.Type == type);
        if (customPacket != null)
        {
            customPacket.Receive(reader, whoAmI);
        }
        else
        {
            mod.Logger.Warn($"Couldn't find ModPacket with type {type}");
        }
    }
}