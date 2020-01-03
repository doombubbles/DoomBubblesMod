using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    class BamisCinder : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bami's Cinder");
            Tooltip.SetDefault("Increased maximum life by 20\n" +
                               "Inferno effect");
        }

        public override void SetDefaults()
        {
            item.value = Item.buyPrice(0, 9);
            item.width = 30;
            item.height = 40;
            item.rare = 4;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 20;
            if (!hideVisual) player.inferno = true;
            
            Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.65f, 0.4f, 0.1f);
            int num2 = 24;
            float num3 = 200f;
            bool flag = player.infernoCounter % 60 == 0;
            int damage = 20;
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
            recipe.AddIngredient(mod.ItemType("RubyCrystal"));
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
