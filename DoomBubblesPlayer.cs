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

namespace DoomBubblesMod
{
    class DoomBubblesPlayer : ModPlayer
    {

        public float magicMult = 1f;
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
        
        public int botrk;
        public int critCombo = 0;

        public int gem = -1;
        public int tbMouseX;
        public int tbMouseY;
        public bool soulStone;
        public Tile soulStoneTile = new Tile();
        public bool soulStoneTileActive;
        public int[] timeHealth = new int[302];
        public int powerStoned;
        public int powerStoneCharge;
        public bool powerStone;
        public List<int> powerStoning = new List<int>();

        public List<int> noManaItems = new List<int>();

        public override void ResetEffects()
        {
            sterak = false;
            homing = false;
            powerStone = false;
            crystalBulletBonus = false;
            explosionBulletBonus = false;
            luminiteBulletBonus = false;
            sStone = false;
            magicMult = 1f;
            fireRate = 1f;
            critDamage = 0f;
            critChanceMult = 1f;
            customRadiantDamage = 1f;
            customSymphonicDamage = 1f;
            customRadiantCrit = 0;
            customSymphonicCrit = 0;
            if (powerStoned > 0)
            {
                powerStoned--;
            }

            powerStoning.RemoveAll(i => !Main.npc[i].HasBuff(mod.BuffType("PowerStoneDebuff")));

            if (botrk != 0)
            {
                botrk -= 1;
            }
            if (botrk < 0)
            {
                botrk = 0;
            }
            
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

        public void powerStoneDamage(int npcId, float multiplier)
        {
            NPC target = Main.npc[npcId];
            if (!target.immortal)
            {
                int damage = 10 + Math.Min((int) (target.lifeMax * .001), 490);
                Projectile.NewProjectileDirect(target.Center, new Vector2(0,0), mod.ProjectileType("PowerStone"), damage, 0, player.whoAmI);
            }
        }

        public override void PreUpdate()
        {
            if (Main.time % 60 == 0)
            {
                foreach (int i in powerStoning)
                {
                    powerStoneDamage(i, 1f);
                }  
            }
            
            if (soulStone)
            {
                player.chestX = (int) (player.position.X / 16f);
                player.chestY = (int) (player.position.Y / 16f);
                soulStoneTile = Main.tile[(int) (player.position.X / 16f), (int) (player.position.Y / 16f)];
                soulStoneTileActive = soulStoneTile.active();
                if (!soulStoneTileActive)
                {
                    soulStoneTile.active(true);
                }
            }

            
            for (int i = 300; i > 0; i--)
            {
                timeHealth[i] = timeHealth[i-1];
            }

            timeHealth[0] = player.statLife;
            
            base.PreUpdate();
        }


        public override void PostUpdate()
        {
            if (soulStone)
            {
                if (!soulStoneTileActive)
                {
                    soulStoneTile.active(false);
                }
            }
            
            
            if (player.chest != -3)
            {
                soulStone = false;
            }
            
            base.PostUpdate();
        }
        
        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            base.UpdateEquips(ref wallSpeedBuff, ref tileSpeedBuff, ref tileRangeBuff);
            
            player.magicDamage += (player.magicDamage - 1f) * (magicMult - 1f);
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
            if (player.GetModPlayer<DoomBubblesPlayer>().powerStone)
            {
                if (target.HasBuff(mod.BuffType("PowerStoneDebuff")))
                {
                    player.GetModPlayer<DoomBubblesPlayer>().powerStoneDamage(target.whoAmI, .1f);
                }
                target.AddBuff(mod.BuffType("PowerStoneDebuff"), 300);
                if (!player.GetModPlayer<DoomBubblesPlayer>().powerStoning.Contains(target.whoAmI))
                {
                    player.GetModPlayer<DoomBubblesPlayer>().powerStoning.Add(target.whoAmI);
                }
                
            }
            if (crit)
            {
                damage += (int)(damage * (player.GetModPlayer<DoomBubblesPlayer>().critDamage / 200f));
            }
            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (player.GetModPlayer<DoomBubblesPlayer>().powerStone && proj.type != mod.ProjectileType("PowerStone"))
            {
                if (target.HasBuff(mod.BuffType("PowerStoneDebuff")))
                {
                    player.GetModPlayer<DoomBubblesPlayer>().powerStoneDamage(target.whoAmI, .1f);
                }
                target.AddBuff(mod.BuffType("PowerStoneDebuff"), 300);
                if (!player.GetModPlayer<DoomBubblesPlayer>().powerStoning.Contains(target.whoAmI))
                {
                    player.GetModPlayer<DoomBubblesPlayer>().powerStoning.Add(target.whoAmI);
                }
            }
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


        public override void UpdateLifeRegen()
        {
            base.UpdateLifeRegen();
        }
    }
}
