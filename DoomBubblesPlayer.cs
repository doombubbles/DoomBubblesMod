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
        public float critChanceMult = 1f;
        public float customRadiantDamage = 1f;
        public float customSymphonicDamage = 1f;
        public int customRadiantCrit = 0;
        public int customSymphonicCrit = 0;
        
        public bool homing;
        public bool crystalBulletBonus;
        public bool explosionBulletBonus;
        public bool luminiteBulletBonus;
        public bool sStone;
        public bool bloodlust;
        public bool vampireKnifeBat;
        public bool united;
        public int emblem;
        public float potionHealing;

        public List<int> noManaItems = new List<int>();

        public override void ResetEffects()
        {
            homing = false;
            crystalBulletBonus = false;
            explosionBulletBonus = false;
            luminiteBulletBonus = false;
            sStone = false;
            bloodlust = false;
            critChanceMult = 1f;
            customRadiantDamage = 1f;
            customSymphonicDamage = 1f;
            emblem = 0;
            customRadiantCrit = 0;
            customSymphonicCrit = 0;
            vampireKnifeBat = false;
            united = false;
            
            noManaItems = new List<int>();
            potionHealing = 1f;
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (explosionBulletBonus && damageSource.SourceProjectileType == 286 && damageSource.SourcePlayerIndex == player.whoAmI)
            {
                return false;
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void PostUpdateEquips()
        {
            int uniteds = -1;
            for (var i = 0; i < Main.player.Length; i++)
            {
                Player player = Main.player[i];
                if (player.active && !player.dead && player.GetModPlayer<DoomBubblesPlayer>().united)
                {
                    uniteds++;
                }
            }

            player.allDamage += .1f * uniteds;
        }

        public override void PostUpdate()
        {
            base.PostUpdate();

            if (bloodlust)
            {
                player.lifeSteal = 1000f;
            }
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumcoreDeath)
        {
            Item item = new Item();
            item.SetDefaults(3508);
            item.Prefix(-1);
            player.inventory[0].TurnToAir();
            player.inventory[0] = item;
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            if (player.GetModPlayer<DoomBubblesPlayer>().homing)
            {
                return false;
            }
            return base.ConsumeAmmo(weapon, ammo);
        }
        
        
    }
}
