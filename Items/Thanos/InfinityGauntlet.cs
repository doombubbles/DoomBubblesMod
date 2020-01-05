using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DoomBubblesMod.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using SoundType = Terraria.ModLoader.SoundType;


namespace DoomBubblesMod.Items.Thanos
{
    class InfinityGauntlet : ModItem
    {
        public static Color power = new Color(123, 0, 255);
        public static Color space = new Color(0, 38, 255);
        public static Color reality = new Color(150, 0, 0);
        public static Color soul = new Color(255, 106, 0);
        public static Color time = new Color(0, 200, 0);
        public static Color mind = new Color(255, 255, 0);
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Gauntlet");
            Tooltip.SetDefault("Right click to select stone\n" +
                               "\"Perfectly balanced...\n" +
                               "...as all things should be.\"\n" +
                               "-Thanos");
        }

        public override void SetDefaults()
        {
            item.value = Item.sellPrice(50, 0, 0, 0);
            item.width = 22;
            item.height = 30;
            item.rare = 11;
            item.expert = true;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 4;
            item.useTurn = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ThanosPlayer>().INfinityGauntlet = true;
            UpdateInventory(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            switch (player.GetModPlayer<ThanosPlayer>().gem)
            {
                case 0:
                    item.SetNameOverride("Infinity Gauntlet (Power)");
                    item.rare = 11;
                    item.expert = false;
                    item.channel = true;
                    break;
                case 1:
                    item.SetNameOverride("Infinity Gauntlet (Space)");
                    item.rare = 1;
                    item.expert = false;
                    item.channel = false;
                    break;
                case 2:
                    item.SetNameOverride("Infinity Gauntlet (Reality)");
                    item.rare = 10;
                    item.expert = false;
                    item.channel = true;
                    break;
                case 3:
                    item.SetNameOverride("Infinity Gauntlet (Soul)");
                    item.rare = -11;
                    item.expert = false;
                    item.channel = false;
                    break;
                case 4:
                    item.SetNameOverride("Infinity Gauntlet (Time)");
                    item.rare = 2;
                    item.expert = false;
                    item.channel = false;
                    break;
                case 5:
                    item.SetNameOverride("Infinity Gauntlet (Mind)");
                    item.rare = 8;
                    item.expert = false;
                    item.channel = false;
                    break;
                default:
                    break;
            }
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                if (item.Name.Contains("(Space)"))
                {
                    Vector2 newPos = Main.MouseWorld;
                    if (player.HasBuff(mod.BuffType("SpaceStoneCooldown")) || Collision.SolidCollision(newPos, player.width, player.height)
                                || !(newPos.X > 50f && newPos.X < Main.maxTilesX * 16 - 50 && newPos.Y > 50f && newPos.Y < Main.maxTilesY * 16 - 50))
                    {
                        return false;
                    }
                }
                if (item.Name.Contains("(Time)"))
                {
                    int previousHp = player.GetModPlayer<ThanosPlayer>().timeHealth[300];
                    if (previousHp == 0 || player.HasBuff(mod.BuffType("TimeStoneCooldown")) || previousHp <= player.statLife)
                    {
                        return false;
                    }
                }
                if (item.Name.Contains("(Mind)") && player.HasBuff(mod.BuffType("MindStoneCooldown")))
                {
                    return false;
                }
            }
            
            return base.CanUseItem(player);
        }

