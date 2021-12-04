using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Projectiles.Ranged;
using Terraria.DataStructures;

namespace DoomBubblesMod.Common.GlobalProjectiles;

internal class DoomBubblesGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;
    
    public bool realityStoned;
}