using System;
using System.Collections.Generic;
using DoomBubblesMod.Common.Players;
using DoomBubblesMod.Content.Items.HotS;
using DoomBubblesMod.Utils;
using Terraria.Audio;
using Terraria.GameContent;

namespace DoomBubblesMod.Content.Projectiles.HotS;

public class PhotonCannon : ModProjectile
{
    private static readonly float ProjSpeed = 10f;

    private int ChosenTalent => (int) Math.Round(Projectile.ai[0]);

    private float ShootDistance => 600f;
    private float AttackSpeed => ChosenTalent == 3 || ChosenTalent == -1 ? 30f : 60f;

    public override void SetStaticDefaults()
    {
        DisplayName.SetDefault("Photon Cannon");
        Main.projFrames[Projectile.type] = 6;
    }

    public override void SetDefaults()
    {
        Projectile.width = 36;
        Projectile.height = 36;
        Projectile.friendly = true;
        Projectile.ignoreWater = true;
        Projectile.aiStyle = -1;
        Projectile.timeLeft = 100000;
        Projectile.tileCollide = true;
        Projectile.light = .25f;
        Projectile.minionSlots = .5f;
        Projectile.minion = true;
        Projectile.netImportant = true;
        Projectile.alpha = 69;
    }

    public override bool PreKill(int timeLeft)
    {
        if (Main.player[Projectile.owner].slotsMinions + Projectile.minionSlots >
            Main.player[Projectile.owner].maxMinions &&
            timeLeft == 100000)
        {
            Projectile oldest = null;
            foreach (var proj in Main.projectile)
            {
                if (proj.active &&
                    proj.type == ProjectileType<PhotonCannon>() &&
                    proj.owner == Projectile.owner &&
                    (oldest == null || oldest.timeLeft > proj.timeLeft) &&
                    proj.whoAmI != Projectile.whoAmI)
                {
                    oldest = proj;
                }
            }

            if (oldest != null)
            {
                oldest.position = Projectile.position;
                oldest.timeLeft = oldest.timeLeft % 60 + 100000 - 60;
            }
        }

        return base.PreKill(timeLeft);
    }

    public override bool PreAI()
    {
        if (!(ChosenTalent == 2 || ChosenTalent == -1))
        {
            Projectile.minionSlots = 1f;
        }

        return base.PreAI();
    }

