using System;
using System.IO;
using Terraria;
using System.Collections.Generic;
using DoomBubblesMod.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Player = On.Terraria.Player;

namespace DoomBubblesMod
{
    public class DoomBubblesMod : Mod
    {
        public static ModHotKey PredatorHotKey;
        public static ModHotKey PowerStoneHotKey;
        public static ModHotKey SpaceStoneHotKey;
        public static ModHotKey RealityStoneHotKey;
        public static ModHotKey SoulStoneHotKey;
        public static ModHotKey TimeStoneHotKey;
        public static ModHotKey MindStoneHotKey;
        
        public static bool? thoriumLoaded;
        public static bool calamityLoaded;

        private UserInterface m_InfinityGauntletUserInterface;
        internal InfinityGauntletUI infinityGauntletUi;

        public UserInterface RunesUserInterface;
        
        public static List<Color> rainbowColors;
        
        public DoomBubblesMod()
        {
            
        }

        public override void AddRecipeGroups()
        {
            RecipeGroup recipeGroup = new RecipeGroup(() => "Any Palladium Helmet", ItemID.PalladiumHeadgear, ItemID.PalladiumHelmet, ItemID.PalladiumMask);
            RecipeGroup.RegisterGroup("DoomBubblesMod:AnyPalladiumHelmet", recipeGroup);
        }

        public override void Load()
        {
            PredatorHotKey = RegisterHotKey("Activate Predator", "P");
            PowerStoneHotKey = RegisterHotKey("Power Stone", "F2");
            SpaceStoneHotKey = RegisterHotKey("Space Stone", "F3");
            RealityStoneHotKey = RegisterHotKey("Reality Stone", "F4");
            SoulStoneHotKey = RegisterHotKey("Soul Stone", "F5");
            TimeStoneHotKey = RegisterHotKey("Time Stone", "F6");
            MindStoneHotKey = RegisterHotKey("Mind Stone", "OemTilde");
            
            rainbowColors = new List<Color>()
                {Color.Red, Color.Orange, Color.Yellow, Color.LimeGreen, Color.Blue, Color.Indigo, Color.Violet};
            thoriumLoaded = ModLoader.GetMod("ThoriumMod") != null;
            calamityLoaded = ModLoader.GetMod("CalamityMod") != null;
            /*
            LoLPlayer.RUNES = new Dictionary<RunePath, List<Rune>[]>
            {
                {RunePath.Precision, new[]
                {
                    new List<Rune> {Rune.Overheal, Rune.Triumph, Rune.PresenceOfMind}, 
                    new List<Rune> {Rune.LegendAlacrity, Rune.LegendTenacity, Rune.LegendBloodline}, 
                    new List<Rune> {Rune.CoupDeGrace, Rune.CutDown, Rune.LastStand}
                }},
                {RunePath.Domination, new[]
                {
                    new List<Rune> {Rune.CheapShot, Rune.TasteOfBlood, Rune.SuddenImpact}, 
                    new List<Rune> {Rune.ZombieWard, Rune.GhostPoro, Rune.EyeballCollection}, 
                    new List<Rune> {Rune.RavenousHunter, Rune.IngeniousHunter, Rune.RelentlessHunter, Rune.UltimateHunter}
                }},
                {RunePath.Sorcery, new[]
                {
                    new List<Rune> {Rune.NullifyingOrb, Rune.ManaflowBand, Rune.NimbusCloak}, 
                    new List<Rune> {Rune.Transendence, Rune.Celerity, Rune.AbsoluteFocus}, 
                    new List<Rune> {Rune.Scorch, Rune.Waterwalking, Rune.GatheringStorm}
                }},
                {RunePath.Resolve, new[]
                {
                    new List<Rune> {Rune.Demolish, Rune.FontOfLife, Rune.ShieldBash}, 
                    new List<Rune> {Rune.Conditioning, Rune.SecondWind, Rune.BonePlating}, 
                    new List<Rune> {Rune.Overgrowth, Rune.Revitalize, Rune.Unflinching}
                }},
                {RunePath.Inspiration, new[]
                {
                    new List<Rune> {Rune.HextechFlashtraption, Rune.MagicalFootwear, Rune.PerfectingTiming}, 
                    new List<Rune> {Rune.FuturesMarket, Rune.MinionDematerializer, Rune.BiscuitDelivery}, 
                    new List<Rune> {Rune.CosmicInsight, Rune.ApproachVelocity, Rune.TimeWarpTonic}
                }},
                {RunePath.None, new List<Rune>[0]}
            };
            */
            
            if (!Main.dedServ)
            {
                infinityGauntletUi = new InfinityGauntletUI();
                infinityGauntletUi.Activate();
                m_InfinityGauntletUserInterface = new UserInterface();
                m_InfinityGauntletUserInterface.SetState(infinityGauntletUi);
                
                RunesUserInterface = new UserInterface();

                Main.projectileTexture[ProjectileID.MoonlordBullet] = GetTexture("Projectiles/Projectile_638");
                Main.dustTexture = GetTexture("Dusts/Dust");
            }

            Player.UpdateLifeRegen += PlayerOnUpdateLifeRegen;
            Player.UpdateManaRegen += PlayerOnUpdateManaRegen;
            //On.Terraria.Main.DrawInterface_Resources_Life += MainOnDrawInterfaceResourcesLife;
            
        }

