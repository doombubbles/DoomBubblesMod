using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Buffs;
using DoomBubblesMod.Utils;
using Terraria.Audio;

namespace DoomBubblesMod.Content.Projectiles.HotS;

public class PhaseBomb : HotsProjectile2
{
    protected override bool Centered => true;

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 9;
    }

    public override void SetDefaults()
    {
        Projectile.width = 100;
        Projectile.height = 100;
        Projectile.scale = .33f;
        Projectile.aiStyle = 0;
        Projectile.friendly = true;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.timeLeft = 1000;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.penetrate = -1;
        Projectile.alpha = 69;
    }

    public override bool? CanHitNPC(NPC target)
    {
        if (1000 - Projectile.timeLeft <= Projectile.ai[0] && !(Projectile.ai[1] == 1 || Projectile.ai[1] == -1))
        {
            return false;
        }

        return 1000 - Projectile.timeLeft > (int) Projectile.ai[0] + 1 ? false : base.CanHitNPC(target);
    }

    public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
    {
        Projectile.localAI[0]++;
    }

    public override void AI()
    {
        if (Projectile.alpha == 69)
        {
            SoundEngine.PlaySound(Mod.Sound("Phase"), Projectile.position);
        }

        if (Projectile.alpha > 0)
        {
            Projectile.alpha -= 15;
        }
        else
        {
            Projectile.alpha = 0;
        }


        Lighting.AddLight((int) Projectile.Center.X / 16, (int) Projectile.Center.Y / 16, 0.4f, 0.4f, 0.5f);

        var center = Projectile.Center;

        if (1000 - Projectile.timeLeft <= Projectile.ai[0])
        {
            Projectile.frame = Projectile.timeLeft / 10 % 2;
            Projectile.velocity *= 1.015f;
        }
        else
        {
            Projectile.width = (int) (20 * Projectile.knockBack);
            Projectile.height = (int) (20 * Projectile.knockBack);
            Projectile.scale = Projectile.knockBack / 5f;
            Projectile.Center = center;
            if (Projectile.velocity != new Vector2(0, 0))
            {
                var npcs = 0;
                for (var i = 0; i < Main.npc.Length; i++)
                {
                    var npc = Main.npc[i];
                    if (!npc.active || npc.friendly)
                    {
                        continue;
                    }

                    if (Projectile.Hitbox.Intersects(npc.getRect()))
                    {
                        npcs++;
                        npc.immune[Projectile.owner] = 0;
                    }
                }

                if (npcs == 1 && (Projectile.ai[1] == 2 || Projectile.ai[1] == -1))
                {
                    Projectile.damage = (int) (Projectile.damage * 2 * Projectile.knockBack / 5f);
                }

                SoundEngine.PlaySound(Mod.Sound("Bomb"), Projectile.position);
            }

            Projectile.velocity = new Vector2(0, 0);

            Projectile.frame++;


            if (Projectile.frame == 9)
            {
                Projectile.Kill();
            }
        }
    }

    public override void Kill(int timeLeft)
    {
        if (Projectile.localAI[0] > 0)
        {
            var player = Main.player[Projectile.owner];
            player.AddBuff(BuffType<FenixRepeaterBuff>(), 360);
            player.GetModPlayer<HotsPlayer>().fenixRepeaterBuff += (int) Projectile.localAI[0];
            if (player.gravControl2)
            {
                if (player.GetModPlayer<HotsPlayer>().fenixRepeaterBuff > 15)
                {
                    player.GetModPlayer<HotsPlayer>().fenixRepeaterBuff = 15;
                }
            }
            else if (player.GetModPlayer<HotsPlayer>().fenixRepeaterBuff > 10)
            {
                player.GetModPlayer<HotsPlayer>().fenixRepeaterBuff = 10;
            }
        }

        base.Kill(timeLeft);
    }
}