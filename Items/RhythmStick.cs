using DoomBubblesMod.Utils;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace DoomBubblesMod.Items
{
    public class RhythmStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("\"Hit me with your rhythm stick.\"\n\t-some song I listened to");
            Item.SetResearchAmount(1);
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

        public override bool? UseItem(Player player)
        {
            if (player.itemAnimation == player.itemAnimationMax - 1)
            {
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/DiscordStrike"));
            }

            return base.UseItem(player);
        }
    }
}