using System;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace DoomBubblesMod
{
    public class DoubleSetBonuses : ModPlayer
    {
        public string secondSetBonus = "";
        
        public override void UpdateVanityAccessories()
        {
            base.UpdateVanityAccessories();
            UpdateArmorSets();
            UpdateModArmorSets(player.armor[10], player.armor[11], player.armor[12]);
        }

        public void UpdateModArmorSets(Item head, Item body, Item legs)
        {
            String ogSetBonus = player.setBonus;
            if (head.modItem != null && head.modItem.IsArmorSet(head, body, legs))
                head.modItem.UpdateArmorSet(player);
            if (body.modItem != null && body.modItem.IsArmorSet(head, body, legs))
                body.modItem.UpdateArmorSet(player);
            if (legs.modItem != null && legs.modItem.IsArmorSet(head, body, legs))
                legs.modItem.UpdateArmorSet(player);
            if (player.setBonus != ogSetBonus)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = player.setBonus;
                player.setBonus = ogSetBonus;
            }
        }

        public void UpdateArmorSets()
        {
            
            int vanityHead = player.armor[10].headSlot;
            int vanityBody = player.armor[11].bodySlot;
            int vanityLegs = player.armor[12].legSlot;

            player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = "";
            
            if (vanityBody == 67 && vanityLegs == 56 && (vanityHead >= 103 && vanityHead <= 105))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Shroomite");
                player.shroomiteStealth = true;
            }
            if (vanityHead == 52 && vanityBody == 32 && vanityLegs == 31 || vanityHead == 53 && vanityBody == 33 && vanityLegs == 32 || (vanityHead == 54 && vanityBody == 34 && vanityLegs == 33 || vanityHead == 55 && vanityBody == 35 && vanityLegs == 34) || (vanityHead == 70 && vanityBody == 46 && vanityLegs == 42 || vanityHead == 71 && vanityBody == 47 && vanityLegs == 43 || (vanityHead == 166 && vanityBody == 173 && vanityLegs == 108 || vanityHead == 167 && vanityBody == 174 && vanityLegs == 109)))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Wood");
                ++player.statDefense;
            }
            if (vanityHead == 1 && vanityBody == 1 && vanityLegs == 1 || (vanityHead == 72 || vanityHead == 2) && (vanityBody == 2 && vanityLegs == 2) || vanityHead == 47 && vanityBody == 28 && vanityLegs == 27)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.MetalTier1");
                player.statDefense += 2;
            }
            if (vanityHead == 3 && vanityBody == 3 && vanityLegs == 3 || (vanityHead == 73 || vanityHead == 4) && (vanityBody == 4 && vanityLegs == 4) || (vanityHead == 48 && vanityBody == 29 && vanityLegs == 28 || vanityHead == 49 && vanityBody == 30 && vanityLegs == 29))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.MetalTier2");
                player.statDefense += 3;
            }
            if (vanityHead == 188 && vanityBody == 189 && vanityLegs == 129)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Fossil");
                player.thrownCost50 = true;
            }
            if (vanityHead == 50 && vanityBody == 31 && vanityLegs == 30)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Platinum");
                player.statDefense += 4;
            }
            if (vanityHead == 112 && vanityBody == 75 && vanityLegs == 64)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Pumpkin");
                player.meleeDamage += 0.1f;
                player.magicDamage += 0.1f;
                player.rangedDamage += 0.1f;
                player.thrownDamage += 0.1f;
            }
            if (vanityHead == 22 && vanityBody == 14 && vanityLegs == 14)
            {
                player.thrownCost33 = true;
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Ninja");
            }
            if (vanityHead == 157 && vanityBody == 105 && vanityLegs == 98)
            {
                int num1 = 0;
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.BeetleDamage");
                player.beetleOffense = true;
                player.beetleCounter -= 3f;
                player.beetleCounter -= (float) (player.beetleCountdown / 10);
                ++player.beetleCountdown;
                if ((double) player.beetleCounter < 0.0)
                    player.beetleCounter = 0.0f;
                int num2 = 400;
                int num3 = 1200;
                int num4 = 4600;
                if ((double) player.beetleCounter > (double) (num2 + num3 + num4 + num3))
                    player.beetleCounter = (float) (num2 + num3 + num4 + num3);
                if ((double) player.beetleCounter > (double) (num2 + num3 + num4))
                {
                    player.AddBuff(100, 5, false);
                    num1 = 3;
                }
                else if ((double) player.beetleCounter > (double) (num2 + num3))
                {
                    player.AddBuff(99, 5, false);
                    num1 = 2;
                }
                else if ((double) player.beetleCounter > (double) num2)
                {
                    player.AddBuff(98, 5, false);
                    num1 = 1;
                }
                if (num1 < player.beetleOrbs)
                    player.beetleCountdown = 0;
                else if (num1 > player.beetleOrbs)
                    player.beetleCounter += 200f;
                if (num1 != player.beetleOrbs && player.beetleOrbs > 0)
                {
                    for (int b = 0; b < 22; ++b)
                    {
                        if (player.buffType[b] >= 98 && player.buffType[b] <= 100 && player.buffType[b] != 97 + num1)
                            player.DelBuff(b);
                    }
                }
            }
            else if (vanityHead == 157 && vanityBody == 106 && vanityLegs == 98)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.BeetleDefense");
                player.beetleDefense = true;
                ++player.beetleCounter;
                int num = 180;
                if ((double) player.beetleCounter >= (double) num)
                {
                    if (player.beetleOrbs > 0 && player.beetleOrbs < 3)
                    {
                        for (int b = 0; b < 22; ++b)
                        {
                            if (player.buffType[b] >= 95 && player.buffType[b] <= 96)
                                player.DelBuff(b);
                        }
                    }
                    if (player.beetleOrbs < 3)
                    {
                        player.AddBuff(95 + player.beetleOrbs, 5, false);
                        player.beetleCounter = 0.0f;
                    }
                    else
                        player.beetleCounter = (float) num;
                }
            }
            if (!player.beetleDefense && !player.beetleOffense)
            {
                player.beetleCounter = 0.0f;
            }
            else
            {
                ++player.beetleFrameCounter;
                if (player.beetleFrameCounter >= 1)
                {
                    player.beetleFrameCounter = 0;
                    ++player.beetleFrame;
                    if (player.beetleFrame > 2)
                        player.beetleFrame = 0;
                }
                for (int beetleOrbs = player.beetleOrbs; beetleOrbs < 3; ++beetleOrbs)
                {
                    player.beetlePos[beetleOrbs].X = 0.0f;
                    player.beetlePos[beetleOrbs].Y = 0.0f;
                }
                for (int index = 0; index < player.beetleOrbs; ++index)
                {
                    player.beetlePos[index] += player.beetleVel[index];
                    player.beetleVel[index].X += (float) Main.rand.Next(-100, 101) * 0.005f;
                    player.beetleVel[index].Y += (float) Main.rand.Next(-100, 101) * 0.005f;
                    float x1 = player.beetlePos[index].X;
                    float y1 = player.beetlePos[index].Y;
                    float num1 = (float) Math.Sqrt((double) x1 * (double) x1 + (double) y1 * (double) y1);
                    if ((double) num1 > 100.0)
                    {
                        float num2 = 20f / num1;
                        float num3 = x1 * -num2;
                        float num4 = y1 * -num2;
                        int num5 = 10;
                        player.beetleVel[index].X = (player.beetleVel[index].X * (float) (num5 - 1) + num3) / (float) num5;
                        player.beetleVel[index].Y = (player.beetleVel[index].Y * (float) (num5 - 1) + num4) / (float) num5;
                    }
                    else if ((double) num1 > 30.0)
                    {
                        float num2 = 10f / num1;
                        float num3 = x1 * -num2;
                        float num4 = y1 * -num2;
                        int num5 = 20;
                        player.beetleVel[index].X = (player.beetleVel[index].X * (float) (num5 - 1) + num3) / (float) num5;
                        player.beetleVel[index].Y = (player.beetleVel[index].Y * (float) (num5 - 1) + num4) / (float) num5;
                    }
                    float x2 = player.beetleVel[index].X;
                    float y2 = player.beetleVel[index].Y;
                    if (Math.Sqrt((double) x2 * (double) x2 + (double) y2 * (double) y2) > 2.0)
                        player.beetleVel[index] *= 0.9f;
                    player.beetlePos[index] -= player.velocity * 0.25f;
                }
            }
            if (vanityHead == 14 && (vanityBody >= 58 && vanityBody <= 63 || vanityBody == 167))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Wizard");
                player.magicCrit += 10;
            }
            if (vanityHead == 159 && (vanityBody >= 58 && vanityBody <= 63 || vanityBody == 167))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.MagicHat");
                player.statManaMax2 += 60;
            }
            if ((vanityHead == 5 || vanityHead == 74) && (vanityBody == 5 || vanityBody == 48) && (vanityLegs == 5 || vanityLegs == 44))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.ShadowScale");
                player.moveSpeed += 0.15f;
            }
            if (vanityHead == 57 && vanityBody == 37 && vanityLegs == 35)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Crimson");
                player.crimsonRegen = true;
            }
            if (vanityHead == 101 && vanityBody == 66 && vanityLegs == 55)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.SpectreHealing");
                player.ghostHeal = true;
            }
            if (vanityHead == 156 && vanityBody == 66 && vanityLegs == 55)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.SpectreDamage");
                player.ghostHurt = true;
            }
            if (vanityHead == 6 && vanityBody == 6 && vanityLegs == 6)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Meteor");
                player.spaceGun = true;
            }
            if (vanityHead == 46 && vanityBody == 27 && vanityLegs == 26)
            {
                player.frostArmor = true;
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Frost");
                player.frostBurn = true;
            }
            if ((vanityHead == 75 || vanityHead == 7) && (vanityBody == 7 && vanityLegs == 7))
            {
                player.boneArmor = true;
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Bone");
                player.ammoCost80 = true;
            }
            if ((vanityHead == 76 || vanityHead == 8) && (vanityBody == 49 || vanityBody == 8) && (vanityLegs == 45 || vanityLegs == 8))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Jungle");
                player.manaCost -= 0.16f;
            }
            if (vanityHead == 9 && vanityBody == 9 && vanityLegs == 9)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Molten");
                player.meleeDamage += 0.17f;
            }
            if (vanityHead == 11 && vanityBody == 20 && vanityLegs == 19)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Mining");
                player.pickSpeed -= 0.3f;
            }
            if ((vanityHead == 78 || vanityHead == 79 || vanityHead == 80) && (vanityBody == 51 && vanityLegs == 47))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Chlorophyte");
                player.AddBuff(60, 18000, true);
            }
            else if (player.crystalLeaf)
            {
                for (int b = 0; b < 22; ++b)
                {
                    if (player.buffType[b] == 60)
                        player.DelBuff(b);
                }
            }
            if (vanityHead == 99 && vanityBody == 65 && vanityLegs == 54)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Turtle");
                player.thorns = 1f;
                player.turtleThorns = true;
            }
            if (vanityBody == 17 && vanityLegs == 16)
            {
                if (vanityHead == 29)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.CobaltCaster");
                    player.manaCost -= 0.14f;
                }
                else if (vanityHead == 30)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.CobaltMelee");
                    player.meleeSpeed += 0.15f;
                }
                else if (vanityHead == 31)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.CobaltRanged");
                    player.ammoCost80 = true;
                }
            }
            if (vanityBody == 18 && vanityLegs == 17)
            {
                if (vanityHead == 32)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.MythrilCaster");
                    player.manaCost -= 0.17f;
                }
                else if (vanityHead == 33)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.MythrilMelee");
                    player.meleeCrit += 5;
                }
                else if (vanityHead == 34)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.MythrilRanged");
                    player.ammoCost80 = true;
                }
            }
            if (vanityBody == 19 && vanityLegs == 18)
            {
                if (vanityHead == 35)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.AdamantiteCaster");
                    player.manaCost -= 0.19f;
                }
                else if (vanityHead == 36)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.AdamantiteMelee");
                    player.meleeSpeed += 0.18f;
                    player.moveSpeed += 0.18f;
                }
                else if (vanityHead == 37)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.AdamantiteRanged");
                    player.ammoCost75 = true;
                }
            }
            if (vanityBody == 54 && vanityLegs == 49 && (vanityHead == 83 || vanityHead == 84 || vanityHead == 85))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Palladium");
                player.onHitRegen = true;
            }
            if (vanityBody == 55 && vanityLegs == 50 && (vanityHead == 86 || vanityHead == 87 || vanityHead == 88))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Orichalcum");
                player.onHitPetal = true;
            }
            if (vanityBody == 56 && vanityLegs == 51 && (vanityHead == 89 || vanityHead == 90 || vanityHead == 91))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Titanium");
                player.onHitDodge = true;
            }
            if (vanityBody == 24 && vanityLegs == 23)
            {
                if (vanityHead == 42)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.HallowCaster");
                    player.manaCost -= 0.2f;
                }
                else if (vanityHead == 43)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.HallowMelee");
                    player.meleeSpeed += 0.19f;
                    player.moveSpeed += 0.19f;
                }
                else if (vanityHead == 41)
                {
                    player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.HallowRanged");
                    player.ammoCost75 = true;
                }
            }
            if (vanityHead == 82 && vanityBody == 53 && vanityLegs == 48)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Tiki");
                ++player.maxMinions;
            }
            if (vanityHead == 134 && vanityBody == 95 && vanityLegs == 79)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Spooky");
                player.minionDamage += 0.25f;
            }
            if (vanityHead == 160 && vanityBody == 168 && vanityLegs == 103)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Bee");
                player.minionDamage += 0.1f;
                if (player.itemAnimation > 0 && player.inventory[player.selectedItem].type == 1121)
                    AchievementsHelper.HandleSpecialEvent(player, 3);
            }
            if (vanityHead == 162 && vanityBody == 170 && vanityLegs == 105)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Spider");
                player.minionDamage += 0.12f;
            }
            if (vanityHead == 171 && vanityBody == 177 && vanityLegs == 112)
            {
                player.setSolar = true;
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Solar");
                ++player.solarCounter;
                int num = 240;
                if (player.solarCounter >= num)
                {
                    if (player.solarShields > 0 && player.solarShields < 3)
                    {
                        for (int b = 0; b < 22; ++b)
                        {
                            if (player.buffType[b] >= 170 && player.buffType[b] <= 171)
                                player.DelBuff(b);
                        }
                    }
                    if (player.solarShields < 3)
                    {
                        player.AddBuff(170 + player.solarShields, 5, false);
                        for (int index = 0; index < 16; ++index)
                        {
                            Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 6, 0.0f, 0.0f, 100, new Color(), 1f)];
                            dust.noGravity = true;
                            dust.scale = 1.7f;
                            dust.fadeIn = 0.5f;
                            dust.velocity *= 5f;
                            dust.shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
                        }
                        player.solarCounter = 0;
                    }
                    else
                        player.solarCounter = num;
                }
                for (int solarShields = player.solarShields; solarShields < 3; ++solarShields)
                    player.solarShieldPos[solarShields] = Vector2.Zero;
                for (int index = 0; index < player.solarShields; ++index)
                {
                    player.solarShieldPos[index] += player.solarShieldVel[index];
                    Vector2 vector2 = ((float) ((double) player.miscCounter / 100.0 * 6.28318548202515 + (double) index * (6.28318548202515 / (double) player.solarShields))).ToRotationVector2() * 6f;
                    vector2.X = (float) (player.direction * 20);
                    player.solarShieldVel[index] = (vector2 - player.solarShieldPos[index]) * 0.2f;
                }
                if (player.dashDelay >= 0)
                {
                    player.solarDashing = false;
                    player.solarDashConsumedFlare = false;
                }
                if (player.solarShields > 0 | (player.solarDashing && player.dashDelay < 0))
                    player.dash = 3;
            }
            else
                player.solarCounter = 0;
            if (vanityHead == 169 && vanityBody == 175 && vanityLegs == 110)
            {
                player.setVortex = true;
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Vortex", (object) Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
            }
            else
                player.vortexStealthActive = false;
            if (vanityHead == 170 && vanityBody == 176 && vanityLegs == 111)
            {
                if (player.nebulaCD > 0)
                    --player.nebulaCD;
                player.setNebula = true;
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Nebula");
            }
            if (vanityHead == 189 && vanityBody == 190 && vanityLegs == 130)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Stardust", (object) Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
                player.setStardust = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(187) == -1)
                        player.AddBuff(187, 3600, true);
                    if (player.ownedProjectileCounts[623] < 1)
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0.0f, -1f, 623, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f);
                }
            }
            else if (player.FindBuffIndex(187) != -1)
                player.DelBuff(player.FindBuffIndex(187));
            if (vanityHead == 200 && vanityBody == 198 && vanityLegs == 142)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.Forbidden", (object) Language.GetTextValue(Main.ReversedUpDownArmorSetBonuses ? "Key.UP" : "Key.DOWN"));
                player.setForbidden = true;
                player.UpdateForbiddenSetLock();
                Lighting.AddLight(player.Center, 0.8f, 0.7f, 0.2f);
            }
            if (vanityHead == 204 && vanityBody == 201 && vanityLegs == 145)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.SquireTier2");
                player.setSquireT2 = true;
                ++player.maxTurrets;
            }
            if (vanityHead == 203 && vanityBody == 200 && vanityLegs == 144)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.ApprenticeTier2");
                player.setApprenticeT2 = true;
                ++player.maxTurrets;
            }
            if (vanityHead == 205 && vanityBody == 202 && (vanityLegs == 147 || vanityLegs == 146))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.HuntressTier2");
                player.setHuntressT2 = true;
                ++player.maxTurrets;
            }
            if (vanityHead == 206 && vanityBody == 203 && vanityLegs == 148)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.MonkTier2");
                player.setMonkT2 = true;
                ++player.maxTurrets;
            }
            if (vanityHead == 210 && vanityBody == 204 && vanityLegs == 152)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.SquireTier3");
                player.setSquireT3 = true;
                player.setSquireT2 = true;
                ++player.maxTurrets;
            }
            if (vanityHead == 211 && vanityBody == 205 && vanityLegs == 153)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.ApprenticeTier3");
                player.setApprenticeT3 = true;
                player.setApprenticeT2 = true;
                ++player.maxTurrets;
            }
            if (vanityHead == 212 && vanityBody == 206 && (vanityLegs == 154 || vanityLegs == 155))
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.HuntressTier3");
                player.setHuntressT3 = true;
                player.setHuntressT2 = true;
                ++player.maxTurrets;
            }
            if (vanityHead == 213 && vanityBody == 207 && vanityLegs == 156)
            {
                player.GetModPlayer<DoubleSetBonuses>().secondSetBonus = Language.GetTextValue("ArmorSetBonus.MonkTier3");
                player.setMonkT3 = true;
                player.setMonkT2 = true;
                ++player.maxTurrets;
            }
            //ItemLoader.UpdateArmorSet(player, player.armor[0], player.armor[1], player.armor[2]);
        }
        
        
    }
}