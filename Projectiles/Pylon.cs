using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class Pylon : ModProjectile
    {
        public static readonly float AttackSpeed = 30f;
        
        public float ShootDistance => ChosenTalent == 2 || ChosenTalent == -1 ? 900f : 600f;
        public float PowerDistance => ChosenTalent == 2 || ChosenTalent == -1 ? 450f : 300f;

        public int ChosenTalent => (int) Math.Round(projectile.ai[0]);

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pylon");
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 52;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.timeLeft = Projectile.SentryLifeTime;
            projectile.tileCollide = true;
            projectile.light = .25f;
            projectile.netImportant = true;
        }


        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = new Vector2(0, 0);
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
            Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>().pylons
                .RemoveAll(i => i == projectile.whoAmI);
            base.Kill(timeLeft);
        }

        public override void AI()
        {
            if (!Main.player[projectile.owner].GetModPlayer<DoomBubblesPlayer>().pylons.Contains(projectile.whoAmI))
            {
                projectile.Kill();
            }
            
            HandleFrames();
            if (projectile.frame > 1)
            {
                CreateDust();
                BuffPlayers();
                Power();
                HandleAttacking();
            }
        }
        
        private void HandleFrames()
        {
            if ((projectile.timeLeft - 1) % 30 == 0)
            {
                projectile.frame++;
            }

            if (projectile.frame == 6)
            {
                projectile.frame = 2;
            }
        }

        private void CreateDust()
        {
            if (Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].type == mod.ItemType("PylonStaff") ||
                projectile.Hitbox.Contains(Main.MouseWorld.ToPoint()))
            {
                for (int i = 0; i < 360; i++)
                {
                    if (Main.rand.Next(4) == 1)
                    {
                        double x = projectile.Center.X + PowerDistance * Math.Cos(i * Math.PI / 180f);
                        double y = projectile.Center.Y + PowerDistance * Math.Sin(i * Math.PI / 180f);
                        Dust dust = Dust.NewDustPerfect(new Vector2((float) x, (float) y), 135);
                        dust.noGravity = true;
                        dust.noLight = true;
                    }
                }

                if (ChosenTalent == 1 || ChosenTalent == -1)
                {
                    for (int i = 0; i < 360; i++)
                    {
                        if (Main.rand.Next(4) == 1)
                        {
                            double x = projectile.Center.X + ShootDistance * Math.Cos(i * Math.PI / 180f);
                            double y = projectile.Center.Y + ShootDistance * Math.Sin(i * Math.PI / 180f);
                            Dust dust = Dust.NewDustPerfect(new Vector2((float) x, (float) y), 182);
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
            for (var i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                int buffType = mod.BuffType("Pylon" + (ChosenTalent == 3 || ChosenTalent == -1 ? "Super":"") + "Power");
                if (player.active && player.Distance(projectile.Center) < PowerDistance)
                {
                    player.AddBuff(buffType, 5);
                }
            }
        }


        public void HandleAttacking()
        {
            if (ChosenTalent != 1 && ChosenTalent != -1)
            {
                return;
            }

            projectile.localAI[0]++;

            if (projectile.localAI[0] >= AttackSpeed)
            {
                projectile.localAI[0] = 0f;
                NPC target;
                if (Main.player[projectile.owner].MinionAttackTargetNPC > 0 
                    && Main.npc[Main.player[projectile.owner].MinionAttackTargetNPC].Distance(projectile.Center) < ShootDistance)
                {
                    target = Main.npc[Main.player[projectile.owner].MinionAttackTargetNPC];
                }
                else
                {
                    List<NPC> inRange = new List<NPC>(); 
                    for (var i = 0; i < Main.npc.Length; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.CanBeChasedBy(projectile) && npc.Distance(projectile.Center) < ShootDistance)
                        {
                            inRange.Add(npc);
                        }
                    }

                    if (inRange.Count == 0)
                    {
                        return;
                    }
                    inRange.Sort((npc1, npc2) => npc1.Distance(Main.player[projectile.owner].Center).CompareTo(npc2.Distance(Main.player[projectile.owner].Center)));
                    target = inRange[0];
                }

                float x = projectile.Center.X;
                float y = projectile.Center.Y - 23;
                float dX = (target.Center.X - x) / 200f;
                float dY = (target.Center.Y - y) / 200f;
                int proj = Projectile.NewProjectile(x, y, dX, dY, mod.ProjectileType("PylonLaser"), projectile.damage, projectile.knockBack, projectile.owner);
                
                Main.projectile[proj].netUpdate = true;
                projectile.netUpdate = true;
                Main.PlaySound(SoundLoader.customSoundType, (int) x, (int) y, mod.GetSoundSlot(SoundType.Custom, "Sounds/PylonLaser"));
            }
            
        }

        public void Power()
        {
            for (var i = 0; i < Main.projectile.Length; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.Distance(projectile.Center) < PowerDistance && proj.type == mod.ProjectileType("PhotonCannon"))
                {
                    proj.ai[1] = 100;
                }
            }
        }
        
        
    }
}