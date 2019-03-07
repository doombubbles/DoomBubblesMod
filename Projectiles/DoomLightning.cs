using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace DoomBubblesMod.Projectiles
{
	public class DoomLightning : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("DoomLightning");
        }

        public override void SetDefaults()
		{
            projectile.width = 130;
            projectile.height = 164;
            projectile.light = 0.3f;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.timeLeft = 300;
            Main.projFrames[projectile.type] = 5;
        }

        public override void AI()
        {
            if (projectile.timeLeft == 300)
            {
                Main.PlaySound(SoundLoader.customSoundType, (int)(projectile.Center.X), (int)(projectile.Center.Y), mod.GetSoundSlot(SoundType.Custom, "Sounds/Lightning"), 4);
                Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>(mod).doom += 1;
            }
            if (Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>(mod).doom > 1 && projectile.timeLeft < 299)
            {
                projectile.timeLeft = 0;
                Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>(mod).doom = 1;
            }
            if (projectile.timeLeft == 1) Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>(mod).doom = 0;


            if (projectile.timeLeft > 290)
            {
                projectile.frame = 0;
            }
            else if (projectile.timeLeft > 280)
            {
                projectile.frame = 1;
            }
            else if (projectile.timeLeft > 270)
            {
                projectile.frame = 2;
            }
            else if ((projectile.timeLeft % 10) == 0)
            {
                projectile.frame += 1;
                if (projectile.frame == 5) projectile.frame = 2;
            }


            if (projectile.frame == 0)
            {
                projectile.height = 30;
            }
            else if (projectile.frame == 1)
            {
                projectile.height = 122;
            }
            else projectile.height = 164;
        }

        public override bool? CanHitNPC(NPC target)
        {
            if (target.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).doomlightning == 0)
            {
                return base.CanHitNPC(target);
            }
            else return false;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.GetGlobalNPC<DoomBubblesGlobalNPC>(mod).doomlightning += 20;
            if (Main.netMode == 1)
            {
                ModPacket packet = mod.GetPacket();
                packet.Write((byte)DoomBubblesModMessageType.doomlightning);
                packet.Write(target.whoAmI);
                packet.Send();
            }
        }

    }
}