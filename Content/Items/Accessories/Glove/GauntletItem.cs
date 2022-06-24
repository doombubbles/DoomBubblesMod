using DoomBubblesMod.Content.Items.Misc;
using DoomBubblesMod.Utils;

namespace DoomBubblesMod.Content.Items.Accessories.Glove;

public abstract class GauntletItem : ModItem, IHasThoriumRecipe
{
    protected virtual int GemID => -1;
    protected virtual string GemName => null;

    public override void SetStaticDefaults()
    {
        SacrificeTotal = 1;
    }

    public override void SetDefaults()
    {
        Item.value = Item.sellPrice(0, 1);
        Item.width = 36;
        Item.height = 40;
        Item.rare = ItemRarityID.Blue;
        Item.accessory = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        player.RadiantDamage(f => f + .05f);
    }

    public void AddThoriumRecipe(Mod thoriumMod)
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(thoriumMod, "LeatherGlove");
        if (GemID > -1)
        {
            recipe.AddIngredient(GemID, 7);
        }
        if (GemName != null)
        {
            recipe.AddIngredient(thoriumMod, GemName, 7);
        }
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}