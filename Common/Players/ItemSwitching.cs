using DoomBubblesMod.Common.Configs;
using Terraria.GameInput;

namespace DoomBubblesMod.Common.Players;

public class ItemSwitchPlayer : ModPlayer
{
    public override void ResetEffects()
    {
        if (Main.myPlayer == Player.whoAmI && Player.itemAnimation == 1 && GetInstance<ClientConfig>().EasyItemSwapping)
        {
            for (var i = 0; i < 10; i++)
            {
                if (PlayerInput.Triggers.Current.KeyStatus[$"Hotbar{i + 1}"] && Player.selectedItem != i && Player.nonTorch == i)
                {
                    Player.releaseUseItem = true;
                    Player.controlUseItem = false;
                }
            }
        }
    }
}