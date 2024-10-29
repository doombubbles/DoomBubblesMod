using DoomBubblesMod.Utils;
using System;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Items.Misc;

public class MelodyStick : ModItem
{
    public override void SetStaticDefaults()
    {
        // Tooltip.SetDefault("Time to play chopsticks with these bad boys.");
        Item.ResearchUnlockCount = 1;
    }

    public override void SetDefaults()
    {
        Item.width = 26;
        Item.height = 28;
        Item.maxStack = 1;
        Item.value = 100;
        Item.rare = ItemRarityID.Blue;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 10;
        Item.useTime = 10;
        Item.useTurn = true;
        Item.autoReuse = true;
    }

    public override void AddRecipes()
    {
        var recipe = CreateRecipe();
        recipe.AddIngredient(Mod, "RhythmStick");
        recipe.AddIngredient(ItemID.MusicBox);
        recipe.AddTile(TileID.CrystalBall);
        recipe.Register();
    }

    public override Nullable<bool> UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
    {
        if (player.itemAnimation == player.itemAnimationMax - 1)
        {
            var sound = SoundEngine.PlaySound(Mod.Sound("MelodyStickSound"), player.position).GetSound();
            var soundInstance = sound.Sound;
            var note = Main.mouseX / (float) Main.screenWidth;
            var dynamic = Main.mouseY / (float) Main.screenHeight;
            soundInstance.Volume = dynamic;
            soundInstance.Pitch = note;
        }

        return base.UseItem(player);
    }
}