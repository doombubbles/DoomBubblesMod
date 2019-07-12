using System;
using System.IO;
using Terraria;
using System.Collections.Generic;
using DoomBubblesMod.UI;
using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace DoomBubblesMod
{
    public class DoomBubblesMod : Mod
    {
        public static bool allDamageLoaded;
        public static bool thoriumLoaded;

        private UserInterface m_InfinityGauntletUserInterface;
        private InfinityGauntletUI m_InfinityGauntletUi;
        
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

            if (!Main.dedServ)
            {
                m_InfinityGauntletUi = new InfinityGauntletUI();
                m_InfinityGauntletUi.Activate();
                m_InfinityGauntletUserInterface = new UserInterface();
                m_InfinityGauntletUserInterface.SetState(m_InfinityGauntletUi);
            }
            
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "DoomBubblesMod",
                    delegate
                    {
                        if (InfinityGauntletUI.visible)
                        {
                            m_InfinityGauntletUserInterface.Update(Main._drawInterfaceGameTime);	//I don't understand
                            m_InfinityGauntletUi.Draw(Main.spriteBatch);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
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
                ModifyThoriumRecipes();
            }
            
        }

        private void ModifyThoriumRecipes()
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
                case DoomBubblesModMessageType.cleaved:
                    int npc2 = reader.ReadInt32();
                    Main.npc[npc2].GetGlobalNPC<DoomBubblesGlobalNPC>(this).Cleaved += 1;
                    break;
                case DoomBubblesModMessageType.cleaving:
                    int npc3 = reader.ReadInt32();
                    int projectile = reader.ReadInt32();
                    Main.projectile[projectile].GetGlobalProjectile<DoomBubblesGlobalProjectile>(this).cleaving.Add(npc3);
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
                    int identity = reader.ReadInt32();
                    if (Main.netMode == NetmodeID.Server)
                    {
                        ModPacket packet = GetPacket();
                        packet.Write((byte)DoomBubblesModMessageType.ak47);
                        packet.Write(identity);
                        packet.Send();
                    }
                    for (int j = 0; j < 1000; j++) {
                        if (Main.projectile[j].identity == identity && Main.projectile[j].active && Main.projectile[j].Name == "Bullet") {
                            Main.projectile[j].GetGlobalProjectile<DoomBubblesGlobalProjectile>(this).ak47 = true;
                            break;
                        }
                    }
                    
                    break;
            }


        }
        
    }


    public enum DoomBubblesModMessageType : byte
    {
        cleaved,
        cleaving,
        infinityStone,
        ak47
    }
        
}