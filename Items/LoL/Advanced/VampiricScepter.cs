using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL.Advanced
{
    public class VampiricScepter : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("10% Lifesteal");
        }

        public override void SetDefaults()
        {
            item.damage = 15;
            item.melee = true;
            item.width = 44;
            item.height = 36;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = Item.buyPrice(0, 9);
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = false;
        }
        
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);
            player.GetModPlayer<LoLPlayer>().Lifesteal(damage * .1f, target, false);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("LongSword"));
            recipe.AddIngredient(ItemID.GoldCoin, 5);
            recipe.AddIngredient(ItemID.SilverCoin, 50);
            recipe.AddTile(TileID.Hellforge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}