        public override void Unload()
        {
            PredatorHotKey = null;
            PowerStoneHotKey = null;
            SpaceStoneHotKey = null;
            RealityStoneHotKey = null;
            SoulStoneHotKey = null;
            TimeStoneHotKey = null;
            MindStoneHotKey = null;
            //LoLPlayer.RUNES = null;
            infinityGauntletUi = null;
            RunesUserInterface = null;
        }

        private void MainOnDrawInterfaceResourcesLife(On.Terraria.Main.orig_DrawInterface_Resources_Life orig)
        {
            if (Main.LocalPlayer.GetModPlayer<HotSPlayer>().shieldCapacitor > 0)
            {
                Texture2D heart = Main.heartTexture;
                Texture2D heart2 = Main.heart2Texture;
                int life = Main.LocalPlayer.statLife;
                int maxLife = Main.LocalPlayer.statLifeMax2;

                Main.heartTexture = GetTexture("UI/Barrier_Heart" +
                                               Main.LocalPlayer.GetModPlayer<HotSPlayer>().shieldCapacitorChosenTalent);
                Main.heart2Texture = GetTexture("UI/Barrier_LifeHeart" +
                                                Main.LocalPlayer.GetModPlayer<HotSPlayer>()
                                                    .shieldCapacitorChosenTalent);
                Main.LocalPlayer.statLife = life + Main.LocalPlayer.GetModPlayer<HotSPlayer>().shieldCapacitor;
                Main.LocalPlayer.statLifeMax2 =
                    maxLife + Main.LocalPlayer.GetModPlayer<HotSPlayer>().shieldCapacitorMax;

                orig();

                Main.heartTexture = heart;
                Main.heart2Texture = heart2;
                Main.LocalPlayer.statLife = life;
                Main.LocalPlayer.statLifeMax2 = maxLife;
            }
            else orig();
        }

        private void PlayerOnUpdateManaRegen(Player.orig_UpdateManaRegen orig, Terraria.Player self)
        {
            bool sStone = self.GetModPlayer<DoomBubblesPlayer>().sStone;
            if (sStone)
            {
                Vector2 v = self.velocity;
                self.velocity = new Vector2(0,0);
                orig(self);
                self.velocity = v;
            }
            else
            {
                orig(self);
            }
        }

        private void PlayerOnUpdateLifeRegen(Player.orig_UpdateLifeRegen orig, Terraria.Player self)
        {
            bool sStone = self.GetModPlayer<DoomBubblesPlayer>().sStone;
            if (sStone)
            {
                Vector2 v = self.velocity;
                self.velocity = new Vector2(0,0);
                orig(self);
                self.velocity = v;
            }
            else
            {
                orig(self);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "DoomBubblesMod: Infinity Gauntlet",
                    delegate
                    {
                        if (InfinityGauntletUI.visible)
                        {
                            m_InfinityGauntletUserInterface.Update(Main._drawInterfaceGameTime);	//I don't understand
                            infinityGauntletUi.Draw(Main.spriteBatch);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
            
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1) {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "DoomBubblesMod: Runes",
                    delegate {
                        RunesUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
            RunesUserInterface?.Update(gameTime);
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
            
            RecipeFinder finder2 = new RecipeFinder();
            finder2.SetResult(ItemID.TerraBlade);
            foreach (var searchRecipe in finder2.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(ItemType("HeartOfTerraria"));
            }

            if (thoriumLoaded.HasValue && thoriumLoaded.Value)
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
                editor.AddIngredient(ItemType("HeartOfTerraria"));
            }
            
            finder = new RecipeFinder();
            finder.SetResult(thoriumMod.ItemType("TerraScythe"));
            foreach (var searchRecipe in finder.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(ItemType("HeartOfTerraria"));
            }
            
            finder = new RecipeFinder();
            finder.SetResult(thoriumMod.ItemType("TerraBow"));
            foreach (var searchRecipe in finder.SearchRecipes())
            {
                RecipeEditor editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(ItemType("HeartOfTerraria"));
            }

        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            DoomBubblesModMessageType msgType = (DoomBubblesModMessageType)reader.ReadByte();
            switch (msgType)
            {
                case DoomBubblesModMessageType.cleaved:
                    int npc2 = reader.ReadInt32();
                    Main.npc[npc2].GetGlobalNPC<DoomBubblesGlobalNPC>().Cleaved += 1;
                    break;
                case DoomBubblesModMessageType.cleaving:
                    int npc3 = reader.ReadInt32();
                    int projectile = reader.ReadInt32();
                    Main.projectile[projectile].GetGlobalProjectile<DoomBubblesGlobalProjectile>().cleaving.Add(npc3);
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
                        Main.npc[id].GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly = true;
                    }
                    else if (process == 3)
                    {
                        int realityId = reader.ReadInt32();
                        Projectile realityBeam = Main.projectile[realityId];
                        Main.projectile[id].Center = realityBeam.Center;
                        Main.projectile[id].velocity = realityBeam.velocity;
                    }
                    break;
            }


        }
        
    }

    public enum DoomBubblesModMessageType : byte
    {
        cleaved,
        cleaving,
        infinityStone
    }
        
}