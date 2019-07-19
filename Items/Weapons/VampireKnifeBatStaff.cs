using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Weapons
{
    public class VampireKnifeBatStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vampire Knife Bat Staff");
            Tooltip.SetDefault("Summons Bats that shoot Vampire Knives as you do");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;
            item.summon = true;
            item.mana = 10;
            item.useTime = 32;
            item.useAnimation = 32;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Item.buyPrice(0, 30, 0, 0);
            item.rare = ItemRarityID.Yellow;
            item.damage = 29;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("VampireKnifeBat");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("VampireKnifeBat");
            item.buffTime = 3600;
        }
        
        public override bool AltFunctionUse(Player player) {
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
            return player.altFunctionUse != 2;
        }

        public override bool UseItem(Player player) {
            if (player.altFunctionUse == 2) {
                player.MinionNPCTargetAim();
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
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BatScepter);
            recipe.AddIngredient(ItemID.VampireKnives);
            recipe.AddIngredient(ItemID.BrokenBatWing, 2);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}