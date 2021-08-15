using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    public class DoomBubblesPlayer : ModPlayer
    {
        public bool noManaFlower;
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
            noManaFlower = false;
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
                damageSource.SourcePlayerIndex == Player.whoAmI)
            {
                return false;
            }

            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound,
                ref genGore, ref damageSource);
        }

        public override void PostUpdateEquips()
        {
            var uniteds = Main.player.Count(p => p.active && !p.dead && p.GetModPlayer<DoomBubblesPlayer>().united);

            Player.GetDamage(DamageClass.Generic) += .1f * uniteds;
            if (noManaFlower)
            {
                Player.manaFlower = false;
            }
            
        }

        public override void ModifyStartingInventory(IReadOnlyDictionary<string, List<Item>> itemsByMod, bool mediumCoreDeath)
        {
            var item = new Item();
            item.SetDefaults(ItemID.CopperBroadsword);
            item.Prefix(-1);
            itemsByMod["Terraria"][0].TurnToAir();
            itemsByMod["Terraria"][0] = item;
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo)
        {
            return !Player.GetModPlayer<DoomBubblesPlayer>().homing && base.ConsumeAmmo(weapon, ammo);
        }
    }
}