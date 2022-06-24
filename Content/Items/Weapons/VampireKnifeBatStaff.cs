using System.Collections.Generic;
using DoomBubblesMod.Content.Projectiles.Summon;

namespace DoomBubblesMod.Content.Items.Weapons;

public class VampireKnifeBatStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Vampire Knife Bat Staff");
        Tooltip.SetDefault("Summons Bats that shoot Vampire Knives as you do");
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 38;
        Item.height = 38;
        Item.DamageType = DamageClass.Summon;
        Item.mana = 10;
        Item.useTime = 32;
        Item.useAnimation = 32;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.noMelee = true;
        Item.knockBack = 3;
        Item.value = Item.buyPrice(0, 30);
        Item.rare = ItemRarityID.Yellow;
        Item.damage = 29;
        Item.UseSound = SoundID.Item44;
        Item.shoot = ProjectileType<VampireKnifeBat>();
        Item.shootSpeed = 10f;
        Item.buffType = BuffType<Buffs.VampireKnifeBat>();
        Item.buffTime = 3600;
    }

    public override bool AltFunctionUse(Player player)
    {
        return true;
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type,
        int damage, float knockback)
    {
        return player.altFunctionUse != 2;
    }

    public override bool? UseItem(Player player)
    {
        if (player.altFunctionUse == 2)
        {
            player.MinionNPCTargetAim(false);
        }

        return base.UseItem(player);
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        for (var i = 0; i < tooltips.Count; i++)
        {
            if (tooltips[i].Name == "BuffTime")
            {
                tooltips.RemoveAt(i);
                i--;
            }
        }

        base.ModifyTooltips(tooltips);
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BatScepter);
        recipe.AddIngredient(ItemID.VampireKnives);
        recipe.AddIngredient(ItemID.BrokenBatWing, 2);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
    }
}