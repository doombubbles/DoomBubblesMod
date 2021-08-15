using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using SoundType = Terraria.ModLoader.SoundType;

namespace DoomBubblesMod.Items
{
    public class MelodyStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Time to play chopsticks with these bad boys.");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults() {
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
            recipe.AddIngredient(Mod, "RhythmStick", 1);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }

        public override bool? UseItem(Player player) {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                SoundEngine.PlaySound(SoundLoader.customSoundType,
                    (int) player.position.X,
                    (int) player.position.Y,
                    Mod.GetSoundSlot(SoundType.Custom, "Sounds/MelodyStickSound"));
            }
            return base.UseItem(player);
        }
    }
}