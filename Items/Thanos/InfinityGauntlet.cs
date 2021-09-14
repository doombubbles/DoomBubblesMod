using System;
using DoomBubblesMod.Buffs;
using DoomBubblesMod.Projectiles.Thanos;
using DoomBubblesMod.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using SoundType = Terraria.ModLoader.SoundType;

namespace DoomBubblesMod.Items.Thanos
{
    internal class InfinityGauntlet : ModItem
    {
        public static readonly Color PowerColor = new Color(123, 0, 255);
        public static readonly Color SpaceColor = new Color(0, 38, 255);
        public static readonly Color RealityColor = new Color(150, 0, 0);
        public static readonly Color SoulColor = new Color(255, 106, 0);
        public static readonly Color TimeColor = new Color(0, 200, 0);
        public static readonly Color MindColor = new Color(255, 255, 0);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Gauntlet");
            Tooltip.SetDefault("Right click to select stone\n" +
                               "\"Perfectly balanced...\n" +
                               "...as all things should be.\"\n" +
                               "-Thanos");
            
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(50);
            Item.width = 22;
            Item.height = 30;
            Item.rare = 11;
            Item.expert = true;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = 4;
            Item.useTurn = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ThanosPlayer>().InfinityGauntlet = Item;
            UpdateInventory(player);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            Item.accessory = true;
            switch (player.GetModPlayer<ThanosPlayer>().gem)
            {
                case 0:
                    Item.SetNameOverride("Infinity Gauntlet (Power)");
                    Item.rare = 11;
                    Item.expert = false;
                    Item.channel = true;
                    break;
                case 1:
                    Item.SetNameOverride("Infinity Gauntlet (Space)");
                    Item.rare = 1;
                    Item.expert = false;
                    Item.channel = false;
                    break;
                case 2:
                    Item.SetNameOverride("Infinity Gauntlet (Reality)");
                    Item.rare = 10;
                    Item.expert = false;
                    Item.channel = true;
                    break;
                case 3:
                    Item.SetNameOverride("Infinity Gauntlet (Soul)");
                    Item.rare = -11;
                    Item.expert = false;
                    Item.channel = false;
                    break;
                case 4:
                    Item.SetNameOverride("Infinity Gauntlet (Time)");
                    Item.rare = 2;
                    Item.expert = false;
                    Item.channel = false;
                    break;
                case 5:
                    Item.SetNameOverride("Infinity Gauntlet (Mind)");
                    Item.rare = 8;
                    Item.expert = false;
                    Item.channel = false;
                    break;
            }
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                if (Item.Name.Contains("(Space)"))
                {
                    var newPos = Main.MouseWorld;
                    if (player.HasBuff(ModContent.BuffType<SpaceStoneCooldown>()) || Collision.SolidCollision(newPos,
                                                                               player.width, player.height)
                                                                           || !(newPos.X > 50f &&
                                                                               newPos.X < Main.maxTilesX * 16 - 50 &&
                                                                               newPos.Y > 50f &&
                                                                               newPos.Y < Main.maxTilesY * 16 - 50))
                    {
                        return false;
                    }
                }

                if (Item.Name.Contains("(Time)"))
                {
                    var previousHp = player.GetModPlayer<ThanosPlayer>().timeHealth[300];
                    if (previousHp == 0 || player.HasBuff(ModContent.BuffType<TimeStoneCooldown>()) ||
                        previousHp <= player.statLife)
                    {
                        return false;
                    }
                }

                if (Item.Name.Contains("(Mind)") && player.HasBuff(ModContent.BuffType<MindStoneCooldown>()))
                {
                    return false;
                }
            }

            return base.CanUseItem(player);
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            Item.useStyle = player.altFunctionUse == 2 ? 1 : 4;

