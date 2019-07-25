using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace DoomBubblesMod.Projectiles
{
    public class StardustBullet : LunarBullet
    {
        public override int DustType => mod.DustType("Stardust229");
        
        
        
    }
}