using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    internal class DoomBubblesPlayer : ModPlayer
    {
        public bool bloodlust;
        public float critChanceMult = 1f;
        public bool crystalBulletBonus;
        public int emblem;
        public bool explosionBulletBonus;

        public bool homing;
        public bool luminiteBulletBonus;

        public List<int> noManaItems = new List<int>();
        public bool sStone;
        public bool united;
        public bool vampireKnifeBat;

        public override void ResetEffects()
        {
            homing = false;
            crystalBulletBonus = false;
            explosionBulletBonus = false;
            luminiteBulletBonus = false;
            sStone = false;
            bloodlust = false;
            critChanceMult = 1f;
            emblem = 0;
            vampireKnifeBat = false;
            united = false;

            noManaItems = new List<int>();
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit,
            ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (explosionBulletBonus && damageSource.SourceProjectileType == 286 &&
                damageSource.SourcePlayerIndex == player.whoAmI)
            {
                return false;
            }

            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound,
                ref genGore, ref damageSource);
        }

        public override void PostUpdateEquips()
        {
            var uniteds = -1;
            for (var i = 0; i < Main.player.Length; i++)
            {
                var player = Main.player[i];
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
            var item = new Item();
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