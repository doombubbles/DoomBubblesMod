using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class RavenousHydra : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ravenous Hydra");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.damage = 80;
            item.melee = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 40;
            item.scale = 1.1f;
            item.useAnimation = 40;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 640000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Crescent");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AdamantiteWaraxe, 1);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectileDirect(target.Center, new Vector2(0,0), mod.ProjectileType("Crescent"), damage, knockBack, player.whoAmI, 1);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
}
