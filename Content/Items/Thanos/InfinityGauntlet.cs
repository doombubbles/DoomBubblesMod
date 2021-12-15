using System;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Common.Systems;
using DoomBubblesMod.Common.UI;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Utils;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Thanos;

internal class InfinityGauntlet : ModItem
{
    public static readonly Color PowerColor = new(123, 0, 255);
    public static readonly Color SpaceColor = new(0, 38, 255);
    public static readonly Color RealityColor = new(150, 0, 0);
    public static readonly Color SoulColor = new(255, 106, 0);
    public static readonly Color TimeColor = new(0, 200, 0);
    public static readonly Color MindColor = new(255, 255, 0);

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
        Item.rare = ItemRarityID.Expert;
        Item.expert = true;
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useTurn = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.GetModPlayer<ThanosPlayer>().infinityGauntlet = Item;
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
                Item.rare = ItemRarityID.Purple;
                Item.expert = false;
                Item.channel = true;
                break;
            case 1:
                Item.SetNameOverride("Infinity Gauntlet (Space)");
                Item.rare = ItemRarityID.Blue;
                Item.expert = false;
                Item.channel = false;
                break;
            case 2:
                Item.SetNameOverride("Infinity Gauntlet (Reality)");
                Item.rare = ItemRarityID.Red;
                Item.expert = false;
                Item.channel = true;
                break;
            case 3:
                Item.SetNameOverride("Infinity Gauntlet (Soul)");
                Item.rare = ItemRarityID.Quest;
                Item.expert = false;
                Item.channel = false;
                break;
            case 4:
                Item.SetNameOverride("Infinity Gauntlet (Time)");
                Item.rare = ItemRarityID.Green;
                Item.expert = false;
                Item.channel = false;
                break;
            case 5:
                Item.SetNameOverride("Infinity Gauntlet (Mind)");
                Item.rare = ItemRarityID.Yellow;
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
                if (player.HasBuff(ModContent.BuffType<SpaceStoneCooldown>()) ||
                    Collision.SolidCollision(newPos,
                        player.width, player.height) ||
                    !(newPos.X > 50f &&
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
                if (previousHp == 0 ||
                    player.HasBuff(ModContent.BuffType<TimeStoneCooldown>()) ||
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
                PowerStone.PowerAbility(player, Item);
            }
            else if (Item.Name.Contains("(Space)"))
            {
                SpaceStone.SpaceAbility(player, Item);
            }
            else if (Item.Name.Contains("(Reality)"))
            {
                RealityStone.RealityAbility(player, Item);
            }
            else if (Item.Name.Contains("(Soul)"))
            {
                SoulStone.SoulAbility(player);
            }
            else if (Item.Name.Contains("(Time)"))
            {
                TimeStone.TimeAbility(player);
            }

            if (Item.Name.Contains("(Mind)"))
            {
                MindStone.MindAbility(Mod, player);
            }
        }

        if (player.altFunctionUse == 2 && player.itemAnimation == player.itemAnimationMax - 1)
        {
            //switchGem();

            ModContent.GetInstance<UISystem>().InfinityGauntlet.SetState(
                new InfinityGauntletUI
                {
                    mouseX = Main.mouseX,
                    mouseY = Main.mouseY
                }
            );

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
                    if ((string) dust.customData == "Reality Beam" &&
                        Math.Sqrt(
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
            var dust = Main.dust[Dust.NewDust(position, 0, 0, DustID.BubbleBurst_White, 0, 0, 0, (Color) color, 1.5f)];
            dust.velocity *= .1f;
            dust.velocity += player.velocity;
            dust.noGravity = true;
        }


        base.HoldItem(player);
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
        SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/GauntletComplete"),
            Main.player[Main.myPlayer].position);
        base.OnCraft(recipe);
    }
}