global using Terraria;
global using Terraria.ModLoader;
using System.Collections.Generic;
using System.IO;
using DoomBubblesMod.UI;
using DoomBubblesMod.Utils;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.UI;


namespace DoomBubblesMod
{
    public class DoomBubblesMod : Mod
    {
        public static ModKeybind powerStoneHotKey;
        public static ModKeybind spaceStoneHotKey;
        public static ModKeybind realityStoneHotKey;
        public static ModKeybind soulStoneHotKey;
        public static ModKeybind timeStoneHotKey;
        public static ModKeybind mindStoneHotKey;

        public static Mod ThoriumMod => ModLoader.TryGetMod("ThoriumMod", out var thoriumMod) ? thoriumMod : null;
        public static Mod CalamityMod => ModLoader.TryGetMod("CalamityMod", out var calamityMod) ? calamityMod : null;

        public static List<Color> RainbowColors => new()
            {Color.Red, Color.Orange, Color.Yellow, Color.LimeGreen, Color.Blue, Color.Indigo, Color.Violet};

        public InfinityGauntletUI infinityGauntletUi;
        public UserInterface infinityGauntletUserInterface;

        public override void AddRecipeGroups()
        {
            var recipeGroup = new RecipeGroup(() => "Any Palladium Helmet", ItemID.PalladiumHeadgear,
                ItemID.PalladiumHelmet, ItemID.PalladiumMask);
            RecipeGroup.RegisterGroup("DoomBubblesMod:AnyPalladiumHelmet", recipeGroup);
        }

        public override void Load()
        {
            powerStoneHotKey = KeybindLoader.RegisterKeybind(this, "Power Stone", "F2");
            spaceStoneHotKey = KeybindLoader.RegisterKeybind(this, "Space Stone", "F3");
            realityStoneHotKey = KeybindLoader.RegisterKeybind(this, "Reality Stone", "F4");
            soulStoneHotKey = KeybindLoader.RegisterKeybind(this, "Soul Stone", "F5");
            timeStoneHotKey = KeybindLoader.RegisterKeybind(this, "Time Stone", "F6");
            mindStoneHotKey = KeybindLoader.RegisterKeybind(this, "Mind Stone", "OemTilde");

            if (!Main.dedServ)
            {
                infinityGauntletUi = new InfinityGauntletUI();
                infinityGauntletUi.Activate();
                infinityGauntletUserInterface = new UserInterface();
                infinityGauntletUserInterface.SetState(infinityGauntletUi);

                //TextureAssets.Projectile[ProjectileID.MoonlordBullet]. TODO texture changing
                //Main.dustTexture = GetTexture("Dusts/Dust"); TODO dust changing
            }

            DoomBubblesHooks.Load();
            ThoriumChanges.Load();
        }


        public override void Unload()
        {
            powerStoneHotKey = null;
            spaceStoneHotKey = null;
            realityStoneHotKey = null;
            soulStoneHotKey = null;
            timeStoneHotKey = null;
            mindStoneHotKey = null;
            InfinityGauntletUI.backgroundPanel = null;
            infinityGauntletUi = null;
            infinityGauntletUserInterface = null;
        }


        public override void AddRecipes()
        {
            var badItems = new[]
            {
                "AutoHouse",
                "BoomShuriken",
                "CityBuster",
                "InstaBridge",
                "DoubleObsidianInstaBridge",
                "InstaTrack",
                "Instavator",
                "LihzahrdInstactuationBomb",
                "MiniInstaBridge",
                "ObsidianInstaBridge",
                "Trollbomb",
            };


            ModLoader.TryGetMod("Fargowiltas", out var fargo);

            for (var i = 0; i < Recipe.numRecipes; i++)
            {
                var recipe = Main.recipe[i];

                if (recipe.HasResult(ItemID.TerraBlade))
                {
                    //recipe.AddIngredient(ModContent.ItemType<HeartOfTerraria>());
                }


                if (fargo != null)
                {
                    foreach (var badItem in badItems)
                    {
                        if (fargo.TryFind(badItem, out ModItem bad) && recipe.HasResult(bad))
                        {
                            recipe.RemoveRecipe();
                        }
                    }
                }
            }

            ThoriumChanges.ModifyThoriumRecipes();
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            this.HandleCustomPacket(reader, whoAmI);
        }
    }
}