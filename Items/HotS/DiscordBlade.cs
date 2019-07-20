using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.HotS
{
    public class DiscordBlade : TalentItem
    {
        public override string Talent1Name => "TalentChaosReigns";
        public override string Talent2Name => "TalentDissonance";
        public override string Talent3Name => "TalentLethalOnslaught";
        protected override Color? TalentColor => Color.Red;
        
        private float Length => ChosenTalent == 2 || ChosenTalent == -1 ? 500f : 200f;

        public override bool OnlyShootOnSwing => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Discord Blade");
            Tooltip.SetDefault("Makes a very hard to code triangle attack\n" +
                               "Magic and Melee Weapon");
        }

        public override void SetDefaults()
        {
            item.damage = 75;
            item.melee = true;
            item.magic = true;
            item.width = 36;
            item.height = 44;
            item.useTime = 30;
            item.useAnimation = 30;
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = 640000;
            item.rare = 8;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = mod.ProjectileType("DiscordStrike");
            item.shootSpeed = 10f;
            item.scale = 1.2f;
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox) {
            if (Main.rand.NextBool(5)) {
                //Emit dusts when swing the sword
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 182);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].noLight = false;
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            if (player == Main.LocalPlayer)
            {
                var dX = Main.MouseWorld.X - player.Center.X;
                var dY = Main.MouseWorld.Y - player.Center.Y;
                float theta = new Vector2(dX, dY).ToRotation();
                position = player.Center;
                speedX = (float) Math.Cos(theta) * Length / 15f;
                speedY = (float) Math.Sin(theta) * Length / 15f;
                int proj = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, ChosenTalent);
                Main.projectile[proj].netUpdate = true;
                Main.PlaySound(SoundLoader.customSoundType, (int)position.X, (int)position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/DiscordStrike"));
            }

            return false;
        }
    }
}