        public override void UseStyle(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 1;
            }
            else
            {
                item.useStyle = 4;
            }
            base.UseStyle(player);
        }

        public override bool UseItem(Player player)
        {
            if (player.altFunctionUse != 2 && player.itemAnimation == player.itemAnimationMax - 1)
            {
                if (item.Name.Contains("(Power)"))
                {
                    powerAbility(player);
                } 
                else if (item.Name.Contains("(Space)") )
                {
                    spaceAbility(player);
                } 
                else if (item.Name.Contains("(Reality)"))
                {
                    realityAbility(player);
                }
                else if (item.Name.Contains("(Soul)"))
                {
                    soulAbility(player);
                }
                else if (item.Name.Contains("(Time)"))
                {
                    timeAbility(player);
                }

                if (item.Name.Contains("(Mind)"))
                {
                    mindAbility(player);
                }
            }
            
            if (player.altFunctionUse == 2 && player.itemAnimation == player.itemAnimationMax - 1)
            {
                //switchGem();
                player.GetModPlayer<ThanosPlayer>().tbMouseX = Main.mouseX;
                player.GetModPlayer<ThanosPlayer>().tbMouseY = Main.mouseY;
                InfinityGauntletUI.visible = true;
                
                return false;
            }
            
            
            
           
            return base.UseItem(player);
        }

        public override void HoldItem(Player player)
        {
            
            Vector2 vector = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector.X = player.bodyFrame.Width - vector.X;
            }
            if (player.gravDir != 1f)
            {
                vector.Y = player.bodyFrame.Height - vector.Y;
            }
            vector -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
            Vector2 position = player.RotatedRelativePoint(player.position + vector) - player.velocity;

            Color? color = null;
            
            if (item.Name.Contains("(Power)"))
            {
                color = power;
            } 
            else if (item.Name.Contains("(Space)"))
            {
                color = space;
            } 
            else if (item.Name.Contains("(Reality)"))
            {
                color = reality;

                foreach (Dust dust in Main.dust)
                {
                    Vector2 gauntlet = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);
                    if (dust.customData is string)
                    {
                        if ((string) dust.customData == "Reality Beam" && Math.Sqrt(Math.Pow(gauntlet.X - dust.position.X, 2) + Math.Pow(gauntlet.Y - dust.position.Y, 2)) < 10f)
                        {
                            dust.active = false;
                        }
                    }
                    
                }
            }
            else if (item.Name.Contains("(Soul)"))
            {
                color = soul;
            }
            else if (item.Name.Contains("(Time)"))
            {
                color = time;
            }
            else if (item.Name.Contains("(Mind)"))
            {
                color = mind;
            }

            if (color != null && !player.channel)
            {
                Dust dust = Main.dust[Dust.NewDust(position, 0, 0, 212, 0, 0, 0, (Color) color, 1.5f)];
                dust.velocity *= .1f;
                dust.velocity += player.velocity;
                dust.noGravity = true;
            }
            
            
            base.HoldItem(player);
        }

        void realityAbility(Player player)
        {
            if (player.channel)
            {
                if (Main.time % 15 == 0)
                {
                    Main.PlaySound(2, (int) player.Center.X, (int) player.Center.Y, 103, 1f);
                }
                
                player.itemAnimation = player.itemAnimationMax;

                Vector2 mousePos = Main.MouseWorld;
                double theta = Main.rand.NextDouble() * 2 * Math.PI;
                double x = mousePos.X + Main.rand.NextDouble() * 40 * Math.Cos(theta);
                double y = mousePos.Y + Main.rand.NextDouble() * 40 * Math.Sin(theta);
                Projectile.NewProjectile((float) x, (float) y, 0f, 0f, mod.ProjectileType("RealityBeam"), 100, 0, player.whoAmI);
            }
            else
            {
                player.itemAnimation = 1;
                
                
            }
        }

        void spaceAbility(Player player)
        {
            Vector2 newPos = Main.MouseWorld;
            
            Projectile.NewProjectileDirect(player.Center, new Vector2(0, 0), mod.ProjectileType("SpaceStoneWormhole"), 0, 0, player.whoAmI);
            for (int i = 0; i <= 360; i += 4)
            {
                double rad = (Math.PI * i) / 180;
                float dX = (float) (12 * Math.Cos(rad));
                float dY = (float) (12 * Math.Sin(rad));
                Dust dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY), 0, space, 1.5f);
                dust.noGravity = true;
            }
                    
            player.Teleport(newPos, -1);
                    
            Projectile.NewProjectileDirect(player.Center, new Vector2(0, 0), mod.ProjectileType("SpaceStoneWormhole"), 0, 0, player.whoAmI);
            for (int i = 0; i <= 360; i += 4)
            {
                double rad = (Math.PI * i) / 180;
                float dX = (float) (12 * Math.Cos(rad));
                float dY = (float) (12 * Math.Sin(rad));
                Dust dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY), 0, space, 1.5f);
                dust.noGravity = true;
            }
            player.AddBuff(mod.BuffType("SpaceStoneCooldown"), 360);
            Main.PlaySound(SoundID.Item8, player.Center);
        }

        void powerAbility(Player player)
        {
            Vector2 gauntlet = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);
            
            if (player.channel)
            {
                player.itemAnimation = player.itemAnimationMax;
                if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                    if (Main.time % 10 == 0)
                    {
                        Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 34, .3f + (float) (.05 * Math.Sqrt(player.GetModPlayer<ThanosPlayer>().powerStoneCharge)));
                    }
                }

                if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge > 100 && player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                }
                
                if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge > 200 && player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                }
                
                
                
                if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge == 300)
                {
                    if (Main.time % 10 == 0)
                    {
                        Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 15, 1f);
                        for (int i = 0; i <= 360; i += 4)
                        {
                            double rad = (Math.PI * i) / 180;
                            float dX = (float) (10 * Math.Cos(rad));
                            float dY = (float) (10 * Math.Sin(rad));
                            Dust dust = Dust.NewDustPerfect(new Vector2(gauntlet.X + 10 * dX, gauntlet.Y + 10 * dY), 212, 
                                new Vector2(dX * -1f + player.velocity.X, dY * -1f + player.velocity.Y), 0, power);
                            dust.noGravity = true;
                        }
                    }
                    
                }
                else
                {
                    for (int i = 0; i < player.GetModPlayer<ThanosPlayer>().powerStoneCharge / 10; i++)
                    {
                        double rad = Math.PI * Main.rand.NextDouble() * 2;
                        float dX = (float) (10 * Math.Cos(rad));
                        float dY = (float) (10 * Math.Sin(rad));
                    
                        Dust dust = Dust.NewDustPerfect(new Vector2(gauntlet.X + 10 * dX, gauntlet.Y + 10 * dY), 212, 
                            new Vector2(dX * -1f + player.velocity.X, dY * -1f + player.velocity.Y), 0, power);
                        dust.noGravity = true;
                    }
                }
            }
            else
            {
                Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 74, 2f);
                Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 89, 2f);
                Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 93, .5f);
                player.itemAnimation = 1;
                
                player.AddBuff(mod.BuffType("PowerStone"), 6 * player.GetModPlayer<ThanosPlayer>().powerStoneCharge);
                for (int i = 0; i <= 360; i += 5)
                {
                    double rad = (Math.PI * i) / 180;
                    float dX = (float) (20 * Math.Cos(rad));
                    float dY = (float) (20 * Math.Sin(rad));
                    Projectile.NewProjectile(gauntlet, new Vector2(dX, dY), mod.ProjectileType("PowerExplosion"), (int) (player.GetModPlayer<ThanosPlayer>().powerStoneCharge * 3.33333333f), 5, player.whoAmI);
                }
                
                player.GetModPlayer<ThanosPlayer>().powerStoneCharge = 0;
            }
        }

        void soulAbility(Player player)
        {
            if (player.chest == -3)
            {
                player.chest = -1;
                Recipe.FindRecipes();
            }
            else
            {
                player.chest = -3;
                player.chestX = (int) (player.position.X / 16f);
                player.chestY = (int) (player.position.Y / 16f);
                player.talkNPC = -1;
                Main.npcShop = 0;
                Main.playerInventory = true;
                Main.recBigList = false;
                Recipe.FindRecipes();
                player.GetModPlayer<ThanosPlayer>().soulStone = true;
            }
        }

        void timeAbility(Player player)
        {
            Vector2 gauntlet = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);
            int previousHp = player.GetModPlayer<ThanosPlayer>().timeHealth[300];
            player.HealEffect(previousHp - player.statLife);
            player.statLife = previousHp;
            for (int i = 0; i <= 360; i += 4)
            {
                double rad = (Math.PI * i) / 180;
                float dX = (float) (5 * Math.Cos(rad));
                float dY = (float) (5 * Math.Sin(rad));
                Dust dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY), 0, time);
                dust.noGravity = true;
            }
            Main.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 15, 2f);
            player.AddBuff(mod.BuffType("TimeStoneCooldown"), 1800);
        }

        void mindAbility(Player player)
        {
            Vector2 newPos = Main.MouseWorld;
            
            for (int i = 0; i <= 360; i += 3)
            {
                double rad = (Math.PI * i) / 180;
                float dX = (float) (9 * Math.Cos(rad));
                float dY = (float) (9 * Math.Sin(rad));
                Dust dust = Dust.NewDustPerfect(newPos, 212, new Vector2(dX, dY), 0, mind, 1.5f);
                dust.noGravity = true;
            }
            for (int i = 0; i <= 360; i += 3)
            {
                double rad = (Math.PI * i) / 180;
                float dX = (float) (9 * Math.Cos(rad));
                float dY = (float) (9 * Math.Sin(rad));
                Dust dust = Dust.NewDustPerfect(new Vector2(newPos.X + (11 * dX), newPos.Y + (11 * dY)), 212, new Vector2(-dX, -dY), 0, mind, 1.5f);
                dust.noGravity = true;
            }
            
            Main.PlaySound(SoundLoader.customSoundType, (int) player.position.X, (int) player.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/MindStone"));
            
            foreach (NPC npc in Main.npc)
            {
                if (npc.Distance(newPos) < 100f && !npc.friendly && npc.TypeName != "Target Dummy" && !npc.boss)
                {
                    npc.damage = 0;
                    npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly = true;
                    if (Main.netMode == 1)
                    {
                        ModPacket packet = mod.GetPacket();
                        packet.Write((byte)DoomBubblesModMessageType.infinityStone);
                        packet.Write(npc.whoAmI);
                        packet.Write(2);
                        packet.Send();
                    }
                }
            }
            
            
            player.AddBuff(mod.BuffType("MindStoneCooldown"), 1200);
        }
        
        
        
        
        void switchGem()
        {
            
            if (item.Name.Contains("(Power)"))
            {
                item.SetNameOverride("Infinity Gauntlet (Space)");
                item.rare = 1;
                item.expert = false;
                item.channel = false;
            } 
            else if (item.Name.Contains("(Space)"))
            {
                item.SetNameOverride("Infinity Gauntlet (Reality)");
                item.rare = 10;
                item.expert = false;
                item.channel = true;
            } 
            else if (item.Name.Contains("(Reality)"))
            {
                item.SetNameOverride("Infinity Gauntlet (Soul)");
                item.rare = -11;
                item.expert = false;
                item.channel = false;
            }
            else if (item.Name.Contains("(Soul)"))
            {
                item.SetNameOverride("Infinity Gauntlet (Time)");
                item.rare = 2;
                item.expert = false;
                item.channel = false;
            }
            else if (item.Name.Contains("(Time)"))
            {
                item.SetNameOverride("Infinity Gauntlet (Mind)");
                item.rare = 8;
                item.expert = false;
                item.channel = false;
            }
            else 
            {
                item.SetNameOverride("Infinity Gauntlet (Power)");
                item.rare = 11;
                item.expert = false;
                item.channel = true;
            }
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PowerGlove);
            recipe.AddIngredient(mod.ItemType("PowerStone"));
            recipe.AddIngredient(mod.ItemType("SpaceStone"));
            recipe.AddIngredient(mod.ItemType("RealityStone"));
            recipe.AddIngredient(mod.ItemType("SoulStone"));
            recipe.AddIngredient(mod.ItemType("TimeStone"));
            recipe.AddIngredient(mod.ItemType("MindStone"));
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void OnCraft(Recipe recipe)
        {
            Main.PlaySound(SoundLoader.customSoundType, (int) Main.player[Main.myPlayer].position.X, (int) Main.player[Main.myPlayer].position.Y, 
                mod.GetSoundSlot(SoundType.Custom, "Sounds/GauntletComplete"));
            base.OnCraft(recipe);
        }
    }
}
