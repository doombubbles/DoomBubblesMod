using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    public class SeraphsEmbrace : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seraph's Embrace");
            Tooltip.SetDefault("Increased damage based on your max mana\n" +
                               "Equipped - 25% reduced mana usage and 125 bonus mana\n" +
                               "and 20% cooldown reduction\n" +
                               "20% of your max mana is gained as max life");
            
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 50;
            item.magic = true;
            item.width = 44;
            item.useStyle = 5;
            item.height = 44;
            item.useTime = 4;
            item.useAnimation = 12;
            item.reuseDelay = 10;
            item.mana = 4;
            item.knockBack = 5;
            item.value = Item.buyPrice(0, 32);
            item.rare = 9;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = ProjectileID.StarWrath;
            item.shootSpeed = 10f;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            flat += (int) (player.statManaMax2 * .3);
            base.ModifyWeaponDamage(player, ref add, ref mult, ref flat);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += player.statManaMax2 * .0006f;
            player.manaCost -= .25f;
            player.statManaMax2 += 125;
            player.GetModPlayer<LoLPlayer>().seraph = true;
            base.UpdateAccessory(player, hideVisual);
        }

        public override void UpdateInventory(Player player)
        {
            item.accessory = true;
            base.UpdateInventory(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            float num70 = (float)Main.mouseX + Main.screenPosition.X - position.X;
            float num71 = (float)Main.mouseY + Main.screenPosition.Y - position.Y;
            if (player.gravDir == -1f)
            {
                num71 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - position.Y;
            }

            float f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
            float value12 = 20f;
            float value13 = 60f;
            Vector2 vector19 = position + f.ToRotationVector2() * MathHelper.Lerp(value12, value13, Main.rand.NextFloat());
            for (int num199 = 0; num199 < 50; num199++)
            {
                vector19 = position + f.ToRotationVector2() * MathHelper.Lerp(value12, value13, Main.rand.NextFloat());
                if (Collision.CanHit(position, 0, 0, vector19 + (vector19 - position).SafeNormalize(Vector2.UnitX) * 8f, 0, 0))
                {
                    break;
                }
                f = Main.rand.NextFloat() * ((float)Math.PI * 2f);
            }
            Vector2 v = Main.MouseWorld - vector19;
            Vector2 vector20 = new Vector2(num70, num71).SafeNormalize(Vector2.UnitY) * new Vector2(speedX, speedY).Length();
            v = v.SafeNormalize(vector20) * new Vector2(speedX, speedY).Length();
            v = Vector2.Lerp(v, vector20, 0.25f);
            Projectile proj = Projectile.NewProjectileDirect(vector19, v, type, damage, knockBack, player.whoAmI, 0);
            proj.melee = false;
            proj.magic = true;
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("Tear"));
            recipe.AddIngredient(mod.ItemType("LostChapter"));
            recipe.AddIngredient(ItemID.GoldCoin, 10);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        

    }
}