    public override void PostDraw(Color lightColor)
    {
        base.PostDraw(lightColor);
        if (IsPowered() || ChosenTalent is 1 or -1)
        {
            var texture2D = TextureAssets.Projectile[Projectile.type].Value;
            var height = texture2D.Height / Main.projFrames[Projectile.type];
            var y = height * Projectile.frame;


            var pos = (Projectile.position +
                       new Vector2(Projectile.width, Projectile.height) / 2f +
                       Vector2.UnitY * Projectile.gfxOffY -
                       Main.screenPosition).Floor();

            Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Projectiles/HotS/PhotonCannon_Glow").Value, pos,
                new Rectangle(0, y, texture2D.Width, height), Projectile.GetAlpha(lightColor), Projectile.rotation,
                new Vector2(texture2D.Width / 2f, height / 2f), Projectile.scale, SpriteEffects.None, 0);
        }
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.velocity = new Vector2(0, 0);
        return false;
    }

    public override bool? CanHitNPC(NPC target)
    {
        return false;
    }

    public override bool CanHitPlayer(Player target)
    {
        return false;
    }

    public override bool CanHitPvp(Player target)
    {
        return false;
    }

    public override void AI()
    {
        if (Projectile.alpha == 69)
        {
            SoundEngine.PlaySound(Mod.Sound("PhotonCannonWarpIn"),
                Projectile.Center);
            Projectile.alpha = 0;
        }

        CheckActive();
        HandleFrames();
        if (Projectile.frame > 1)
        {
            CreateDust();
            if ((IsPowered() || ChosenTalent == 1 || ChosenTalent == -1) && Projectile.owner == Main.myPlayer)
            {
                HandleAttacking();
            }
        }

        Projectile.ai[1]--;
        if (Projectile.ai[1] < 0)
        {
            Projectile.ai[1] = 0;
        }
    }

    public void CheckActive()
    {
        var player = Main.player[Projectile.owner];
        var modPlayer = player.GetModPlayer<HotSPlayer>();
        if (player.dead)
        {
            modPlayer.photonCannon = false;
        }

        if (modPlayer.photonCannon && Projectile.timeLeft == 10)
        {
            Projectile.timeLeft = 70;
        }

        if (!modPlayer.photonCannon && Projectile.frame > 0)
        {
            Projectile.Kill();
        }
    }

    private void HandleFrames()
    {
        if ((Projectile.timeLeft - 1) % 30 == 0)
        {
            Projectile.frame++;
        }

        if (Projectile.frame == 6)
        {
            Projectile.frame = 2;
        }
    }

    private void CreateDust()
    {
        if (Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].type ==
            ItemType<PhotonCannonStaff>() &&
            Projectile.owner == Main.myPlayer ||
            Projectile.Hitbox.Contains(Main.MouseWorld.ToPoint()))
        {
            for (var i = 0; i < 360; i++)
            {
                if (Main.rand.NextBool(4))
                {
                    var x = Projectile.Center.X + ShootDistance * Math.Cos(i * Math.PI / 180f);
                    var y = Projectile.Center.Y + ShootDistance * Math.Sin(i * Math.PI / 180f);
                    var dust = Dust.NewDustPerfect(new Vector2((float) x, (float) y), 182);
                    dust.scale = .5f;
                    dust.noGravity = true;
                    dust.noLight = true;
                }
            }
        }
    }


    public void HandleAttacking()
    {
        Projectile.localAI[0]++;

        if (Projectile.localAI[0] >= AttackSpeed)
        {
            Projectile.localAI[0] = 0f;
            NPC target;
            if (Main.player[Projectile.owner].MinionAttackTargetNPC > 0 &&
                Main.npc[Main.player[Projectile.owner].MinionAttackTargetNPC].Distance(Projectile.Center) <
                ShootDistance)
            {
                target = Main.npc[Main.player[Projectile.owner].MinionAttackTargetNPC];
            }
            else
            {
                var inRange = new List<NPC>();
                for (var i = 0; i < Main.npc.Length; i++)
                {
                    var npc = Main.npc[i];
                    if (npc.CanBeChasedBy(Projectile) && npc.Distance(Projectile.Center) < ShootDistance)
                    {
                        inRange.Add(npc);
                    }
                }

                if (inRange.Count == 0)
                {
                    return;
                }

                inRange.Sort((npc1, npc2) => npc1.Distance(Main.player[Projectile.owner].Center)
                    .CompareTo(npc2.Distance(Main.player[Projectile.owner].Center)));
                target = inRange[0];
            }

            var x = Projectile.Center.X;
            var y = Projectile.Center.Y - 6;
            var theta = Math.Atan2(target.Center.Y - y, target.Center.X - x);
            var dX = ProjSpeed * Math.Cos(theta);
            var dY = ProjSpeed * Math.Sin(theta);
            var proj = Projectile.NewProjectile(new EntitySource_Parent(Projectile), x, y, (float) dX,
                (float) dY, ProjectileType<Photon>(),
                (int) (Projectile.damage + (ChosenTalent == 1 || ChosenTalent == -1 ? Projectile.ai[1] : 0)),
                Projectile.knockBack, Projectile.owner, ChosenTalent, target.whoAmI);

            Main.projectile[proj].netUpdate = true;
            Projectile.netUpdate = true;
            SoundEngine.PlaySound(Mod.Sound("PhotonShoot"), new Vector2(x, y));
        }
    }

    public bool IsPowered()
    {
        return Projectile.ai[1] > 1f;
    }
}