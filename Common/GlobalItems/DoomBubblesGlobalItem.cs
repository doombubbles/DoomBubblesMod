using System;
using System.Collections.Generic;
using System.Linq;
using DoomBubblesMod.Common.Configs;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Content.Items.Weapons;
using DoomBubblesMod.Utils;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;

namespace DoomBubblesMod.Common.GlobalItems;

public class DoomBubblesGlobalItem : GlobalItem
{
    public override void SetDefaults(Item item)
    {
        var itemNameOverrides = new Dictionary<string, string>
        {
            {"Celestial Fragment", "Heavenly Fragment"},
            {"Celestial Crown", "Heavenly Crown"},
            {"Celestial Vestment", "Heavenly Vestment"},
            {"Celestial Leggings", "Heavenly Leggings"},
            {"Celestial Carrier", "Heavenly Carrier"},
            {"Celestial Burst Staff", "Heavenly Burst Staff"},
            {"Waifu in a Bottle", "Weeaboo in a Bottle"},
            {"Rare Waifu in a Bottle", "Rare Weeaboo in a Bottle"},
            {"Omega Core", "Varp Kore"},
        };

        foreach (var (itemId, replacementName) in itemNameOverrides)
        {
            if (item.Name == itemId)
            {
                item.SetNameOverride(replacementName);
            }
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

        if (GetInstance<ServerConfig>().WormholeSicknessItems.Contains(item.type) && player.HasBuff<WormholeSickness>())
        {
            return false;
        }

        return base.CanUseItem(item, player);
    }

    public override bool? UseItem(Item item, Player player)
    {
        if (item.type == ItemID.PotionOfReturn && GetInstance<ServerConfig>().WormholeSicknessItems.Contains(item.type))
        {
            player.AddBuff(BuffType<WormholeSickness>(), player.potionDelayTime);
        }

        return base.UseItem(item, player);
    }

    public override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame,
        Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        if (Main.gameMenu ||
            Main.playerInventory ||
            GetInstance<ServerConfig>() == null ||
            !GetInstance<ServerConfig>().WormholeSicknessItems.Contains(item.type) ||
            !Main.CurrentPlayer.HasBuff<WormholeSickness>()) return;

        var buffTime = Main.CurrentPlayer.buffTime[Main.CurrentPlayer.FindBuffIndex(BuffType<WormholeSickness>())];

        var itemTexture = TextureAssets.Item[item.type].Value;
        var realScale = scale * Math.Max(itemTexture.Height, itemTexture.Width) / 32f;
        var cdTexture = TextureAssets.Cd.Value;
        var position3 = position + itemTexture.Size() * scale / 2f - cdTexture.Size() * realScale / 2f;
        var color = item.GetAlpha(drawColor) * (buffTime / (float) Main.CurrentPlayer.potionDelayTime);
        spriteBatch.Draw(cdTexture, position3, new Rectangle(0, 0, 32, 32), color, 0f, origin,
            realScale, SpriteEffects.None, 0f);
    }

    public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
    {
        crit = (int) (crit * player.GetModPlayer<DoomBubblesPlayer>().critChanceMult);
        base.ModifyWeaponCrit(item, player, ref crit);
    }

    public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
    {
        switch (item.type)
        {
            case ItemID.FishronBossBag:
            {
                foreach (var rule in itemLoot.GetDescendents<OneFromOptionsNotScaledWithLuckDropRule>())
                {
                    if (rule.dropIds.Contains(ItemID.RazorbladeTyphoon))
                    {
                        rule.dropIds = rule.dropIds.Append(ItemType<Ultrashark>()).ToArray();
                    }
                }

                break;
            }
            case ItemID.LavaCrate or ItemID.LavaCrateHard:
                var oneSuccessDropRule = itemLoot.Get().OfType<AlwaysAtleastOneSuccessDropRule>().First();

                oneSuccessDropRule.rules = oneSuccessDropRule.rules
                    .Append(ItemDropRule.NotScalingWithLuck(ItemID.ObsidianRose, 10))
                    .ToArray();

                break;
        }
    }

    public override void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
    {
        if (player.GetModPlayer<DoomBubblesPlayer>().NoManaItems.Contains(item.type))
        {
            mult = 0f;
        }
    }

    private static readonly Dictionary<string, string> AlchemistGrammarFixes = new()
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
        {"Omega Core", "Varp Kore"},
        {"Dead Man's Sweater", "Dead Woman's Sweater"},
        {"Gender Change Potion", "Sophie's Choice"}
    };

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

        if (!Main.gameMenu && Main.LocalPlayer.GetModPlayer<DoomBubblesPlayer>().NoManaItems.Contains(item.type))
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

        if (GetInstance<ClientConfig>().FixAlchemistNPCGrammar)
        {
            foreach (var (find, replace) in AlchemistGrammarFixes)
            {
                tooltips.ForEach(line => line.Text = line.Text.Replace(find, replace));
            }
        }

        if (GetInstance<ClientConfig>().CalamityThrowingDamage)
        {
            var modName = item.ModItem?.Mod.Name ?? "";
            var changeItem = modName.Contains("Calamity") || modName == "SetBonusAccessories";
            foreach (var line in tooltips.Where(line =>
                         line.Name != "ItemName" && (changeItem || line.Mod.Contains("Calamity"))))
            {
                line.Text = line.Text
                    .Replace("rogue stealth", "stealth")
                    .Replace("Rogue stealth", "Stealth")
                    .Replace("rogue", "throwing")
                    .Replace("Rogue", "Throwing");
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

    public override bool CanEquipAccessory(Item item, Player player, int slot, bool modded)
    {
        if (item.Name.Contains("Emblem") && player.GetModPlayer<DoomBubblesPlayer>().emblem < 0)
        {
            return false;
        }

        return base.CanEquipAccessory(item, player, slot, modded);
    }

    public override bool? CanAutoReuseItem(Item item, Player player)
    {
        if (player.GetDoomBubblesPlayer().autoShoot &&
            item.DamageType.CountsAsClass(DamageClass.Ranged) &&
            item.shoot > ProjectileID.None &&
            item.damage > 0)
        {
            return true;
        }

        return null;
    }
}