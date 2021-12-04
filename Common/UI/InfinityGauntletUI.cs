using System.Collections.Generic;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Common.Systems;
using DoomBubblesMod.Content.Items.Thanos;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace DoomBubblesMod.Common.UI;

public class InfinityGauntletUI : UIState
{
    private UIImage backgroundPanel;

    private List<UIImageButton> buttonList;
    private const float PanelHeight = 84f;
    private const float PanelWidth = 117f;

    public int mouseX;
    public int mouseY;


    public override void OnInitialize()
    {
        backgroundPanel =
            new UIImage(ModContent.Request<Texture2D>("DoomBubblesMod/Assets/Textures/UI/Gauntlet", AssetRequestMode.ImmediateLoad));
        backgroundPanel.SetPadding(0);
        backgroundPanel.Left.Set(mouseX / Main.UIScale - PanelWidth / 2f, 0f);
        backgroundPanel.Top.Set(mouseY / Main.UIScale - PanelHeight / 2f, 0f);
        backgroundPanel.Width.Set(PanelWidth, 0f);
        backgroundPanel.Height.Set(PanelHeight, 0f);

        InitializeGems();

        Append(backgroundPanel);
    }


    private void InitializeGems()
    {
        buttonList = new List<UIImageButton>();
        AddButton(0, ModContent.GetInstance<PowerStone>().Texture, 52, 9);
        AddButton(1, ModContent.GetInstance<SpaceStone>().Texture, 37, 7);
        AddButton(2, ModContent.GetInstance<RealityStone>().Texture, 22, 7);
        AddButton(3, ModContent.GetInstance<SoulStone>().Texture, 8, 11);
        AddButton(4, ModContent.GetInstance<TimeStone>().Texture, 70, 28);
        AddButton(5, ModContent.GetInstance<MindStone>().Texture, 29, 26);
    }

    private void AddButton(int i, string texture, int x, int y)
    {
        var buttonTexture = ModContent.Request<Texture2D>(texture, AssetRequestMode.ImmediateLoad);
        buttonList.Add(new UIImageButton(buttonTexture));
        var index = i;
        buttonList[i].OnClick += (_, _) => ChooseGem(index);
        buttonList[i].SetVisibility(1f, .75f);
        buttonList[i].Left.Set(x, 0f);
        buttonList[i].Top.Set(y, 0f);
        backgroundPanel.Append(buttonList[i]);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (!backgroundPanel.ContainsPoint(Main.MouseScreen / Main.UIScale))
        {
            ModContent.GetInstance<UISystem>().InfinityGauntlet.SetState(null);
        }
    }

    private static void ChooseGem(int gem)
    {
        Main.LocalPlayer.GetModPlayer<ThanosPlayer>().gem = gem;
        ModContent.GetInstance<UISystem>().InfinityGauntlet.SetState(null);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);
        if (backgroundPanel.ContainsPoint(Main.MouseScreen / Main.UIScale))
        {
            Main.LocalPlayer.mouseInterface = true;
        }
    }
}