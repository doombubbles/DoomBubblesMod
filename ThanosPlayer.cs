﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    public class ThanosPlayer : ModPlayer
    {
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

        public override void ResetEffects()
        {
            powerStone = false;
            if (powerStoned > 0)
            {
                powerStoned--;
            }
            powerStoning.RemoveAll(i => !Main.npc[i].HasBuff(mod.BuffType("PowerStoneDebuff")));
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

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (player.GetModPlayer<ThanosPlayer>().powerStone)
            {
                if (target.HasBuff(mod.BuffType("PowerStoneDebuff")))
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoneDamage(target.whoAmI, .1f);
                }
                target.AddBuff(mod.BuffType("PowerStoneDebuff"), 300);
                if (!player.GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoning.Add(target.whoAmI);
                }
                
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            if (player.GetModPlayer<ThanosPlayer>().powerStone && proj.type != mod.ProjectileType("PowerStone"))
            {
                if (target.HasBuff(mod.BuffType("PowerStoneDebuff")))
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoneDamage(target.whoAmI, .1f);
                }
                target.AddBuff(mod.BuffType("PowerStoneDebuff"), 300);
                if (!player.GetModPlayer<ThanosPlayer>().powerStoning.Contains(target.whoAmI))
                {
                    player.GetModPlayer<ThanosPlayer>().powerStoning.Add(target.whoAmI);
                }
            }
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
    }
}