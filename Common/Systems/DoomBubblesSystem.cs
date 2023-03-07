using System.Collections.Generic;
using System.Linq;
using DoomBubblesMod.Content.Items.Misc;
using DoomBubblesMod.Utils;
using Terraria.UI;

namespace DoomBubblesMod.Common.Systems;

public class DoomBubblesSystem : ModSystem
{
    public UserInterface InfinityGauntlet { get; private set; }

    public override void Load()
    {
        if (!Main.dedServ)
        {
            InfinityGauntlet = new UserInterface();
            // InfinityGauntlet.SetState(new InfinityGauntletUI());
        }
    }

    public override void Unload()
    {
        InfinityGauntlet = null;
    }

    public override void UpdateUI(GameTime gameTime)
    {
        InfinityGauntlet.Update(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        var mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
        if (mouseTextIndex != -1)
        {
            layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                "DoomBubblesMod: Infinity Gauntlet",
                () =>
                {
                    InfinityGauntlet?.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }
    }

    public override void AddRecipeGroups()
    {
        var recipeGroup = new RecipeGroup(() => "Any Palladium Helmet", ItemID.PalladiumHeadgear,
            ItemID.PalladiumHelmet, ItemID.PalladiumMask);
        RecipeGroup.RegisterGroup("DoomBubblesMod:AnyPalladiumHelmet", recipeGroup);
    }

    public override void AddRecipes()
    {
        if (ThoriumMod is Mod thoriumMod)
        {
            foreach (var item in GetContent<ModItem>().OfType<IHasThoriumRecipe>())
            {
                item.AddThoriumRecipe(thoriumMod);
            }
        }


        if (ModLoader.TryGetMod("Fargowiltas", out var fargosMod) &&
            ModLoader.TryGetMod("AmuletOfManyMinions", out var minionsMod))
        {
            void AddCrateRecipe(string result, int crate, int crateAmount, int hardCrate, int extraItem = -1)
            {
                if (crate != -1)
                {
                    var recipe = Recipe.Create(minionsMod.Find<ModItem>(result).Type);
                    recipe.AddIngredient(crate, crateAmount);
                    if (extraItem != -1)
                    {
                        recipe.AddIngredient(extraItem);
                    }

                    recipe.AddTile(TileID.WorkBenches);
                    recipe.Register();
                }

                if (hardCrate != -1)
                {
                    var recipe = Recipe.Create(minionsMod.Find<ModItem>(result).Type);
                    recipe.AddIngredient(hardCrate, crateAmount);
                    if (extraItem != -1)
                    {
                        recipe.AddIngredient(extraItem);
                    }

                    recipe.AddTile(TileID.WorkBenches);
                    recipe.Register();
                }
            }
            
            AddCrateRecipe("TumbleSheepMinionItem", ItemID.WoodenCrate, 3, ItemID.WoodenCrateHard);
            AddCrateRecipe("FishBowlMinionItem", ItemID.OceanCrate, 1, ItemID.OceanCrateHard);
            AddCrateRecipe("BalloonMonkeyMinionItem", ItemID.JungleFishingCrate, 1, ItemID.JungleFishingCrateHard);
            AddCrateRecipe("ExciteSkullMinionItem", ItemID.DungeonFishingCrate, 1, ItemID.DungeonFishingCrateHard);
        }
    }

    public override void PostAddRecipes()
    {
        var badItems = new[]
        {
            // "AutoHouse",
            "BoomShuriken",
            "CityBuster",
            "InstaBridge",
            "DoubleObsidianInstaBridge",
            "GraveBuster",
            "HalfInstavator",
            "Instavator",
            "InstaBridge",
            "InstaTrack",
            "Instavator",
            "LihzahrdInstactuationBomb",
            "MiniInstaBridge",
            "ObsidianInstaBridge",
            "Trollbomb"
        };


        ModLoader.TryGetMod("Fargowiltas", out var fargo);

        for (var i = 0; i < Recipe.numRecipes; i++)
        {
            var recipe = Main.recipe[i];

            if (recipe.HasResult(ItemID.TerraBlade))
            {
                //recipe.AddIngredient(ModContent.ItemType<HeartOfTerraria>());
            }

            if (recipe.HasResult(ItemID.TrueExcalibur) && recipe.HasIngredient(ItemID.ChlorophyteBar))
            {
                recipe.RemoveIngredient(ItemID.ChlorophyteBar);
                recipe.AddIngredient(ItemID.SoulofFright, 5);
                recipe.AddIngredient(ItemID.SoulofMight, 5);
                recipe.AddIngredient(ItemID.SoulofSight, 5);
            }

            if (recipe.HasResult(ItemID.TrueNightsEdge) &&
                recipe.HasIngredient(ItemID.SoulofFright) &&
                recipe.HasIngredient(ItemID.SoulofMight) &&
                recipe.HasIngredient(ItemID.SoulofSight))
            {
                recipe.RemoveIngredient(ItemID.ChlorophyteBar);
                recipe.RemoveIngredient(ItemID.SoulofFright);
                recipe.RemoveIngredient(ItemID.SoulofMight);
                recipe.RemoveIngredient(ItemID.SoulofSight);
                recipe.AddIngredient(ItemID.SoulofFright, 5);
                recipe.AddIngredient(ItemID.SoulofMight, 5);
                recipe.AddIngredient(ItemID.SoulofSight, 5);
            }
            
            if (fargo != null)
            {
                foreach (var badItem in badItems)
                {
                    if (fargo.TryFind(badItem, out ModItem bad) && recipe.HasResult(bad))
                    {
                        recipe.DisableRecipe();
                    }
                }
            }
        }
    }
}