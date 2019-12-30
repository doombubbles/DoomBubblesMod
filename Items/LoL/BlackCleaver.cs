using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.LoL
{
	public class BlackCleaver : ModItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Black Cleaver");
            Tooltip.SetDefault("Cleaves enemy armor, enraging you\n" +
                               "Equipped - +20% cdr and 40 bonus life");
        }

        public override void SetDefaults()
		{
			item.damage = 80;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = Item.buyPrice(0, 30);
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("Cleaver");
            item.shootSpeed = 8f;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
	        player.GetModPlayer<LoLPlayer>().rage = true;
	        player.GetModPlayer<LoLPlayer>().cleaving = true;
	        player.GetModPlayer<LoLPlayer>().cdr += .2f;
	        player.statLifeMax2 += 40;
	        base.UpdateAccessory(player, hideVisual);
        }

        public override void HoldItem(Player player)
        {
	        player.GetModPlayer<LoLPlayer>().rage = true;
	        player.GetModPlayer<LoLPlayer>().cleaving = true;
	        base.HoldItem(player);
        }

        public override void UpdateInventory(Player player)
        {
	        item.accessory = true;
	        base.UpdateInventory(player);
        }  
        
        public override void AddRecipes()
        {
	        ModRecipe recipe = new ModRecipe(mod);
	        recipe.AddIngredient(mod.ItemType("Phage"));
	        recipe.AddIngredient(mod.ItemType("KindleGem"));
	        recipe.AddIngredient(ItemID.GoldCoin, 4);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.SetResult(this);
	        recipe.AddRecipe();
        }

    }
}
