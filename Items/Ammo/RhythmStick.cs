using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.Ammo
{
    public class RhythmStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\"Hit me with your rhythm stick.\"\n\t-some song I listened to");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 28;
            item.maxStack = 1;
            item.value = 100;
            item.rare = 1;
            //item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/RhythmStick");
            item.useStyle = 1;
            item.useAnimation = 360;
            item.useTime = 360;
            item.useTurn = true;
            // Set other item.X values here
        }

        public override void AddRecipes()
        {
            var recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                Main.PlaySound(SoundLoader.customSoundType, (int) player.position.X, (int) player.position.Y,
                    mod.GetSoundSlot(SoundType.Custom, "Sounds/RhythmStick"));
            }

            return base.UseItem(player);
        }
    }
}