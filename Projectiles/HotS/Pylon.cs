using System;
using System.Collections.Generic;
using DoomBubblesMod.Items.HotS;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;


namespace DoomBubblesMod.Projectiles.HotS
{
    public class Pylon : ModProjectile
    {
        public static readonly float AttackSpeed = 30f;

        public float ShootDistance => ChosenTalent is 2 or -1 ? 900f : 600f;
        public float PowerDistance => ChosenTalent is 2 or -1 ? 450f : 300f;

        public int ChosenTalent => (int)Math.Round(Projectile.ai[0]);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pylon");
            Main.projFrames[Projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 52;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = Projectile.SentryLifeTime * 10;
            Projectile.tileCollide = true;
            Projectile.light = .25f;
            Projectile.minion = true;
            Projectile.alpha = 69;
            Projectile.netImportant = true;
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

        public override void Kill(int timeLeft)
        {
            Main.player[Projectile.owner].GetModPlayer<HotSPlayer>().pylons
                .RemoveAll(i => i == Projectile.whoAmI);
            base.Kill(timeLeft);
        }

        public override void AI()
        {
            if (!Main.player[Projectile.owner].GetModPlayer<HotSPlayer>().pylons.Contains(Projectile.whoAmI) &&
                Projectile.owner == Main.myPlayer)
            {
                Projectile.Kill();
                return;
            }

            if (Projectile.alpha == 69)
            {
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/PylonWarpIn"), Projectile.Center);
                Projectile.alpha = 0;
            }

            HandleFrames();
            if (Projectile.frame > 1)
            {
                CreateDust();
                BuffPlayers();
                Power();
                HandleAttacking();
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
                ModContent.ItemType<PylonStaff>() && Projectile.owner == Main.myPlayer ||
                Projectile.Hitbox.Contains(Main.MouseWorld.ToPoint()))
            {
                for (var i = 0; i < 360; i++)
                {
                    if (Main.rand.Next(4) == 1)
                    {
                        var x = Projectile.Center.X + PowerDistance * Math.Cos(i * Math.PI / 180f);
                        var y = Projectile.Center.Y + PowerDistance * Math.Sin(i * Math.PI / 180f);
                        var dust = Dust.NewDustPerfect(new Vector2((float)x, (float)y), 135);
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                }

                if (ChosenTalent == 1 || ChosenTalent == -1)
                {
                    for (var i = 0; i < 360; i++)
                    {
                        if (Main.rand.Next(4) == 1)
                        {
                            var x = Projectile.Center.X + ShootDistance * Math.Cos(i * Math.PI / 180f);
                            var y = Projectile.Center.Y + ShootDistance * Math.Sin(i * Math.PI / 180f);
                            var dust = Dust.NewDustPerfect(new Vector2((float)x, (float)y), 182);
                            dust.scale = .5f;
                            dust.noGravity = true;
                            dust.noLight = true;
                        }
                    }
                }
            }
        }

        private void BuffPlayers()
        {
            foreach (var player in Main.player)
            {
                if (player.active && player.Distance(Projectile.Center) < PowerDistance)
                {
                    var buffType = ModContent
                        .Find<ModBuff>("Pylon" + (ChosenTalent == 3 || ChosenTalent == -1 ? "Super" : "") + "Power")
                        .Type;
                    player.AddBuff(buffType, 5);
                }
            }
        }


        public void HandleAttacking()
        {
            if (ChosenTalent != 1 && ChosenTalent != -1 || Projectile.owner == Main.myPlayer)
            {
                return;
            }

            Projectile.localAI[0]++;

            if (Projectile.localAI[0] >= AttackSpeed)
            {
                Projectile.localAI[0] = 0f;
                NPC target;
                if (Main.player[Projectile.owner].MinionAttackTargetNPC > 0
                    && Main.npc[Main.player[Projectile.owner].MinionAttackTargetNPC].Distance(Projectile.Center) <
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
                var y = Projectile.Center.Y - 23;
                var dX = (target.Center.X - x) / 200f;
                var dY = (target.Center.Y - y) / 200f;
                var proj = Projectile.NewProjectile(new ProjectileSource_ProjectileParent(Projectile), x, y, dX, dY, ModContent.ProjectileType<PylonLaser>(),
                    Projectile.damage,
                    Projectile.knockBack, Projectile.owner);

                Main.projectile[proj].netUpdate = true;
                Projectile.netUpdate = true;
                SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/PylonLaser"), (int)x, (int)y);
            }
        }

        public void Power()
        {
            for (var i = 0; i < Main.projectile.Length; i++)
            {
                var proj = Main.projectile[i];
                if (proj.active && proj.Distance(Projectile.Center) < PowerDistance &&
                    proj.type == ModContent.ProjectileType<PhotonCannon>())
                {
                    proj.ai[1] = 100;
                }
            }
        }
    }
}