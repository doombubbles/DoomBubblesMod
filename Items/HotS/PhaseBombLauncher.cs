using System;
using DoomBubblesMod.Buffs;
using DoomBubblesMod.Items.Talent;
using DoomBubblesMod.Projectiles.HotS;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DoomBubblesMod.Items.HotS
{
    public class PhaseBombLauncher : ModItemWithTalents<TalentSecondaryFire, TalentSingularityCharge, TalentDivertPowerWeapons>
    {
        protected override Color? TalentColor => Color.Orange;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phase Bomb Launcher");
            Tooltip.SetDefault("Buffs Repeater Cannon attack speed for each enemy hit\n" +
                               "(Up to 10)");
            Item.SetResearchAmount(1);
        }

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.height = 34;
            Item.noMelee = true;
            Item.damage = 83;
            Item.shoot = ModContent.ProjectileType<PhaseBomb>();
            Item.shootSpeed = 7f;
            Item.useAnimation = 38;
            Item.useTime = 38;
            Item.DamageType = DamageClass.Ranged;
            Item.knockBack = 7;
            Item.useStyle = 5;
            Item.rare = 10;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 69);
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-7, -3);
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage, ref float flat)
        {
            if (ChosenTalent is 3 or -1)
            {
                flat += .5f;
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

        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type,
            int damage, float knockback)
        {
            if (player == Main.LocalPlayer)
            {
                var dX = position.X - Main.MouseWorld.X;
                var dY = position.Y - Main.MouseWorld.Y;
                var distance = Math.Sqrt(dX * dX + dY * dY);
                velocity *= 1f + player.GetModPlayer<HotSPlayer>().fenixBombBuildUp * .1f;
                knockback *= 1f + player.GetModPlayer<HotSPlayer>().fenixBombBuildUp * .1f;
                damage = (int) (damage * Math.Pow(1.1f, player.GetModPlayer<HotSPlayer>().fenixBombBuildUp));


                var speedFactor = 1.015;

                var time = Math.Log(distance / velocity.Length() * Math.Log(speedFactor) + 1) / Math.Log(speedFactor);

                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, (float) time,
                    ChosenTalent);

                if (player.HasBuff(ModContent.BuffType<FenixBombBuildUp>()))
                {
                    player.DelBuff(player.FindBuffIndex(ModContent.BuffType<FenixBombBuildUp>()));
                }

                player.GetModPlayer<HotSPlayer>().phaseUseTime = Item.useTime;
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