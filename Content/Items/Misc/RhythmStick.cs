using DoomBubblesMod.Utils;
using System;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Misc;

public class RhythmStick : ModItem
{
    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("\"Hit me with your rhythm stick.\"\n\t-some song I listened to");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 28;
        Item.maxStack = 1;
        Item.value = 100;
        Item.rare = ItemRarityID.Blue;
        //Item.UseSound = Mod.GetLegacySoundSlot(SoundType.Item, "Sounds/RhythmStick");
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 360;
        Item.useTime = 360;
        Item.useTurn = true;
        // Set other Item.X values here
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wood, 10);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }

    public override Nullable<bool> UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
    {
        if (player.itemAnimation == player.itemAnimationMax - 1)
        {
            SoundEngine.PlaySound(Mod.Sound("RhythmStick"));
        }

        return base.UseItem(player);
    }
}