            base.UseStyle(player, heldItemFrame);
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse != 2 && player.itemAnimation == player.itemAnimationMax - 1)
            {
                if (Item.Name.Contains("(Power)"))
                {
                    powerAbility(player);
                }
                else if (Item.Name.Contains("(Space)"))
                {
                    spaceAbility(player);
                }
                else if (Item.Name.Contains("(Reality)"))
                {
                    realityAbility(player);
                }
                else if (Item.Name.Contains("(Soul)"))
                {
                    soulAbility(player);
                }
                else if (Item.Name.Contains("(Time)"))
                {
                    timeAbility(player);
                }

                if (Item.Name.Contains("(Mind)"))
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
            var vector = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector.X = player.bodyFrame.Width - vector.X;
            }

            if (player.gravDir != 1f)
            {
                vector.Y = player.bodyFrame.Height - vector.Y;
            }

            vector -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
            var position = player.RotatedRelativePoint(player.position + vector) - player.velocity;

            Color? color = null;

            if (Item.Name.Contains("(Power)"))
            {
                color = PowerColor;
            }
            else if (Item.Name.Contains("(Space)"))
            {
                color = SpaceColor;
            }
            else if (Item.Name.Contains("(Reality)"))
            {
                color = RealityColor;

                foreach (var dust in Main.dust)
                {
                    var gauntlet = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);
                    if (dust.customData is string)
                    {
                        if ((string) dust.customData == "Reality Beam" && Math.Sqrt(
                                Math.Pow(gauntlet.X - dust.position.X, 2) + Math.Pow(gauntlet.Y - dust.position.Y, 2)) <
                            10f)
                        {
                            dust.active = false;
                        }
                    }
                }
            }
            else if (Item.Name.Contains("(Soul)"))
            {
                color = SoulColor;
            }
            else if (Item.Name.Contains("(Time)"))
            {
                color = TimeColor;
            }
            else if (Item.Name.Contains("(Mind)"))
            {
                color = MindColor;
            }

            if (color != null && !player.channel)
            {
                var dust = Main.dust[Dust.NewDust(position, 0, 0, 212, 0, 0, 0, (Color) color, 1.5f)];
                dust.velocity *= .1f;
                dust.velocity += player.velocity;
                dust.noGravity = true;
            }


            base.HoldItem(player);
        }

        private void realityAbility(Player player)
        {
            if (player.channel)
            {
                if (Main.time % 15 == 0)
                {
                    SoundEngine.PlaySound(2, (int) player.Center.X, (int) player.Center.Y, 103);
                }

                player.itemAnimation = player.itemAnimationMax;

                var mousePos = Main.MouseWorld;
                var theta = Main.rand.NextDouble() * 2 * Math.PI;
                var x = mousePos.X + Main.rand.NextDouble() * 40 * Math.Cos(theta);
                var y = mousePos.Y + Main.rand.NextDouble() * 40 * Math.Sin(theta);
                Projectile.NewProjectile(new ProjectileSource_Item(player, Item), (float) x, (float) y, 0f, 0f, ModContent.ProjectileType<RealityBeam>(), 100, 0,
                    player.whoAmI);
            }
            else
            {
                player.itemAnimation = 1;
            }
        }

        private void spaceAbility(Player player)
        {
            var newPos = Main.MouseWorld;

            Projectile.NewProjectileDirect(new ProjectileSource_Item(player, Item), player.Center, new Vector2(0, 0), ModContent.ProjectileType<SpaceStoneWormhole>(),
                0, 0, player.whoAmI);
            for (var i = 0; i <= 360; i += 4)
            {
                var rad = Math.PI * i / 180;
                var dX = (float) (12 * Math.Cos(rad));
                var dY = (float) (12 * Math.Sin(rad));
                var dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY),
                    0, SpaceColor, 1.5f);
                dust.noGravity = true;
            }

            player.Teleport(newPos, -1);

            Projectile.NewProjectileDirect(new ProjectileSource_Item(player, Item), player.Center, new Vector2(0, 0), ModContent.ProjectileType<SpaceStoneWormhole>(),
                0, 0, player.whoAmI);
            for (var i = 0; i <= 360; i += 4)
            {
                var rad = Math.PI * i / 180;
                var dX = (float) (12 * Math.Cos(rad));
                var dY = (float) (12 * Math.Sin(rad));
                var dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY),
                    0, SpaceColor, 1.5f);
                dust.noGravity = true;
            }

            player.AddBuff(ModContent.BuffType<SpaceStoneCooldown>(), 360);
            SoundEngine.PlaySound(SoundID.Item8, player.Center);
        }

        private void powerAbility(Player player)
        {
            var gauntlet = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);

            if (player.channel)
            {
                player.itemAnimation = player.itemAnimationMax;
                if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                    if (Main.time % 10 == 0)
                    {
                        SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 34,
                            .3f + (float) (.05 * Math.Sqrt(player.GetModPlayer<ThanosPlayer>().powerStoneCharge)));
                    }
                }

                if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge > 100 &&
                    player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                }

                if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge > 200 &&
                    player.GetModPlayer<ThanosPlayer>().powerStoneCharge < 300)
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoneCharge += 1;
                }


                if (player.GetModPlayer<ThanosPlayer>().powerStoneCharge == 300)
                {
                    if (Main.time % 10 == 0)
                    {
                        SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 15);
                        for (var i = 0; i <= 360; i += 4)
                        {
                            var rad = Math.PI * i / 180;
                            var dX = (float) (10 * Math.Cos(rad));
                            var dY = (float) (10 * Math.Sin(rad));
                            var dust = Dust.NewDustPerfect(new Vector2(gauntlet.X + 10 * dX, gauntlet.Y + 10 * dY), 212,
                                new Vector2(dX * -1f + player.velocity.X, dY * -1f + player.velocity.Y), 0, PowerColor);
                            dust.noGravity = true;
                        }
                    }
                }
                else
                {
                    for (var i = 0; i < player.GetModPlayer<ThanosPlayer>().powerStoneCharge / 10; i++)
                    {
                        var rad = Math.PI * Main.rand.NextDouble() * 2;
                        var dX = (float) (10 * Math.Cos(rad));
                        var dY = (float) (10 * Math.Sin(rad));

                        var dust = Dust.NewDustPerfect(new Vector2(gauntlet.X + 10 * dX, gauntlet.Y + 10 * dY), 212,
                            new Vector2(dX * -1f + player.velocity.X, dY * -1f + player.velocity.Y), 0, PowerColor);
                        dust.noGravity = true;
                    }
                }
            }
            else
            {
                SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 74, 2f);
                SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 89, 2f);
                SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 93, .5f);
                player.itemAnimation = 1;

                player.AddBuff(ModContent.BuffType<Buffs.PowerStone>(), 6 * player.GetModPlayer<ThanosPlayer>().powerStoneCharge);
                for (var i = 0; i <= 360; i += 5)
                {
                    var rad = Math.PI * i / 180;
                    var dX = (float) (20 * Math.Cos(rad));
                    var dY = (float) (20 * Math.Sin(rad));
                    Projectile.NewProjectile(new ProjectileSource_Item(player, Item), gauntlet, new Vector2(dX, dY), ModContent.ProjectileType<PowerExplosion>(),
                        (int) (player.GetModPlayer<ThanosPlayer>().powerStoneCharge * 3.33333333f), 5, player.whoAmI);
                }

                player.GetModPlayer<ThanosPlayer>().powerStoneCharge = 0;
            }
        }

        private void soulAbility(Player player)
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
                player.SetTalkNPC(-1);
                Main.SetNPCShopIndex(0);
                Main.playerInventory = true;
                Main.recBigList = false;
                Recipe.FindRecipes();
                player.GetModPlayer<ThanosPlayer>().soulStone = true;
            }
        }

        private void timeAbility(Player player)
        {
            var gauntlet = new Vector2(player.Center.X + 10 * player.direction, player.Center.Y - 25);
            var previousHp = player.GetModPlayer<ThanosPlayer>().timeHealth[300];
            player.HealEffect(previousHp - player.statLife);
            player.statLife = previousHp;
            for (var i = 0; i <= 360; i += 4)
            {
                var rad = Math.PI * i / 180;
                var dX = (float) (5 * Math.Cos(rad));
                var dY = (float) (5 * Math.Sin(rad));
                var dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY),
                    0, TimeColor);
                dust.noGravity = true;
            }

            SoundEngine.PlaySound(2, (int) gauntlet.X, (int) gauntlet.Y, 15, 2f);
            player.AddBuff(ModContent.BuffType<TimeStoneCooldown>(), 1800);
        }

        private void mindAbility(Player player)
        {
            var newPos = Main.MouseWorld;

            for (var i = 0; i <= 360; i += 3)
            {
                var rad = Math.PI * i / 180;
                var dX = (float) (9 * Math.Cos(rad));
                var dY = (float) (9 * Math.Sin(rad));
                var dust = Dust.NewDustPerfect(newPos, 212, new Vector2(dX, dY), 0, MindColor, 1.5f);
                dust.noGravity = true;
            }

            for (var i = 0; i <= 360; i += 3)
            {
                var rad = Math.PI * i / 180;
                var dX = (float) (9 * Math.Cos(rad));
                var dY = (float) (9 * Math.Sin(rad));
                var dust = Dust.NewDustPerfect(new Vector2(newPos.X + 11 * dX, newPos.Y + 11 * dY), 212,
                    new Vector2(-dX, -dY), 0, MindColor, 1.5f);
                dust.noGravity = true;
            }

            SoundEngine.PlaySound(SoundLoader.customSoundType, (int) player.position.X, (int) player.position.Y,
                Mod.GetSoundSlot(SoundType.Custom, "Sounds/MindStone"));

            foreach (var npc in Main.npc)
            {
                if (npc.Distance(newPos) < 100f && !npc.friendly && npc.TypeName != "Target Dummy" && !npc.boss)
                {
                    npc.damage = 0;
                    npc.GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly = true;
                    if (Main.netMode == 1)
                    {
                        var packet = Mod.GetPacket();
                        packet.Write((byte) DoomBubblesModMessageType.infinityStone);
                        packet.Write(npc.whoAmI);
                        packet.Write(2);
                        packet.Send();
                    }
                }
            }


            player.AddBuff(ModContent.BuffType<MindStoneCooldown>(), 1200);
        }


        private void switchGem()
        {
            if (Item.Name.Contains("(Power)"))
            {
                Item.SetNameOverride("Infinity Gauntlet (Space)");
                Item.rare = ItemRarityID.Blue;
                Item.expert = false;
                Item.channel = false;
            }
            else if (Item.Name.Contains("(Space)"))
            {
                Item.SetNameOverride("Infinity Gauntlet (Reality)");
                Item.rare = ItemRarityID.Red;
                Item.expert = false;
                Item.channel = true;
            }
            else if (Item.Name.Contains("(Reality)"))
            {
                Item.SetNameOverride("Infinity Gauntlet (Soul)");
                Item.rare = ItemRarityID.Quest;
                Item.expert = false;
                Item.channel = false;
            }
            else if (Item.Name.Contains("(Soul)"))
            {
                Item.SetNameOverride("Infinity Gauntlet (Time)");
                Item.rare = ItemRarityID.Green;
                Item.expert = false;
                Item.channel = false;
            }
            else if (Item.Name.Contains("(Time)"))
            {
                Item.SetNameOverride("Infinity Gauntlet (Mind)");
                Item.rare = ItemRarityID.Yellow;
                Item.expert = false;
                Item.channel = false;
            }
            else
            {
                Item.SetNameOverride("Infinity Gauntlet (Power)");
                Item.rare = ItemRarityID.Purple;
                Item.expert = false;
                Item.channel = true;
            }
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PowerGlove);
            recipe.AddIngredient(ModContent.ItemType<PowerStone>());
            recipe.AddIngredient(ModContent.ItemType<SpaceStone>());
            recipe.AddIngredient(ModContent.ItemType<RealityStone>());
            recipe.AddIngredient(ModContent.ItemType<SoulStone>());
            recipe.AddIngredient(ModContent.ItemType<TimeStone>());
            recipe.AddIngredient(ModContent.ItemType<MindStone>());
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override void OnCraft(Recipe recipe)
        {
            SoundEngine.PlaySound(SoundLoader.customSoundType, (int) Main.player[Main.myPlayer].position.X,
                (int) Main.player[Main.myPlayer].position.Y,
                Mod.GetSoundSlot(SoundType.Custom, "Sounds/GauntletComplete"));
            base.OnCraft(recipe);
        }
    }
}