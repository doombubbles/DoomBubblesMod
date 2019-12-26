using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Projectiles.LoL
{
    public class Aery : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }
        
        public override void SetDefaults()
        {
            projectile.width = 54;
            projectile.height = 34;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.minionSlots = 0f;
            projectile.tileCollide = false;
            projectile.timeLeft = 18000;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
        }
        
        public override bool? CanHitNPC(NPC target)
        {
            return target.whoAmI == projectile.ai[0];
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
            Behavior();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.friendly = false;
            projectile.ai[0] = -1f;
            projectile.velocity = Vector2.Zero;
        }

        public void CheckActive() {
            Player player = Main.player[projectile.owner];
            LoLPlayer modPlayer = player.GetModPlayer<LoLPlayer>();
            if (player.dead || modPlayer.aery - 1 != projectile.whoAmI) {
                projectile.Kill();
            }
            if (modPlayer.SummonAery) {
                projectile.timeLeft = 2;
            }
        }

        private void HandleFrames()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public void Behavior()
        {
            Player player = Main.player[projectile.owner];

            projectile.damage = 20 + 5 * player.GetModPlayer<LoLPlayer>().getLevel();
            
            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 48f; // Go up 48 coordinates (three tiles from the center of the player)

            // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
            // The index is projectile.minionPos
            float minionPositionOffsetX = (10 + projectile.minionPos * 40) * -player.direction;
            idlePosition.X += minionPositionOffsetX; // Go behind the player

            // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

            // Teleport to player if distance is too big
            Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f && player.GetModPlayer<LoLPlayer>().keystoneCooldown == 0) {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                projectile.position = idlePosition;
                projectile.velocity *= 0.1f;
                projectile.netUpdate = true;
            }
            
            
            float speed = 20f;
            float inertia = 10f;

            bool foundTarget = projectile.ai[0] > -0.1f;

            if (!foundTarget)
            {
                // Minion doesn't have a target: return to player and idle
                if (distanceToIdlePosition > 600f) {
                    // Speed up the minion if it's away from the player
                    speed = 30f;
                    inertia = 30f;
                }
                else {
                    // Slow down the minion if closer to the player
                    speed = 10f;
                    inertia = 40f;
                }
                if (distanceToIdlePosition > 20f) {
                    // The immediate range around the player (when it passively floats about)

                    // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                    vectorToIdlePosition.Normalize();
                    vectorToIdlePosition *= speed;
                    projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
                }
                else if (projectile.velocity == Vector2.Zero) {
                    // If there is a case where it's not moving at all, give it a little "poke"
                    projectile.velocity.X = -0.15f;
                    projectile.velocity.Y = -0.05f;
                }
                
                if (distanceToIdlePosition < 40f)
                {
                    player.GetModPlayer<LoLPlayer>().keystoneCooldown = 0;
                }
            }
            else
            {
                NPC target = Main.npc[(int) projectile.ai[0]];
                Vector2 targetCenter = target.Center;
                float distanceFromTarget = (targetCenter - projectile.Center).Length();
                
                if (distanceFromTarget > 5f) {
                    // The immediate range around the target (so it doesn't latch onto it when close)
                    Vector2 direction = targetCenter - projectile.Center;
                    direction.Normalize();
                    direction *= speed;
                    projectile.velocity = (projectile.velocity * (inertia - 1) + direction) / inertia;
                }
                else
                {
                    projectile.friendly = true;
                }
            }
            
            projectile.rotation = projectile.velocity.X * 0.05f;
            projectile.spriteDirection = projectile.velocity.X < 0 ? 1 : -1;
        }
    }
}