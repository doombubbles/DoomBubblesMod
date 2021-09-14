using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DoomBubblesMod.Items;
using DoomBubblesMod.UI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
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

        public static Mod thoriumMod;
        public static Mod calamityMod;

        public static List<Color> rainbowColors;
        public InfinityGauntletUI infinityGauntletUi;
        public UserInterface InfinityGauntletUserInterface;

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

            rainbowColors = new List<Color>
                {Color.Red, Color.Orange, Color.Yellow, Color.LimeGreen, Color.Blue, Color.Indigo, Color.Violet};
            ModLoader.TryGetMod("ThoriumMod", out thoriumMod);
            ModLoader.TryGetMod("CalamityMod", out calamityMod);

            if (!Main.dedServ)
            {
                infinityGauntletUi = new InfinityGauntletUI();
                infinityGauntletUi.Activate();
                InfinityGauntletUserInterface = new UserInterface();
                InfinityGauntletUserInterface.SetState(infinityGauntletUi);

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
            rainbowColors = null;
            InfinityGauntletUserInterface = null;
            
            thoriumMod = null;
            calamityMod = null;
        }


        public override void AddRecipes()
        {
            /*
            RecipeFinder finder = new RecipeFinder();
            finder.AddIngredient(ItemID.FrostsparkBoots);
            finder.AddIngredient(ItemID.LavaWaders);
            finder.AddIngredient(ItemID.SoulofMight);
            finder.AddIngredient(ItemID.SoulofSight);
            finder.AddIngredient(ItemID.SoulofFright);
            foreach (Recipe recipe2 in finder.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(recipe2);
                editor.DeleteIngredient(ItemID.SoulofMight);
                editor.DeleteIngredient(ItemID.SoulofFright);
                editor.DeleteIngredient(ItemID.SoulofSight);
                editor.AddIngredient(ItemID.SoulofLight);
                editor.AddIngredient(ItemID.SoulofNight);
            }
            */
            /*
            RecipeFinder finder2 = new RecipeFinder();
            finder2.AddIngredient(ItemID.FrostsparkBoots);
            finder2.AddIngredient(ItemID.LavaWaders);
            finder2.AddIngredient(ItemID.PanicNecklace);
            foreach (Recipe recipe in finder2.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(recipe);
                editor.AddIngredient(ItemID.Ectoplasm);
            }
            */


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

        /*
        for (var i = 1; i < ItemLoader.ItemCount; i++)
        {
            var item = new Item();
            Item.SetDefaults(i);
            if (string.IsNullOrEmpty(Item.Name))
            {
                continue;
            }

            var cost = 0;
            
            if (Item.createTile != -1 && (Item.value == 0 || Item.placeStyle != 0))
            {
                cost = 100;
            }

            if (Item.createWall != -1)
            {
                cost = 400;
            }

            if (cost > 0)
            {
                var modRecipe = new ModRecipe(this);
                modRecipe.AddIngredient(i, cost);
                modrecipe.ReplaceResult(i, Item.maxStack);
                try
                {
                    modrecipe.Register();
                }
                catch (Exception e)
                {
                    Logger.Info($"{Item.Name}: {e.Message}");
                }
            }
        }
        */

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            var msgType = (DoomBubblesModMessageType) reader.ReadByte();
            switch (msgType)
            {
                case DoomBubblesModMessageType.infinityStone:
                    var id = reader.ReadInt32();
                    var process = reader.ReadInt32();
                    if (process == 1)
                    {
                        if (!Main.projectile[id].friendly)
                        {
                            Main.projectile[id].hostile = false;
                        }
                    }
                    else if (process == 2)
                    {
                        Main.npc[id].damage = 0;
                        Main.npc[id].GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly = true;
                    }
                    else if (process == 3)
                    {
                        var realityId = reader.ReadInt32();
                        var realityBeam = Main.projectile[realityId];
                        Main.projectile[id].Center = realityBeam.Center;
                        Main.projectile[id].velocity = realityBeam.velocity;
                    }

                    break;
            }
        }
    }


    public enum DoomBubblesModMessageType : byte
    {
        infinityStone
    }
}