using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;

namespace DoomBubblesMod.Common.Players;

public class CompuPlayer : ModPlayer
{
    public bool showCompuGlow;

    public override void ResetEffects()
    {
        showCompuGlow = false;
    }
}