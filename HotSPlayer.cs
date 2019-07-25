using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    public class HotSPlayer : ModPlayer
    {
        public int fenixBombBuildUp;
        public int fenixRepeaterBuff;
        
        public int phaseUseTime;
        
        public List<int> pylons = new List<int>();
        
        public bool photonCannon;
        
        public int shieldCapacitor;
        public int shieldCapacitorMax;
        public int shieldCapacitorDamageCounter;
        public int shieldCapacitorChosenTalent;

        public bool verdant;
        public bool manaTap;
        public bool superVerdant;
        
        public int convection;

        public override void ResetEffects()
        {
            if (!player.HasBuff(mod.BuffType("FenixBombBuildUp")))
            {
                fenixBombBuildUp = 0;
            }
            if (!player.HasBuff(mod.BuffType("FenixRepeaterBuff")))
            {
                fenixRepeaterBuff = 0;
            }
            if (!player.HasBuff(mod.BuffType("PhotonCannon")))
            {
                photonCannon = false;
            }
            if (!player.HasBuff(mod.BuffType("Convection")))
            {
                convection = 0;
            }
            
            if (phaseUseTime > 0)
            {
                phaseUseTime--;
            }

            if (shieldCapacitorDamageCounter > 0)
            {
                shieldCapacitorDamageCounter--;
            }
            if (shieldCapacitorDamageCounter == 0 && shieldCapacitor < shieldCapacitorMax && (int)Main.time % 12 == 0)
            {
                if (shieldCapacitor == 0)
                {
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/ShieldRecharge"));
                }
                shieldCapacitor++;
            }
            if (shieldCapacitor > 0)
            {
                if (shieldCapacitorChosenTalent == 2 || shieldCapacitorChosenTalent == -1)
                {
                    player.endurance += .25f;
                }
                if (shieldCapacitorChosenTalent == 3 || shieldCapacitorChosenTalent == -1)
                {
                    player.allDamage += .15f;
                }
            }
            if (shieldCapacitorMax < shieldCapacitor)
            {
                shieldCapacitor = shieldCapacitorMax;
            }
            shieldCapacitorChosenTalent = 0;
            shieldCapacitorMax = 0;

            verdant = false;
            manaTap = false;
            superVerdant = false;
        }


        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage,
            ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (shieldCapacitorMax > 0 && !player.immune)
            {
                damage = (int) Main.CalculatePlayerDamage(damage, player.statDefense);
                if (damage > player.statLife && (shieldCapacitorChosenTalent == 1 || shieldCapacitorChosenTalent == -1) &&
                    !player.HasBuff(mod.BuffType("UnconqueredSpiritCooldown")))
                {
                    shieldCapacitor = shieldCapacitorMax;
                    player.AddBuff(mod.BuffType("UnconqueredSpiritCooldown"), 60 * 120);
                    player.immune = true;
                    player.immuneTime = 40;
                    if (player.longInvince)
                    {
                        player.immuneTime += 40;
                    }
                    
                    for (int i = 0; i <= 360; i += 3)
                    {
                        double rad = (Math.PI * i) / 180;
                        float dX = (float) (9 * Math.Cos(rad));
                        float dY = (float) (9 * Math.Sin(rad));
                        Dust dust = Dust.NewDustPerfect(player.Center, mod.DustType("Green182"), new Vector2(dX, dY), 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    for (int i = 0; i <= 360; i += 3)
                    {
                        double rad = (Math.PI * i) / 180;
                        float dX = (float) (9 * Math.Cos(rad));
                        float dY = (float) (9 * Math.Sin(rad));
                        Dust dust = Dust.NewDustPerfect(new Vector2(player.Center.X + (11 * dX), player.Center.Y + (11 * dY)), mod.DustType("LightBlue182"), new Vector2(-dX, -dY), 0, default, 1.5f);
                        dust.noGravity = true;
                    }
                    Main.PlaySound(SoundLoader.customSoundType, (int)player.Center.X, (int)player.Center.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/UnconqueredSpirit"));
                    
                    return false;
                }
                if (shieldCapacitor > 0)
                {
                    int startShields = shieldCapacitor;
                    damage -= shieldCapacitor;
                    if (damage >= 0)
                    {
                        shieldCapacitor = 0;
                    }
                    else
                    {
                        shieldCapacitor = damage * -1;
                        damage = 0;
                    }
                
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), Color.Blue, startShields - shieldCapacitor, crit);
                    Main.PlaySound(SoundLoader.customSoundType, (int) player.position.X, (int) player.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/ShieldHit"));
                    if (damage == 0)
                    {
                        player.immune = true;
                        player.immuneTime = 40;
                        if (player.longInvince)
                        {
                            player.immuneTime += 40;
                        }
                    }
                }
            
                shieldCapacitorDamageCounter = 300;
                customDamage = true;
            }

            if (player.immune)
            {
                return false;
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override void UpdateDead()
        {
            convection = 0;
        }

        public override void ModifyWeaponDamage(Item item, ref float add, ref float mult, ref float flat)
        {
            if (item.magic && manaTap && item.mana > 0)
            {
                flat += item.mana;
            }
        }
    }
}