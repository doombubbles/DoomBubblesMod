using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    class SpikyBallBlaster : ModItem
    {
        public override void SetDefaults()
        {

            item.value = 100;

            item.rare = 0;
            item.scale = 0.8f;
            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;
            item.width = 58;
            item.height = 30;
            item.shoot = ProjectileID.SpikyBall;
            item.useAmmo = ItemID.SpikyBall;
            item.damage = 10;
            item.shootSpeed = 16.0f;
            item.noMelee = true;
            item.ranged = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item40;
        }

        public override void SetStaticDefaults()
        {
              DisplayName.SetDefault("Spiky Ball Blaster");
              Tooltip.SetDefault("33% chance not to consume Spiky Balls");
        }


        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-5, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpikyBall, 100);
            recipe.AddIngredient(ItemID.MeteoriteBar, 10);
            recipe.AddIngredient(ItemID.FlintlockPistol, 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool ConsumeAmmo(Player player)
        {
            if (Main.rand.Next(1,3) == 1)
            {
                return false;
            }
            return base.ConsumeAmmo(player);
            
        }
    }
}
