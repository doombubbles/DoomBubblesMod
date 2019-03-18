using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;

namespace DoomBubblesMod.Items
{
    public class DoomBubblesInstancedGlobalItem : GlobalItem
    {
        public byte mana;
        public byte critDamage;
        public byte hp;

        public DoomBubblesInstancedGlobalItem()
        {
            mana = 0;
            critDamage = 0;
            hp = 0;
        }

        public override bool InstancePerEntity
        {
            get { return true; }
        }

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            DoomBubblesInstancedGlobalItem myClone = (DoomBubblesInstancedGlobalItem) base.Clone(item, itemClone);
            myClone.mana = mana;
            myClone.critDamage = critDamage;
            myClone.hp = hp;
            return myClone;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!item.social && item.prefix > 0)
            {
                int bonus = mana - Main.cpItem.GetGlobalItem<DoomBubblesInstancedGlobalItem>().mana;
                if (bonus > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "ManaPrefix", "+" + bonus + " Mana");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
            }

            if (!item.social && item.prefix > 0)
            {
                int bonus = critDamage - Main.cpItem.GetGlobalItem<DoomBubblesInstancedGlobalItem>().critDamage;
                if (bonus > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "CritDamagePrefix", "+" + bonus + "% Crit Damage");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
            }

            if (!item.social && item.prefix > 0)
            {
                int bonus = hp - Main.cpItem.GetGlobalItem<DoomBubblesInstancedGlobalItem>().hp;
                if (bonus > 0)
                {
                    TooltipLine line = new TooltipLine(mod, "HpPrefix", "+" + bonus + " Max Life");
                    line.isModifier = true;
                    tooltips.Add(line);
                }
            }
        }

        public override bool NewPreReforge(Item item)
        {
            mana = 0;
            critDamage = 0;
            hp = 0;
            return base.NewPreReforge(item);
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().mana > 0)
            {
                player.statManaMax2 += mana;
            }

            if (item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().critDamage > 0)
            {
                player.GetModPlayer<DoomBubblesPlayer>().critDamage += critDamage;
            }
            
            if (item.GetGlobalItem<DoomBubblesInstancedGlobalItem>().hp > 0)
            {
                player.statLifeMax2 += hp;
            }

            base.UpdateEquip(item, player);
        }

        public override void NetSend(Item item, BinaryWriter writer)
        {
            writer.Write(mana);
            writer.Write(critDamage);
            writer.Write(hp);
        }

        public override void NetReceive(Item item, BinaryReader reader)
        {
            mana = reader.ReadByte();
            critDamage = reader.ReadByte();
            hp = reader.ReadByte();
        }


        public override float UseTimeMultiplier(Item item, Player player)
        {
            if (item.ranged)
            {
                return base.UseTimeMultiplier(item, player) * player.GetModPlayer<DoomBubblesPlayer>().fireRate;
            }
            return base.UseTimeMultiplier(item, player);
        }

        public override float MeleeSpeedMultiplier(Item item, Player player)
        {
            if (item.ranged)
            {
                return base.MeleeSpeedMultiplier(item, player) * player.GetModPlayer<DoomBubblesPlayer>().fireRate;
            }
            return base.MeleeSpeedMultiplier(item, player);
        }
    }
}