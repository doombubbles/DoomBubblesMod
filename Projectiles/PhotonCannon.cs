using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles
{
    public class PhotonCannon : ModProjectile
    {
        private static readonly float ProjSpeed = 10f;

        private int ChosenTalent => (int) Math.Round(projectile.ai[0]);

        private float ShootDistance => 600f;
        private float AttackSpeed => ChosenTalent == 3 || ChosenTalent == -1 ? 30f : 60f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Photon Cannon");
            Main.projFrames[projectile.type] = 6;
        }

        public override void SetDefaults()
        {
            projectile.width = 36;
            projectile.height = 36;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            projectile.timeLeft = 100000;
            projectile.tileCollide = true;
            projectile.light = .25f;
            projectile.minionSlots = .5f;
            projectile.minion = true;
            projectile.netImportant = true;
        }

        public override bool PreKill(int timeLeft)
        {
            if (Main.player[projectile.owner].slotsMinions + projectile.minionSlots >
                (float) Main.player[projectile.owner].maxMinions && timeLeft == 100000)
            {
                Projectile oldest = null;
                foreach (var proj in Main.projectile)
                {
                    if (proj.active && proj.type == mod.ProjectileType("PhotonCannon") && proj.owner == projectile.owner && 
                        (oldest == null || oldest.timeLeft > proj.timeLeft) && proj.whoAmI != projectile.whoAmI)
                    {
                        oldest = proj;
                    }
                }

                if (oldest != null)
                {
                    oldest.position = projectile.position;
                    oldest.timeLeft = oldest.timeLeft % 60 + 100000 - 60;
                }
            }
            return base.PreKill(timeLeft);
        }

        public override bool PreAI()
        {
            if (!(ChosenTalent == 2 || ChosenTalent == -1))
            {
                projectile.minionSlots = 1f;
            }
            return base.PreAI();
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            base.PostDraw(spriteBatch, lightColor);
            
            if (isPowered() || ChosenTalent == 1 || ChosenTalent == -1)
            {
                
                Texture2D texture2D = Main.projectileTexture[projectile.type];
                int height = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
                int y = height * projectile.frame;
            
            
                Vector2 pos = (projectile.position + new Vector2(projectile.width, projectile.height) / 2f +
                               Vector2.UnitY * projectile.gfxOffY - Main.screenPosition).Floor();
            
                spriteBatch.Draw(mod.GetTexture("Projectiles/PhotonCannon_Glow"), pos, new Rectangle(0, y, texture2D.Width, height), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture2D.Width / 2f, (float) height / 2f), projectile.scale, SpriteEffects.None, 0f);
            }
            
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

        public override void AI()
        {
            CheckActive();
            HandleFrames();
            if (projectile.frame > 1)
            {
                CreateDust();
                if (isPowered() || ChosenTalent == 1 || ChosenTalent == -1)
                {
                    HandleAttacking();
                }
            }

            projectile.ai[1]--;
            if (projectile.ai[1] < 0)
            {
                projectile.ai[1] = 0;
            }
        }
        
        public void CheckActive() {
            Player player = Main.player[projectile.owner];
            DoomBubblesPlayer modPlayer = player.GetModPlayer<DoomBubblesPlayer>();
            if (player.dead) {
                modPlayer.photonCannon = false;
            }

            if (modPlayer.photonCannon && projectile.timeLeft == 10)
            {
                projectile.timeLeft = 70;
            }
            if (!modPlayer.photonCannon && projectile.frame > 0) {
                projectile.Kill();
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
            if (Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].type == mod.ItemType("PhotonCannonStaff") ||
                projectile.Hitbox.Contains(Main.MouseWorld.ToPoint()))
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


        public void HandleAttacking()
        {
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
                float y = projectile.Center.Y - 6;
                double theta = Math.Atan2(target.Center.Y - y, target.Center.X - x);
                double dX = ProjSpeed * Math.Cos(theta);
                double dY = ProjSpeed * Math.Sin(theta);
                int proj = Projectile.NewProjectile(x, y, (float)dX, (float)dY, mod.ProjectileType("Photon"), (int) (projectile.damage + (ChosenTalent == 1 || ChosenTalent == -1 ? projectile.ai[1] : 0)), projectile.knockBack, projectile.owner, ChosenTalent, target.whoAmI);
                
                Main.projectile[proj].netUpdate = true;
                projectile.netUpdate = true;
                Main.PlaySound(SoundLoader.customSoundType, (int) x, (int) y, mod.GetSoundSlot(SoundType.Custom, "Sounds/PhotonShoot"));
            }
        }
        
        public bool isPowered()
        {
            return projectile.ai[1] > 1f;
        }
        
        
    }
}