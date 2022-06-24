using System.Collections.Generic;
using DoomBubblesMod.Common.Configs;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.Weapons;

namespace DoomBubblesMod.Common.GlobalItems;

public class DoomBubblesGlobalItem : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        var itemNameOverrides = new Dictionary<string, string>
        {
            {"Avenger Emblem", "Avengers Emblem"},
            {"Celestial Fragment", "Heavenly Fragment"},
            {"Celestial Crown", "Heavenly Crown"},
            {"Celestial Vestment", "Heavenly Vestment"},
            {"Celestial Leggings", "Heavenly Leggings"},
            {"Celestial Carrier", "Heavenly Carrier"},
            {"Celestial Burst Staff", "Heavenly Burst Staff"},
            {"Waifu in a Bottle", "Weeaboo in a Bottle"},
            {"Rare Waifu in a Bottle", "Rare Weeaboo in a Bottle"},
            {"Twin's Ire", "Twin's Aiur"},
            {"Crystal Ball", "Krystal Ball"},
            {"Omega Core", "Varp Kore"}
        };

        foreach (var (itemId, replacementName) in itemNameOverrides)
        {
            if (item.Name == itemId)
            {
                item.SetNameOverride(replacementName);
            }
        }

        if (item.accessory)
        {
            item.canBePlacedInVanityRegardlessOfConditions = true;
        }

        if (item.type is ItemID.BlueDynastyShingles or ItemID.RedDynastyShingles or ItemID.DynastyWood)
        {
            item.value /= 5;
        }
    }

    public override bool CanUseItem(Item item, Player player)
    {
        if (player.GetModPlayer<ThanosPlayer>().infinityGauntlet != null && PowerStoneHotKey.Current)
        {
            return false;
        }

        return base.CanUseItem(item, player);
    }

    public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
    {
        crit = (int) (crit * player.GetModPlayer<DoomBubblesPlayer>().critChanceMult);
        base.ModifyWeaponCrit(item, player, ref crit);
    }

    public override void OpenVanillaBag(string context, Player player, int arg)
    {
        if (arg == ItemID.FishronBossBag && context == "bossBag")
        {
            if (Main.rand.Next(1, 5) == 1)
            {
                player.QuickSpawnItem(new EntitySource_ItemOpen(player, ItemID.FishronBossBag), ItemType<Ultrashark>());
            }
        }

        base.OpenVanillaBag(context, player, arg);
    }

    public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
    {
        if (player.GetModPlayer<DoomBubblesPlayer>().NoManaItems.Contains(item.type))
        {
            mult = 0f;
        }
    }

    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (item.type == ItemID.GravityGlobe)
        {
            tooltips.Add(new TooltipLine(Mod, "Secret", "Now has certain special properties...")
            {
                OverrideColor = Color.MediumPurple
            });
            tooltips.Add(new TooltipLine(Mod, "Secret2", "  -doombubbles")
            {
                OverrideColor = Color.MediumPurple
            });
        }

        if (!Main.gameMenu &&
            Main.LocalPlayer
                .GetModPlayer<DoomBubblesPlayer>()
                .NoManaItems.Contains(item.type))
        {
            for (var i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].Name == "UseMana" && tooltips[i].Mod.Equals("Terraria"))
                {
                    tooltips.RemoveAt(i);
                    i--;
                }
            }
        }

        if (item.canBePlacedInVanityRegardlessOfConditions)
        {
            tooltips.RemoveAll(line => line.Name == "VanityLegal");
        }

        if (GetInstance<ClientConfig>().FixAlchemistNPCGrammar)
        {
            tooltips.ForEach(line => line.Text = line.Text.Replace("are providing", "provide a"));
            tooltips.ForEach(line => line.Text = line.Text.Replace("Buffs duration is", "Buffs' durations are"));
            tooltips.ForEach(line => line.Text = line.Text.Replace("Allows to use", "Allows you to use"));
            tooltips.ForEach(line => line.Text = line.Text.Replace("not to consume potion", "to not consume potions"));
            tooltips.ForEach(line => line.Text = line.Text.Replace("have better chance", "have a better chance"));
        }
    }

    public override void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
        if (item.Name.Contains("Emblem") && item.Name != "Aquatic Emblem")
        {
            player.GetModPlayer<DoomBubblesPlayer>().emblem++;
        }

        base.UpdateAccessory(item, player, hideVisual);
    }

    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        if (item.Name.Contains("Emblem") && player.GetModPlayer<DoomBubblesPlayer>().emblem < 0)
        {
            return false;
        }

        return base.CanEquipAccessory(item, player, slot, modded);
    }
}