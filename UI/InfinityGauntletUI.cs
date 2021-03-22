using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DoomBubblesMod.UI
{
    internal class InfinityGauntletUI : UIState
    {
        public static UIImage backgroundPanel;
        public static bool visible;
        private List<UIImageButton> buttonList;
        public float panelHeight;
        public float panelWidth;


        public override void OnInitialize()
        {
            buttonList = new List<UIImageButton>();
            panelWidth = 84f;
            panelHeight = 117f;

            backgroundPanel = new UIImage(ModContent.GetTexture("DoomBubblesMod/UI/Gauntlet"));
            backgroundPanel.SetPadding(0);
            backgroundPanel.Left.Set((float) Main.screenWidth / 2 - panelWidth / 2, 0f);
            backgroundPanel.Top.Set((float) Main.screenHeight / 2 - panelHeight / 2, 0f);
            backgroundPanel.Width.Set(panelWidth, 0f);
            backgroundPanel.Height.Set(panelHeight, 0f);

            initializeGems(ref backgroundPanel);

            Append(backgroundPanel);
            Recalculate();
        }


        public void initializeGems(ref UIImage backgroundPanel)
        {
            addButton(0, "DoomBubblesMod/Items/Thanos/PowerStone", 52, 9);
            addButton(1, "DoomBubblesMod/Items/Thanos/SpaceStone", 37, 7);
            addButton(2, "DoomBubblesMod/Items/Thanos/RealityStone", 22, 7);
            addButton(3, "DoomBubblesMod/Items/Thanos/SoulStone", 8, 11);
            addButton(4, "DoomBubblesMod/Items/Thanos/TimeStone", 70, 28);
            addButton(5, "DoomBubblesMod/Items/Thanos/MindStone", 29, 26);
        }

        public void addButton(int i, string texture, int x, int y)
        {
            var buttonTexture = ModContent.GetTexture(texture);
            buttonList.Add(new UIImageButton(buttonTexture));
            var index = i;
            buttonList[i].OnClick += (evt, element) => chooseGem(index);
            buttonList[i].SetVisibility(1f, .75f);
            buttonList[i].Left.Set(x, 0f);
            buttonList[i].Top.Set(y, 0f);
            backgroundPanel.Append(buttonList[i]);
        }

        public override void Update(GameTime gameTime)
        {
            var newPanelX = Main.player[Main.myPlayer].GetModPlayer<ThanosPlayer>().tbMouseX - panelWidth / 2;
            var newPanelY = Main.player[Main.myPlayer].GetModPlayer<ThanosPlayer>().tbMouseY - panelHeight / 2;

            if (backgroundPanel.Left.Pixels == newPanelX)
            {
                if (!backgroundPanel.ContainsPoint(new Vector2(Main.mouseX, Main.mouseY)))
                {
                    visible = false;
                }
            }
            else
            {
                backgroundPanel.Left.Set(newPanelX, 0f);
                backgroundPanel.Top.Set(newPanelY, 0f);
            }

            Recalculate();
        }

        public void chooseGem(int gem)
        {
            Main.player[Main.myPlayer].GetModPlayer<ThanosPlayer>().gem = gem;
            visible = false;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var MousePosition = new Vector2(Main.mouseX, Main.mouseY);
            if (backgroundPanel.ContainsPoint(MousePosition))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}