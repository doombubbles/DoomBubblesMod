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
    private const float PanelHeight = 84f;
    private const float PanelWidth = 117f;
    private UIImage backgroundPanel;

    private List<UIImageButton> buttonList;

    public int mouseX;
    public int mouseY;


    public override void OnInitialize()
    {
        backgroundPanel =
            new UIImage(Request<Texture2D>("DoomBubblesMod/Assets/Textures/UI/Gauntlet",
                AssetRequestMode.ImmediateLoad));
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
        buttonList = [];
        AddButton(0, GetInstance<PowerStone>().Texture, 52, 9);
        AddButton(1, GetInstance<SpaceStone>().Texture, 37, 7);
        AddButton(2, GetInstance<RealityStone>().Texture, 22, 7);
        AddButton(3, GetInstance<SoulStone>().Texture, 8, 11);
        AddButton(4, GetInstance<TimeStone>().Texture, 70, 28);
        AddButton(5, GetInstance<MindStone>().Texture, 29, 26);
    }

    private void AddButton(int i, string texture, int x, int y)
    {
        var buttonTexture = Request<Texture2D>(texture, AssetRequestMode.ImmediateLoad);
        buttonList.Add(new UIImageButton(buttonTexture));
        var index = i;
        buttonList[i].OnLeftClick += (_, _) => ChooseGem(index);
        buttonList[i].SetVisibility(1f, .75f);
        buttonList[i].Left.Set(x, 0f);
        buttonList[i].Top.Set(y, 0f);
        backgroundPanel.Append(buttonList[i]);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        if (backgroundPanel.ContainsPoint(Main.MouseScreen))
        {
            Main.LocalPlayer.mouseInterface = true;
        }
        else
        {
            GetInstance<DoomBubblesSystem>().InfinityGauntlet.SetState(null);
        }
    }

    private static void ChooseGem(int gem)
    {
        Main.LocalPlayer.GetModPlayer<ThanosPlayer>().gem = gem;
        GetInstance<DoomBubblesSystem>().InfinityGauntlet.SetState(null);
    }
}