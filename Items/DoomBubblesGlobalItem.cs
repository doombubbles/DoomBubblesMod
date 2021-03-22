using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
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

            foreach (var kvp in itemNameOverrides)
            {
                var itemId = kvp.Key;
                var replacementName = kvp.Value;
                if (item.Name == itemId)
                {
                    item.SetNameOverride(replacementName);
                }
            }

            if (item.type == ItemID.SpikyBall)
            {
                item.ammo = item.type;
            }
        }

        public override bool CanUseItem(Item item, Player player)
        {
            if (player.GetModPlayer<ThanosPlayer>().InfinityGauntlet && DoomBubblesMod.powerStoneHotKey.Current)
            {
                return false;
            }

            return base.CanUseItem(item, player);
        }

        public override void GetWeaponCrit(Item item, Player player, ref int crit)
        {
            crit = (int) (crit * player.GetModPlayer<DoomBubblesPlayer>().critChanceMult);
            base.GetWeaponCrit(item, player, ref crit);
        }

        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            if ((item.type == ItemID.ExplodingBullet || item.Name == "Endless Explosive Pouch") &&
                player.GetModPlayer<DoomBubblesPlayer>().explosionBulletBonus)
            {
                mult *= 2;
            }

            base.ModifyWeaponDamage(item, player, ref add, ref mult, ref flat);
        }

        public override void OpenVanillaBag(string context, Player player, int arg)
        {
            if (arg == ItemID.PlanteraBossBag && context == "bossBag")
            {
                player.QuickSpawnItem(mod.ItemType("HeartOfTerraria"));
            }

            if (arg == ItemID.FishronBossBag && context == "bossBag")
            {
                if (Main.rand.Next(1, 5) == 1)
                {
                    player.QuickSpawnItem(mod.ItemType("Ultrashark"));
                }
            }

            base.OpenVanillaBag(context, player, arg);
        }

        public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
        {
            if (player.GetModPlayer<DoomBubblesPlayer>().noManaItems.Contains(item.type))
            {
                mult = 0f;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.GravityGlobe)
            {
                tooltips.Add(new TooltipLine(mod, "Secret", "Now has certain special properties...")
                {
                    overrideColor = Color.MediumPurple
                });
                tooltips.Add(new TooltipLine(mod, "Secret2", "  -doombubbles")
                {
                    overrideColor = Color.MediumPurple
                });
            }

            if (item.owner != 255 && item.owner != -1 && Main.player[item.owner].GetModPlayer<DoomBubblesPlayer>()
                .noManaItems.Contains(item.type))
            {
                for (var i = 0; i < tooltips.Count; i++)
                {
                    if (tooltips[i].Name == "UseMana" && tooltips[i].mod.Equals("Terraria"))
                    {
                        tooltips.RemoveAt(i);
                        i--;
                    }
                }
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

        public override bool CanEquipAccessory(Item item, Player player, int slot)
        {
            if (item.Name.Contains("Emblem") && player.GetModPlayer<DoomBubblesPlayer>().emblem == -1)
            {
                return false;
            }

            return base.CanEquipAccessory(item, player, slot);
        }

    }
}