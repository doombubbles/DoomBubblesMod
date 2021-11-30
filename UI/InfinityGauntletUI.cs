using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;

namespace DoomBubblesMod.UI
{
    public class InfinityGauntletUI : UIState
    {
        public static UIImage backgroundPanel;
        public static bool visible;
        private List<UIImageButton> buttonList;
        private float panelHeight;
        private float panelWidth;


        public override void OnInitialize()
        {
            buttonList = new();
            panelWidth = 84f;
            panelHeight = 117f;

            backgroundPanel = new(ModContent.Request<Texture2D>("DoomBubblesMod/UI/Gauntlet", AssetRequestMode.ImmediateLoad));
            backgroundPanel.SetPadding(0);
            backgroundPanel.Left.Set((Main.screenWidth / 2f - panelWidth / 2) / Main.UIScale, 0f);
            backgroundPanel.Top.Set((Main.screenHeight / 2f - panelHeight / 2) / Main.UIScale, 0f);
            backgroundPanel.Width.Set(panelWidth, 0f);
            backgroundPanel.Height.Set(panelHeight, 0f);

            InitializeGems();

            Append(backgroundPanel);
            Recalculate();
        }


        private void InitializeGems()
        {
            AddButton(0, "DoomBubblesMod/Items/Thanos/PowerStone", 52, 9);
            AddButton(1, "DoomBubblesMod/Items/Thanos/SpaceStone", 37, 7);
            AddButton(2, "DoomBubblesMod/Items/Thanos/RealityStone", 22, 7);
            AddButton(3, "DoomBubblesMod/Items/Thanos/SoulStone", 8, 11);
            AddButton(4, "DoomBubblesMod/Items/Thanos/TimeStone", 70, 28);
            AddButton(5, "DoomBubblesMod/Items/Thanos/MindStone", 29, 26);
        }

        private void AddButton(int i, string texture, int x, int y)
        {
            var buttonTexture = ModContent.Request<Texture2D>(texture, AssetRequestMode.ImmediateLoad);
            buttonList.Add(new(buttonTexture));
            var index = i;
            buttonList[i].OnClick += (evt, element) => ChooseGem(index);
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
                if (!backgroundPanel.ContainsPoint(new(Main.mouseX, Main.mouseY)))
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

        private static void ChooseGem(int gem)
        {
            Main.player[Main.myPlayer].GetModPlayer<ThanosPlayer>().gem = gem;
            visible = false;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var mousePosition = new Vector2(Main.mouseX, Main.mouseY);
            if (backgroundPanel.ContainsPoint(mousePosition))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }
    }
}