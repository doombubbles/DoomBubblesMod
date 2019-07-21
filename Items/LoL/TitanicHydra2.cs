using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class TitanicHydra2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanic Hydra 2");
            Tooltip.SetDefault("Damage scales with bonus health");
        }

        public override void SetDefaults()
        {
            item.damage = 40;
            item.melee = true;
            item.width = 52;
            item.height = 52;
            item.useTime = 40;
            item.scale = 1.0f;
            item.useAnimation = 40;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 640000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Cleave");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TitaniumWaraxe, 1);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult)
        {
            add += (player.statDefense / 40f);
            base.ModifyWeaponDamage(player, ref add, ref mult);
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            Projectile.NewProjectileDirect(target.Center, new Vector2(0,0), mod.ProjectileType("Cleave"), damage, knockBack, player.whoAmI, 1);
            base.OnHitNPC(player, target, damage, knockBack, crit);
        }
    }
}
