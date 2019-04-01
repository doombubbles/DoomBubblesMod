using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Weapons
{
	public class Frostmourne : ModItem
	{
		public override void SetDefaults()
		{
            item.damage = 100;
			item.melee = true;
			item.width = 68;
			item.height = 68;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 640000;
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.useTurn = false;
            item.shootSpeed = 10f;
            projOnSwing = true;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frostmourne");
            Tooltip.SetDefault("'Whomsoever takes up this blade shall wield power eternal.'");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Frostbrand);
            recipe.AddIngredient(ItemID.FrostCore, 2);
            recipe.AddIngredient(ItemID.SoulofFright, 20);
            recipe.AddIngredient(mod.ItemType("RunicEssence"), 15);
            recipe.AddTile(TileID.IceMachine);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (player.GetModPlayer<DoomBubblesPlayer>().frostmourne >= 100)
            {
                return;
            }
            
            target.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).frostmournedmg += Math.Min(damage, target.life);
            if (Main.netMode == 1)
            {
                ModPacket packet = mod.GetPacket();
                packet.Write((byte)DoomBubblesModMessageType.frostmournedmg);
                packet.Write(target.whoAmI);
                packet.Write(target.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).frostmournedmg);
                packet.Send();
            }
            /*
            if (target.life - damage <= 0 && target.SpawnedFromStatue == false && player.GetModPlayer<DoomBubblesPlayer>(mod).frostmourne < 100)
            {
                int i = target.lifeMax;
                while (i > 100)
                {
                    Item.NewItem((int)target.Center.X, (int)target.Center.Y, target.width, target.height, mod.ItemType("Spirit"));
                    i = i - 100;
                }
                if (Main.rand.Next(0, 100) <= target.lifeMax)
                {
                    Item.NewItem((int)target.Center.X, (int)target.Center.Y, target.width, target.height, mod.ItemType("Spirit"));
                }   
            }
            */
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.player[Main.myPlayer].GetModPlayer<DoomBubblesPlayer>(mod).frostmourne >= 100 && player.itemTime == item.useTime)
            {
                for (int i = 1; i <= 3; i++)
                {
                    float posX = player.position.X + Main.rand.Next(-200, 200);
                    float posY = player.position.Y - 600;

                    float mouseX = (Main.mouseX - Main.screenWidth / 2) + Main.player[Main.myPlayer].position.X;
                    float mouseY = (Main.mouseY - Main.screenHeight / 2) + Main.player[Main.myPlayer].position.Y;
                

                    float numX = posX - mouseX + Main.rand.Next(-50, 50);
                    float numY = posY - mouseY;

                    double angle = -Math.Atan2(numX, numY) - Math.PI / 2;

                    Projectile.NewProjectile(posX, posY, 20 * (float)Math.Cos(angle), 20 * (float)Math.Sin(angle), mod.ProjectileType("Etonmourne"), item.damage, item.knockBack, player.whoAmI);
                }
            }
            else if (Main.player[Main.myPlayer].GetModPlayer<DoomBubblesPlayer>(mod).frostmourne >= 75 && player.itemTime == item.useTime)
            {
                for (int i = 1; i <= 3; i++)
                {
                    float posX = player.position.X + Main.rand.Next(-200, 200);
                    float posY = player.position.Y - 600;

                    float mouseX = (Main.mouseX - Main.screenWidth / 2) + Main.player[Main.myPlayer].position.X;
                    float mouseY = (Main.mouseY - Main.screenHeight / 2) + Main.player[Main.myPlayer].position.Y;


                    float numX = posX - mouseX + Main.rand.Next(-50, 50);
                    float numY = posY - mouseY;

                    double angle = -Math.Atan2(numX, numY) - Math.PI / 2;

                    Projectile.NewProjectile(posX, posY, 20 * (float)Math.Cos(angle), 20 * (float)Math.Sin(angle), mod.ProjectileType("Skelmourne"), item.damage, item.knockBack, player.whoAmI);
                }
            }
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            item.damage = 100 + Main.player[Main.myPlayer].GetModPlayer<DoomBubblesPlayer>(mod).frostmourne;
            if (Main.player[Main.myPlayer].GetModPlayer<DoomBubblesPlayer>(mod).frostmourne >= 100)
            {
                item.shootSpeed = 16f;
                item.shoot = mod.ProjectileType("Scaremourne");
                item.useTime = 18;
                item.useAnimation = 18;
                projOnSwing = false;
            }
            else if (Main.player[Main.myPlayer].GetModPlayer<DoomBubblesPlayer>(mod).frostmourne >= 75)
            {
                item.shootSpeed = 16f;
                item.shoot = mod.ProjectileType("Scaremourne");
            }
            else if (Main.player[Main.myPlayer].GetModPlayer<DoomBubblesPlayer>(mod).frostmourne >= 50)
            {
                item.shoot = mod.ProjectileType("Scaremourne");
            }
            else if (Main.player[Main.myPlayer].GetModPlayer<DoomBubblesPlayer>(mod).frostmourne >= 25)
            {
                item.shoot = mod.ProjectileType("Spookmourne");
            }
            
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

    }
}
