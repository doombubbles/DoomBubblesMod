using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DoomBubblesMod.UI;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using On.Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using RecipeGroup = Terraria.RecipeGroup;

namespace DoomBubblesMod
{
    public class DoomBubblesMod : Mod
    {
        public static ModHotKey powerStoneHotKey;
        public static ModHotKey spaceStoneHotKey;
        public static ModHotKey realityStoneHotKey;
        public static ModHotKey soulStoneHotKey;
        public static ModHotKey timeStoneHotKey;
        public static ModHotKey mindStoneHotKey;

        public static Mod thoriumMod;
        public static Mod calamityMod;

        public static List<Color> rainbowColors;
        internal InfinityGauntletUI infinityGauntletUi;

        private UserInterface m_InfinityGauntletUserInterface;

        public override void AddRecipeGroups()
        {
            var recipeGroup = new RecipeGroup(() => "Any Palladium Helmet", ItemID.PalladiumHeadgear,
                ItemID.PalladiumHelmet, ItemID.PalladiumMask);
            RecipeGroup.RegisterGroup("DoomBubblesMod:AnyPalladiumHelmet", recipeGroup);
        }

        public override void Load()
        {
            powerStoneHotKey = RegisterHotKey("Power Stone", "F2");
            spaceStoneHotKey = RegisterHotKey("Space Stone", "F3");
            realityStoneHotKey = RegisterHotKey("Reality Stone", "F4");
            soulStoneHotKey = RegisterHotKey("Soul Stone", "F5");
            timeStoneHotKey = RegisterHotKey("Time Stone", "F6");
            mindStoneHotKey = RegisterHotKey("Mind Stone", "OemTilde");

            rainbowColors = new List<Color>
                {Color.Red, Color.Orange, Color.Yellow, Color.LimeGreen, Color.Blue, Color.Indigo, Color.Violet};
            thoriumMod = ModLoader.GetMod("ThoriumMod");
            calamityMod = ModLoader.GetMod("CalamityMod");

            if (!Terraria.Main.dedServ)
            {
                infinityGauntletUi = new InfinityGauntletUI();
                infinityGauntletUi.Activate();
                m_InfinityGauntletUserInterface = new UserInterface();
                m_InfinityGauntletUserInterface.SetState(infinityGauntletUi);

                Terraria.Main.projectileTexture[ProjectileID.MoonlordBullet] = GetTexture("Projectiles/Projectile_638");
                Terraria.Main.dustTexture = GetTexture("Dusts/Dust");
            }

            Player.UpdateLifeRegen += PlayerOnUpdateLifeRegen;
            Player.UpdateManaRegen += PlayerOnUpdateManaRegen;
            IL.Terraria.Player.Update += PlayerOnUpdate;
            //On.Terraria.Main.DrawInterface_Resources_Life += MainOnDrawInterfaceResourcesLife;


            DoomBubblesModExtensions.fields = new Dictionary<string, FieldInfo>();
        }

        private void PlayerOnUpdate(ILContext il)
        {
            var c = new ILCursor(il);
            if (!c.TryGotoNext(i => i.MatchLdcI4(400)))
                return;

            c.Index -= 2;
            for (var i = 0; i < 7; i++)
            {
                c.Remove();
            }
        }

        public override void Unload()
        {
            powerStoneHotKey = null;
            spaceStoneHotKey = null;
            realityStoneHotKey = null;
            soulStoneHotKey = null;
            timeStoneHotKey = null;
            mindStoneHotKey = null;
            //LoLPlayer.RUNES = null;
            infinityGauntletUi = null;

            DoomBubblesModExtensions.attackSpeedField = null;
            DoomBubblesModExtensions.symphonicDamageField = null;
            DoomBubblesModExtensions.symphonicCritField = null;
            DoomBubblesModExtensions.radiantCritField = null;
            DoomBubblesModExtensions.attackSpeedField = null;
            DoomBubblesModExtensions.fields = null;
        }

        private void PlayerOnUpdateManaRegen(Player.orig_UpdateManaRegen orig, Terraria.Player self)
        {
            if (self.active)
            {
                var sStone = self.GetModPlayer<DoomBubblesPlayer>().sStone;
                if (sStone)
                {
                    var v = self.velocity;
                    self.velocity = new Vector2(0, 0);
                    orig(self);
                    self.velocity = v;
                    return;
                }
            }

            orig(self);
        }

        private void PlayerOnUpdateLifeRegen(Player.orig_UpdateLifeRegen orig, Terraria.Player self)
        {
            if (self.active)
            {
                var sStone = self.GetModPlayer<DoomBubblesPlayer>().sStone;
                if (sStone)
                {
                    var v = self.velocity;
                    self.velocity = new Vector2(0, 0);
                    orig(self);
                    self.velocity = v;
                    return;
                }
            }

            orig(self);
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
                        if (InfinityGauntletUI.visible)
                        {
                            m_InfinityGauntletUserInterface.Update(Terraria.Main
                                ._drawInterfaceGameTime); //I don't understand
                            infinityGauntletUi.Draw(Terraria.Main.spriteBatch);
                        }

                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }

        }

        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
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

            var finder2 = new RecipeFinder();
            finder2.SetResult(ItemID.TerraBlade);
            foreach (var searchRecipe in finder2.SearchRecipes())
            {
                var editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(ItemType("HeartOfTerraria"));
            }

            if (thoriumMod != null)
            {
                ModifyThoriumRecipes();
            }
        }

        private void ModifyThoriumRecipes()
        {
            var thoriumMod = ModLoader.GetMod("ThoriumMod");
            var finder = new RecipeFinder();
            finder.SetResult(thoriumMod.ItemType("TerraStaff"));
            foreach (var searchRecipe in finder.SearchRecipes())
            {
                var editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(ItemType("HeartOfTerraria"));
            }

            finder = new RecipeFinder();
            finder.SetResult(thoriumMod.ItemType("TerraScythe"));
            foreach (var searchRecipe in finder.SearchRecipes())
            {
                var editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(ItemType("HeartOfTerraria"));
            }

            finder = new RecipeFinder();
            finder.SetResult(thoriumMod.ItemType("TerraBow"));
            foreach (var searchRecipe in finder.SearchRecipes())
            {
                var editor = new RecipeEditor(searchRecipe);
                editor.AddIngredient(ItemType("HeartOfTerraria"));
            }


            var recipe = new ModRecipe(this);
            recipe.SetResult(ItemID.LightningBoots);
            recipe.AddIngredient(ItemID.SpectreBoots);
            recipe.AddIngredient(thoriumMod.ItemType("Zephyr"));
            recipe.AddIngredient(thoriumMod.ItemType("ZephyrsFeather"));
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.AddRecipe();
        }

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
                        if (!Terraria.Main.projectile[id].friendly)
                        {
                            Terraria.Main.projectile[id].hostile = false;
                        }
                    }
                    else if (process == 2)
                    {
                        Terraria.Main.npc[id].damage = 0;
                        Terraria.Main.npc[id].GetGlobalNPC<DoomBubblesGlobalNPC>().mindStoneFriendly = true;
                    }
                    else if (process == 3)
                    {
                        var realityId = reader.ReadInt32();
                        var realityBeam = Terraria.Main.projectile[realityId];
                        Terraria.Main.projectile[id].Center = realityBeam.Center;
                        Terraria.Main.projectile[id].velocity = realityBeam.velocity;
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