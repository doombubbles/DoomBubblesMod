using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.HotS
{
    public class PhaseBombLauncher : TalentItem
    {
        public override string Talent1Name => "TalentSecondaryFire";
        public override string Talent2Name => "TalentSingularityCharge";
        public override string Talent3Name => "TalentDivertPowerWeapons";
        protected override Color? TalentColor => Color.Orange;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phase Bomb Launcher");
            Tooltip.SetDefault("Buffs Repeater Cannon attack speed for each enemy hit\n" +
                               "(Up to 10)");
        }

        public override void SetDefaults()
        {
            item.width = 74;
            item.height = 34;
            item.noMelee = true;
            item.damage = 83;
            item.shoot = mod.ProjectileType("PhaseBomb");
            item.shootSpeed = 7f;
            item.useAnimation = 38;
            item.useTime = 38;
            item.ranged = true;
            item.knockBack = 7;
            item.useStyle = 5;
            item.rare = 10;
            item.autoReuse = true;
            item.value = Item.buyPrice(0, 69, 0, 0);
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, -3);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult)
        {
            if (ChosenTalent == 3 || ChosenTalent == -1)
            {
                add += .5f;
            }

        }
        
        public override float UseTimeMultiplier(Player player)
        {
            if (ChosenTalent == 3 || ChosenTalent == -1)
            {
                return base.UseTimeMultiplier(player) * .666f;
            }
            return base.UseTimeMultiplier(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            if (player == Main.LocalPlayer)
            {
                var dX = position.X - Main.MouseWorld.X;
                var dY = position.Y - Main.MouseWorld.Y;
                var distance = Math.Sqrt(dX * dX + dY * dY);
                Vector2 speed = new Vector2(speedX, speedY);
                speed *= 1f + player.GetModPlayer<HotSPlayer>().fenixBombBuildUp * .1f;
                knockBack *= 1f + player.GetModPlayer<HotSPlayer>().fenixBombBuildUp * .1f;
                damage = (int)(damage * Math.Pow(1.1f, player.GetModPlayer<HotSPlayer>().fenixBombBuildUp));
            
            
                var speedFactor = 1.015;

                var time = Math.Log((distance / speed.Length()) * Math.Log(speedFactor) + 1) / Math.Log(speedFactor);
            
                Projectile.NewProjectile(position, speed, type, damage, knockBack, player.whoAmI, (float)time, ChosenTalent);

                if (player.HasBuff(mod.BuffType("FenixBombBuildUp")))
                {
                    player.DelBuff(player.FindBuffIndex(mod.BuffType("FenixBombBuildUp")));
                }
                player.GetModPlayer<HotSPlayer>().phaseUseTime = item.useTime;
                player.itemAnimation = 10;
                player.itemTime = 10;
            }
            return false;
        }

        public override bool CanUseItem(Player player)
        {
            return player.GetModPlayer<HotSPlayer>().phaseUseTime == 0;
        }
    }
}