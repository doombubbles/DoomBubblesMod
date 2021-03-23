using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items
{
    public class MelodyStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Time to play chopsticks with these bad boys.");
        }

        public override void SetDefaults() {
            item.width = 26;
            item.height = 28;
            item.maxStack = 1;
            item.value = 100;
            item.rare = 1;
            item.useStyle = 1;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useTurn = true;
            item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "RhythmStick", 1);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player) {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                Main.PlaySound(SoundLoader.customSoundType,
                    (int) player.position.X,
                    (int) player.position.Y,
                    mod.GetSoundSlot(SoundType.Custom, "Sounds/MelodyStickSound"));
            }
            return base.UseItem(player);
        }
    }
}