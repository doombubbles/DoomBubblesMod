using System.Collections.Generic;
using DoomBubblesMod.Items.LoL;
using DoomBubblesMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace DoomBubblesMod.UI
{
    public class RunesUI : UIState
    {
        private VanillaItemSlotWrapper runePageSlot;
        private List<UIImageButton> primaryPathSwitchers;
        private List<UIImageButton> keystoners;

        private bool active => !runePageSlot.Item.IsAir;

        private RunePath primaryPath
        {
            get => runePageSlot.Item.IsAir ? RunePath.None : ((RunePage) runePageSlot.Item.modItem).primaryPath;
            set => ((RunePage) runePageSlot.Item.modItem).primaryPath = value;
        }
        
        private int keystone
        {
            get => runePageSlot.Item.IsAir ? 0 : ((RunePage) runePageSlot.Item.modItem).keystone;
            set => ((RunePage) runePageSlot.Item.modItem).keystone = value;
        }

        public override void OnInitialize()
        {
            runePageSlot = new VanillaItemSlotWrapper
            {
                Left = {Pixels = 50},
                Top = {Pixels = 270},
                ValidItemFunc = item => item.type == ModContent.ItemType<RunePage>() || item.IsAir
            };
            primaryPathSwitchers = new List<UIImageButton>();
            keystoners = new List<UIImageButton>();
            Append(runePageSlot);
        }

        public override void OnDeactivate()
        {
            if (!runePageSlot.Item.IsAir) {
                // QuickSpawnClonedItem will preserve mod data of the item. QuickSpawnItem will just spawn a fresh version of the item, losing the prefix.
                Main.LocalPlayer.QuickSpawnClonedItem(runePageSlot.Item, runePageSlot.Item.stack);
                // Now that we've spawned the item back onto the player, we reset the item by turning it into air.
                runePageSlot.Item.TurnToAir();
            }
        }

        private void PopulatePrimarySwitchers()
        {
            for (int i = 1; i <= 5; i++)
            {
                UIImageButton button = new UIImageButton(ModContent.GetTexture("DoomBubblesMod/UI/Runes/Path" + i));
                button.Left.Set(100 + i * 70, 0);
                button.Top.Set(325, 0);
                var path = (RunePath) i;
                button.OnClick += (evt, element) => SwitchPrimaryPath(path);
                button.SetVisibility(1f, .5f);
                primaryPathSwitchers.Add(button);
            }
        }
        
        private void PopulateKeystoners(RunePath path)
        {
            int count = 3;
            count += path == RunePath.Domination || path == RunePath.Precision ? 1 : 0;
            for (int i = 1; i <= count; i++)
            {
                UIImageButton button = new UIImageButton(ModContent.GetTexture("DoomBubblesMod/UI/Runes/Path" + (int)path + "Keystone" + i));
                button.Left.Set(275 - count * 35 + i * 70, 0);
                button.Top.Set(400, 0);
                var i1 = i;
                button.OnClick += (evt, element) => SwitchKeystone(i1, path);
                button.SetVisibility(1f, .5f);
                keystoners.Add(button);
            }
        }

        private Color getColor(RunePath path)
        {
            switch (path)
            {
                case RunePath.Domination:
                    return Color.Red;
                case RunePath.Precision:
                    return Color.Yellow;
                case RunePath.Sorcery:
                    return Color.Blue;
                case RunePath.Inspiration:
                    return Color.LightBlue;
                case RunePath.Resolve:
                    return Color.Green;
                default:
                    return Color.White;
            }
        }

        private void SwitchPrimaryPath(RunePath path)
        {
            if (primaryPath == path) return;
            primaryPath = path;
            keystone = 0;
            foreach (var uiImageButton in keystoners)
            {
                RemoveChild(uiImageButton);
            }
            keystoners = new List<UIImageButton>();
            CombatText.NewText(Main.LocalPlayer.getRect(), getColor(path), path.ToString());
        }

        private void SwitchKeystone(int keystone, RunePath path)
        {
            if (keystone == this.keystone) return;
            this.keystone = keystone;
            CombatText.NewText(Main.LocalPlayer.getRect(), getColor(path), KeystoneName(keystone, path));
        }
        
        public override void Update(GameTime gameTime) {
            // Don't delete this or the UIElements attached to this UIState will cease to function.
            base.Update(gameTime);

            // talkNPC is the index of the NPC the player is currently talking to. By checking talkNPC, we can tell when the player switches to another NPC or closes the NPC chat dialog.
            if (Main.LocalPlayer.talkNPC == -1 || Main.npc[Main.LocalPlayer.talkNPC].type != ModContent.NPCType<Rioter>()) {
                // When that happens, we can set the state of our UserInterface to null, thereby closing this UIState. This will trigger OnDeactivate above.
                ModContent.GetInstance<DoomBubblesMod>().RunesUserInterface.SetState(null);
                return;
            }
            runePageSlot.Recalculate();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Main.HidePlayerCraftingMenu = true;

            if (active && primaryPathSwitchers.Count == 0)
            {
                PopulatePrimarySwitchers();
                foreach (UIImageButton primaryPathSwitcher in primaryPathSwitchers)
                {
                    Append(primaryPathSwitcher);
                }
            } else if (!active && primaryPathSwitchers.Count != 0)
            {
                foreach (UIImageButton primaryPathSwitcher in primaryPathSwitchers)
                {
                    RemoveChild(primaryPathSwitcher);
                }
                primaryPathSwitchers = new List<UIImageButton>();
            }

            if (active && primaryPath != RunePath.None && keystoners.Count == 0)
            {
                PopulateKeystoners(primaryPath);
                foreach (var keystoner in keystoners)
                {
                    Append(keystoner);
                }
            } else if (!active && keystoners.Count != 0)
            {
                foreach (var uiImageButton in keystoners)
                {
                    RemoveChild(uiImageButton);
                }
                keystoners = new List<UIImageButton>();
            }
            
            for (var i = 0; i < primaryPathSwitchers.Count; i++)
            {
                var button = primaryPathSwitchers[i];
                if (button.ContainsPoint(new Vector2(Main.mouseX, Main.mouseY)))
                {
                    Main.LocalPlayer.mouseInterface = true;
                    Main.hoverItemName = ((RunePath)(i+1)).ToString();
                }
                if ((int)primaryPath == i + 1)
                {
                    button.SetVisibility(1f, 1f);
                } else if (!button.IsMouseHovering)
                {
                    button.SetVisibility(1f, .5f);
                }
            }
            
            for (var i = 0; i < keystoners.Count; i++)
            {
                var button = keystoners[i];
                if (button.ContainsPoint(new Vector2(Main.mouseX, Main.mouseY)))
                {
                    Main.LocalPlayer.mouseInterface = true;
                    Main.hoverItemName = KeystoneName(i + 1, primaryPath) + "\n" + KeystoneDesc(i+1, primaryPath);
                }
                if (keystone == i + 1)
                {
                    button.SetVisibility(1f, 1f);
                } else if (!button.IsMouseHovering)
                {
                    button.SetVisibility(1f, .5f);
                }
            }

            if (!active)
            {
                string message = "Place a Rune Page here to reforge it";
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, message, new Vector2(50 + 55, 270), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, Vector2.Zero, Vector2.One, -1f, 2f);
            }
            
        }
        

        public static string KeystoneName(int i, RunePath path)
        {
            return Language.GetTextValue("Mods.DoomBubblesMod.Path" + (int) path + ".Keystone" + i + ".Name");
        }

        public static string KeystoneDesc(int i, RunePath path)
        {
            return Language.GetTextValue("Mods.DoomBubblesMod.Path" + (int) path + ".Keystone" + i + ".Desc");
        }
    }
}