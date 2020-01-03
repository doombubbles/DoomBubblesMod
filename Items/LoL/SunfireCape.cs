using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
    class SunfireCape : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increased maximum life by 40\n" +
                               "Better Inferno effect");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 27, 50);
            item.width = 38;
            item.height = 38;
            item.rare = 8;
            item.accessory = true;
            item.defense = 12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 40;
            if (!hideVisual) player.inferno = true;
            
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.65f, 0.4f, 0.1f);
            int num2 = 24;
            float num3 = 200f;
            bool flag = player.infernoCounter % 60 == 0;
            int damage = 60;
            if (player.whoAmI == Main.myPlayer)
            {
                for (int l = 0; l < 200; l++)
                {
                    NPC nPC = Main.npc[l];
                    if (nPC.active && !nPC.friendly && nPC.damage > 0 && !nPC.dontTakeDamage && !nPC.buffImmune[num2] && Vector2.Distance(player.Center, nPC.Center) <= num3)
                    {
                        if (nPC.FindBuffIndex(num2) == -1)
                        {
                            nPC.AddBuff(num2, 120);
                        }
                        if (flag)
                        {
                            player.GetModPlayer<LoLPlayer>().JustDamage(nPC, damage);
                        }
                    }
                }
            }
        }
        
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("BamisCinder"));
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(mod.ItemType("ChainVest"));
            recipe.AddIngredient(ItemID.GoldCoin, 6);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Autohammer);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
