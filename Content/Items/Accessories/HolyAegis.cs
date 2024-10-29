using System.Linq;

namespace DoomBubblesMod.Content.Items.Accessories;

[AutoloadEquip(EquipType.Shield)]
public class HolyAegis : ModItem
{
    public override void SetStaticDefaults()
    {
        /* Tooltip.SetDefault("Increases length of invincibility after taking damage\n" +
                           "Absorbs 25% of damage done to nearby players on your team when above 25% life\n" +
                           "Grants immunity to knockback"); */
    }


    public override void SetDefaults()
    {
        Item.defense = 6; 
        Item.width = 32;
        Item.height = 32;
        Item.value = Item.sellPrice(0, 8);
        Item.rare = ItemRarityID.Yellow;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.longInvince = true;
        player.hasPaladinShield = true;
        player.noKnockback = true;
        if (player.statLife > player.statLifeMax2 / 4)
        {
            foreach (var p in Main.player.Where(p =>
                         p.active && p != player && p.team == player.team && p.Distance(player.Center) < 400))
            {
                p.AddBuff(BuffID.PaladinsShield, 30);
            }
        }
    }

    public override void AddRecipes()
    {
        if (ThoriumMod is not Mod thoriumMod)
        {
            CreateRecipe()
                .AddIngredient(ItemID.PaladinsShield)
                .AddIngredient(ItemID.CrossNecklace)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
        else
        {
            var recipe = CreateRecipe().AddIngredient(this);
            recipe.ReplaceResult(thoriumMod, "HolyAegis");
            recipe.Register();
        }
    }
}