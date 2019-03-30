using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DoomBubblesMod.UI
{
    public class InfinityGauntletUI : UIState
    {
        public static UIImage backgroundPanel;
        public static bool visible = false;
        public float panelWidth;
        public float panelHeight;
        public float buttonWidth;
        public float buttonHeight;
        List<UIImageButton> buttonList;



        public override void OnInitialize()
        {
            buttonList = new List<UIImageButton>();
            panelWidth = 84f;
            panelHeight = 117f;
            buttonWidth = 14f;
            buttonHeight = 20f;
            
            backgroundPanel = new UIImage(ModLoader.GetMod("DoomBubblesMod").GetTexture("UI/Gauntlet"));
            backgroundPanel.SetPadding(0);
            backgroundPanel.Left.Set((float)Main.screenWidth / 2 - panelWidth / 2, 0f);
            backgroundPanel.Top.Set((float)Main.screenHeight / 2 - panelHeight / 2, 0f);
            backgroundPanel.Width.Set(panelWidth, 0f);
            backgroundPanel.Height.Set(panelHeight, 0f);
            
            
            initializeGems(ref backgroundPanel);
            
            base.Append(backgroundPanel);
            Recalculate();
        }


        public void initializeGems(ref UIImage backgroundPanel)
        {
            addButton(0, "Items/Thanos/PowerStone", 52, 9);
            addButton(1, "Items/Thanos/SpaceStone", 37, 7);
            addButton(2, "Items/Thanos/RealityStone", 22, 7);
            addButton(3, "Items/Thanos/SoulStone", 8, 11);
            addButton(4, "Items/Thanos/TimeStone", 70, 28);
            addButton(5, "Items/Thanos/MindStone", 29, 26);
        }

        public void addButton(int i, String texture, int x, int y)
        {
            Texture2D buttonTexture = ModLoader.GetMod("DoomBubblesMod").GetTexture(texture);
            buttonList.Add(new UIImageButton(buttonTexture));
            int index = i;
            buttonList[i].OnClick += (evt, element) => chooseGem(index);
            buttonList[i].SetVisibility(1f, .75f);
            buttonList[i].Left.Set(x,0f);
            buttonList[i].Top.Set(y,0f);
            backgroundPanel.Append(buttonList[i]);
        }

        public override void Update(GameTime gameTime)
        {
            float newPanelX = Main.player[Main.myPlayer].GetModPlayer<DoomBubblesPlayer>().tbMouseX - panelWidth / 2;
            float newPanelY = Main.player[Main.myPlayer].GetModPlayer<DoomBubblesPlayer>().tbMouseY - panelHeight / 2;

            if (backgroundPanel.Left.Equals(newPanelX))
            {
                if (!backgroundPanel.ContainsPoint(new Vector2((float) Main.mouseX, (float) Main.mouseY)))
                {
                    visible = false;
                }
            }
            else
            {
                backgroundPanel.Left.Set(newPanelX,0f);
                backgroundPanel.Top.Set(newPanelY,0f);
            }
            
            Recalculate();
        }

        public void chooseGem(int gem)
        {
            Main.player[Main.myPlayer].GetModPlayer<DoomBubblesPlayer>().gem = gem;
            visible = false;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Vector2 MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
            if (backgroundPanel.ContainsPoint(MousePosition))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}