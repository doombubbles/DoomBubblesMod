using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoomBubblesMod.Items.HotS
{
    public class RepeaterCannon : TalentItem
    {
        private short m_Shot = 0;
        
        public override string Talent1Name => "TalentMobileOffense";
        public override string Talent2Name => "TalentOffensiveCadence";
        public override string Talent3Name => "TalentArsenalOvercharge";
        protected override Color? TalentColor => Color.Orange;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Repeater Cannon");
            Tooltip.SetDefault("Shots build up to empower your next Phase Bomb\n" +
                               "(Stacks up to 10)");
        }

        public override void SetDefaults()
        {
            item.width = 74;
            item.height = 26;
            item.noMelee = true;
            item.damage = 83;
            item.shoot = mod.ProjectileType("Repeater");
            item.shootSpeed = 15f;
            item.useAnimation = 15;
            item.useTime = 15;
            item.ranged = true;
            item.knockBack = 5;
            item.useStyle = 5;
            item.rare = 10;
            item.autoReuse = true;
            item.value = Item.buyPrice(0, 69, 0, 0);
        }
        
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 3);
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult)
        {
            if (ChosenTalent == 1 || ChosenTalent == -1)
            {
                add += player.velocity.Length() / 50f;
            }

        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage,
            ref float knockBack)
        {
            m_Shot++;

            float pitch = 0f;
            
            if ((ChosenTalent == 2 || ChosenTalent == -1) && m_Shot == 3)
            {
                pitch = -.25f;
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), mod.ProjectileType("RepeaterBig"), damage * 2, knockBack * 2f, player.whoAmI, 0, ChosenTalent);
            }
            else
            {
                Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI, 0, ChosenTalent);
            }

            Main.PlaySound(SoundLoader.customSoundType, (int)position.X, (int)position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Repeater" + m_Shot), 1f, pitch);
            if (m_Shot >= 3)
            {
                m_Shot = 0;
            }
            return false;
        }

        public override float UseTimeMultiplier(Player player)
        {
            return base.UseTimeMultiplier(player) * (1f + .1f * player.GetModPlayer<HotSPlayer>().fenixRepeaterBuff);
        }
    }
}