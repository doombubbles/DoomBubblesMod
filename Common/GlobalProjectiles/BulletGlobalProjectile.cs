using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Projectiles.Ranged;
using Terraria.DataStructures;

namespace DoomBubblesMod.Common.GlobalProjectiles;

public class BulletGlobalProjectile : GlobalProjectile
{
    public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback,
        ref bool crit,
        ref int hitDirection)
    {
        if (projectile.owner != 255 &&
            Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>().luminiteBulletBonus &&
            projectile.type == ProjectileID.MoonlordBullet)
        {
            projectile.damage = (int) (projectile.damage * 1.1f);
        }

        base.ModifyHitNPC(projectile, target, ref damage, ref knockback, ref crit, ref hitDirection);
    }

    public override void Kill(Projectile projectile, int timeLeft)
    {
        base.Kill(projectile, timeLeft);
        if (Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>().crystalBulletBonus &&
            projectile.owner == Main.myPlayer)
        {
            int type;
            if (projectile.type == ProjectileID.CrystalBullet)
            {
                type = ProjectileID.CrystalShard;
            }
            else if (projectile.type == ProjectileType<TerraBullet>())
            {
                type = ProjectileType<TerraShard>();
            }
            else
            {
                return;
            }

            for (var index = 0; index < 3; ++index)
            {
                var speedX =
                    (float) (-(double) projectile.velocity.X * Main.rand.Next(40, 70) * 0.00999999977648258 +
                             Main.rand.Next(-20, 21) * 0.400000005960464);
                var speedY =
                    (float) (-(double) projectile.velocity.Y * Main.rand.Next(40, 70) * 0.00999999977648258 +
                             Main.rand.Next(-20, 21) * 0.400000005960464);
                Projectile.NewProjectile(new EntitySource_Parent(projectile),
                    projectile.position.X + speedX, projectile.position.Y + speedY, speedX,
                    speedY, type, (int) (projectile.damage * 0.5), 0.0f, projectile.owner);
            }
        }
    }
}