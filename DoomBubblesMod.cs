global using Terraria;
global using Terraria.ModLoader;
global using Terraria.ID;
global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using static Terraria.ModLoader.ModContent;
global using Terraria.DataStructures;
global using Terraria.Localization;
global using EasyNetworkingLib;
global using static DoomBubblesMod.DoomBubblesMod;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DoomBubblesMod.Content.Items.Misc;
using DoomBubblesMod.Utils;
using Microsoft.Xna.Framework.Input;
using Terraria.GameContent.ItemDropRules;


namespace DoomBubblesMod;

public class DoomBubblesMod : Mod
{
    public static ModKeybind PowerStoneHotKey { get; private set; }
    public static ModKeybind SpaceStoneHotKey { get; private set; }
    public static ModKeybind RealityStoneHotKey { get; private set; }
    public static ModKeybind SoulStoneHotKey { get; private set; }
    public static ModKeybind TimeStoneHotKey { get; private set; }
    public static ModKeybind MindStoneHotKey { get; private set; }

    public static Mod ThoriumMod => ModLoader.TryGetMod("ThoriumMod", out var mod) ? mod : null;
    public static Mod CalamityMod => ModLoader.TryGetMod("CalamityMod", out var mod) ? mod : null;
    public static Mod SetBonusAccessories => ModLoader.TryGetMod("SetBonusAccessories", out var mod) ? mod : null;

    public static List<Color> RainbowColors => new()
        {Color.Red, Color.Orange, Color.Yellow, Color.LimeGreen, Color.Blue, Color.Indigo, Color.Violet};

    public static ushort[] RopesForWalls => new[] { TileID.Rope, TileID.VineRope, TileID.Chain, TileID.SilkRope};

    public override void AddRecipeGroups()
    {
        var recipeGroup = new RecipeGroup(() => "Any Palladium Helmet", ItemID.PalladiumHeadgear,
            ItemID.PalladiumHelmet, ItemID.PalladiumMask);
        RecipeGroup.RegisterGroup("DoomBubblesMod:AnyPalladiumHelmet", recipeGroup);
    }

    public override void Load()
    {
        PowerStoneHotKey = KeybindLoader.RegisterKeybind(this, "Power Stone", Keys.F2);
        SpaceStoneHotKey = KeybindLoader.RegisterKeybind(this, "Space Stone", Keys.F3);
        RealityStoneHotKey = KeybindLoader.RegisterKeybind(this, "Reality Stone", Keys.F4);
        SoulStoneHotKey = KeybindLoader.RegisterKeybind(this, "Soul Stone", Keys.F5);
        TimeStoneHotKey = KeybindLoader.RegisterKeybind(this, "Time Stone", Keys.F6);
        MindStoneHotKey = KeybindLoader.RegisterKeybind(this, "Mind Stone", Keys.F7);

        if (!Main.dedServ)
        {
            //TextureAssets.Projectile[ProjectileID.MoonlordBullet]. TODO texture changing
            //Main.dustTexture = GetTexture("Dusts/Dust"); TODO dust changing
        }

        foreach (var ropesForWall in RopesForWalls)
        {
            TileID.Sets.HousingWalls[ropesForWall] = true;
        }

        ThoriumChanges.Load();
    }


    public override void Unload()
    {
        PowerStoneHotKey = null;
        SpaceStoneHotKey = null;
        RealityStoneHotKey = null;
        SoulStoneHotKey = null;
        TimeStoneHotKey = null;
        MindStoneHotKey = null;
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

        ThoriumChanges.ModifyThoriumRecipes();

        if (ThoriumMod != null)
        {
            foreach (var item in GetContent<ModItem>().OfType<IHasThoriumRecipe>())
            {
                item.AddThoriumRecipe(ThoriumMod);
            }
        }

        /*var mimicDrops = Main.ItemDropsDB
            .GetRulesForNPCID(NPCID.Mimic, false)
            .OfType<OneFromOptionsDropRule>()
            .SelectMany(rule => rule.dropIds)
            .ToList();
        foreach (var item in mimicDrops)
        {
            var otherDrops = mimicDrops.Where(i => i != item).ToArray();
            var group = RecipeGroup.RegisterGroup("AnyOtherMimicDropBut" + item,
                new RecipeGroup(() => "Any Other Mimic Drop", otherDrops));
            var recipe = CreateRecipe(item);
            recipe.AddRecipeGroup(group);
            recipe.AddTile(TileID.CrystalBall);
            recipe.Register();
        }*/
    }

    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        ModCustomPacket.Handle(this, reader, whoAmI);
    }
}