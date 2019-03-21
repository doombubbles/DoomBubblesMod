using System;
using System.IO;
using Terraria;
using System.Collections.Generic;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    public class DoomBubblesMod : Mod
    {
        public static bool allDamageLoaded;
        public static bool thoriumLoaded;
        
        public DoomBubblesMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }


        public override void Load()
        {
            allDamageLoaded = ModLoader.GetMod("AllDamage") != null;
            thoriumLoaded = ModLoader.GetMod("ThoriumMod") != null;
            base.Load();
        }

        public override void AddRecipes()
        {
            
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
            
            RecipeFinder finder2 = new RecipeFinder();
            finder2.SetResult(ItemID.TerraBlade);
            foreach (var searchRecipe in finder2.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(this.ItemType("HeartOfTerraria"));
            }

            if (thoriumLoaded)
            {
                modifyThoriumRecipes();
            }
            
        }

        private void modifyThoriumRecipes()
        {
            Mod thoriumMod = ModLoader.GetMod("ThoriumMod");
            RecipeFinder finder = new RecipeFinder();
            finder.SetResult(thoriumMod.ItemType("TerraStaff"));
            foreach (var searchRecipe in finder.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(this.ItemType("HeartOfTerraria"));
            }
            
            finder = new RecipeFinder();
            finder.SetResult(thoriumMod.ItemType("TerraScythe"));
            foreach (var searchRecipe in finder.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(this.ItemType("HeartOfTerraria"));
            }
            
            finder = new RecipeFinder();
            finder.SetResult(thoriumMod.ItemType("TerraBow"));
            foreach (var searchRecipe in finder.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(this.ItemType("HeartOfTerraria"));
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            DoomBubblesModMessageType msgType = (DoomBubblesModMessageType)reader.ReadByte();
            switch (msgType)
            {
                case DoomBubblesModMessageType.frostmournedmg:
                    int npc = reader.ReadInt32();
                    int frostmournedmg = reader.ReadInt32();
                    Main.npc[npc].GetGlobalNPC<DoomBubblesGlobalNPC>(this).frostmournedmg = frostmournedmg;
                    break;
                case DoomBubblesModMessageType.cleaved:
                    int npc2 = reader.ReadInt32();
                    Main.npc[npc2].GetGlobalNPC<DoomBubblesGlobalNPC>(this).Cleaved += 1;
                    break;
                case DoomBubblesModMessageType.cleaving:
                    int npc3 = reader.ReadInt32();
                    int projectile = reader.ReadInt32();
                    Main.projectile[projectile].GetGlobalProjectile<DoomBubblesGlobalProjectile>(this).cleaving.Add(npc3);
                    break;
                case DoomBubblesModMessageType.doomlightning:
                    int npc4 = reader.ReadInt32();
                    Main.npc[npc4].GetGlobalNPC<DoomBubblesGlobalNPC>(this).doomlightning += 10;
                    break;
                case DoomBubblesModMessageType.infinityStone:
                    int id = reader.ReadInt32();
                    int process = reader.ReadInt32();
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
                        Main.npc[id].GetGlobalNPC<DoomBubblesGlobalNPC>(this).mindStoneFriendly = true;
                    }
                    else if (process == 3)
                    {
                        int realityId = reader.ReadInt32();
                        Projectile realityBeam = Main.projectile[realityId];
                        Main.projectile[id].Center = realityBeam.Center;
                        Main.projectile[id].velocity = realityBeam.velocity;
                    }
                    break;
                case DoomBubblesModMessageType.ak47:
                    int proj = reader.ReadInt32();
                    Main.projectile[proj].GetGlobalProjectile<DoomBubblesGlobalProjectile>().ak47 = true;
                    break;
            }


        }
        
    }


    public enum DoomBubblesModMessageType : byte
    {
        frostmournedmg,
        cleaved,
        cleaving,
        doomlightning,
        infinityStone,
        ak47
    }
        
}