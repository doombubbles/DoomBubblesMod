using System;

namespace DoomBubblesMod.Content.Items.Thanos;

public abstract class InfinityStone : ModItem
{
    protected abstract int Rarity { get; }
    protected abstract int Gem { get; }
    protected abstract Color Color { get; }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(5);
        Item.rare = Rarity;
        Item.height = 20;
        Item.width = 14;
        Item.useStyle = ItemUseStyleID.HoldUp;
    }

    public override Nullable<bool> UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
    {
        player.KillMe(PlayerDeathReason.ByCustomReason(player.name +
                                                       " wielded power beyond " +
                                                       (player.Male ? "his" : "her") +
                                                       " control."), 0, 0);

        for (var i = 0; i <= 360; i += 5)
        {
            var rad = Math.PI * i / 180;
            var dX = (float) (10 * Math.Cos(rad));
            var dY = (float) (10 * Math.Sin(rad));
            var dust = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y), 212, new Vector2(dX, dY),
                0, Color, 1.5f);
            dust.noGravity = true;
        }

        return base.UseItem(player);
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(Gem);
        recipe.AddIngredient(ItemID.FragmentNebula, 5);
        recipe.AddIngredient(ItemID.FragmentSolar, 5);
        recipe.AddIngredient(ItemID.FragmentStardust, 5);
        recipe.AddIngredient(ItemID.FragmentVortex, 5);
        recipe.AddTile(TileID.LunarCraftingStation);
        recipe.Register();
    }
}