using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DoomBubblesMod.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace DoomBubblesMod
{
    class DoomBubblesPlayer : ModPlayer
    {

        public float critDamage;
        public float critChanceMult = 1f;
        public float fireRate = 1f;
        public float customRadiantDamage = 1f;
        public float customSymphonicDamage = 1f;
        public int customRadiantCrit = 0;
        public int customSymphonicCrit = 0;
        
        public bool sterak;
        public bool homing;
        public bool crystalBulletBonus;
        public bool explosionBulletBonus;
        public bool luminiteBulletBonus;
        public bool sStone;
        public bool rabadon;
        public bool bloodlust;
        public bool vampireKnifeBat;

        public List<int> noManaItems = new List<int>();

        public override void ResetEffects()
        {
            sterak = false;
            homing = false;
            crystalBulletBonus = false;
            explosionBulletBonus = false;
            luminiteBulletBonus = false;
            sStone = false;
            rabadon = false;
            bloodlust = false;
            
            fireRate = 1f;
            critDamage = 0f;
            critChanceMult = 1f;
            customRadiantDamage = 1f;
            customSymphonicDamage = 1f;
            
            customRadiantCrit = 0;
            customSymphonicCrit = 0;
            vampireKnifeBat = false;
            
            noManaItems = new List<int>();
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (explosionBulletBonus && damageSource.SourceProjectileType == 286 && damageSource.SourcePlayerIndex == player.whoAmI)
            {
                return false;
            }
            if (damage >= (player.statLife / 4) && sterak)
            {
                player.AddBuff(mod.BuffType("Sterak"), 600, false);
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void PostUpdate()
        {
            base.PostUpdate();

            if (bloodlust)
            {
                player.lifeSteal = 1000f;
            }
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (player.GetModPlayer<DoomBubblesPlayer>(mod).homing)
            {
                return false;
            }
            return base.ConsumeAmmo(weapon, ammo);
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (crit)
            {
                damage += (int)(damage * (player.GetModPlayer<DoomBubblesPlayer>().critDamage / 200f));
            }
            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (crit)
            {
                damage += (int)(damage * (player.GetModPlayer<DoomBubblesPlayer>().critDamage / 200f));
            }
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }


        public override void ModifyHitPvpWithProj(Projectile proj, Player target, ref int damage, ref bool crit)
        {
            if (crit)
            {
                damage += (int)(damage * (player.GetModPlayer<DoomBubblesPlayer>().critDamage / 200f));
            }
            base.ModifyHitPvpWithProj(proj, target, ref damage, ref crit);
        }

        public override void ModifyHitPvp(Item item, Player target, ref int damage, ref bool crit)
        {
            if (crit)
            {
                damage += (int)(damage * (player.GetModPlayer<DoomBubblesPlayer>().critDamage / 200f));
            }
            base.ModifyHitPvp(item, target, ref damage, ref crit);
        }

        public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
        {
            base.ModifyWeaponDamage(item, ref add, ref mult, ref flat);
            if (player.GetModPlayer<DoomBubblesPlayer>().rabadon && item.magic)
            {
                add = 1f + (add - 1f) * 1.25f;
            }
        }
    }
}
