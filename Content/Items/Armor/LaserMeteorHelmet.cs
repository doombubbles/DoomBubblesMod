﻿using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Armor;

[AutoloadEquip(EquipType.Head)]
public class LaserMeteorHelmet : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Laser Meteor Helmet");
        Tooltip.SetDefault("11% Increased Magic Damage");
        Item.SetResearchAmount(1);
    }

    public override void SetDefaults()
    {
        var realSlot = Item.headSlot;
        Item.CloneDefaults(ItemID.MeteorHelmet);
        Item.rare = ItemRarityID.LightRed;
        Item.value = Item.sellPrice(0, 6, 75);
        Item.defense = 10;
        Item.headSlot = realSlot;
    }

    public override bool IsArmorSet(Item head, Item body, Item legs)
    {
        return body.type == ModContent.ItemType<LaserMeteorSuit>() &&
               legs.type == ModContent.ItemType<LaserMeteorLeggings>();
    }

    public override void UpdateArmorSet(Player player)
    {
        player.setBonus = "Laser Rifle costs 0 mana";
        player.GetModPlayer<DoomBubblesPlayer>().noManaItems.Add(ItemID.LaserRifle);
    }

    public override void UpdateEquip(Player player)
    {
        player.GetDamage(DamageClass.Magic) += .11f;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.MeteorHelmet);
        recipe.AddIngredient(ItemID.CrystalShard, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}