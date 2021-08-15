using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using SoundType = Terraria.ModLoader.SoundType;

namespace DoomBubblesMod.Items.Talent
{
    public abstract class TalentItem : ModItem
    {
        public abstract string Talent1Name { get; }
        public abstract string Talent2Name { get; }
        public abstract string Talent3Name { get; }
        protected abstract Color? TalentColor { get; }

        private bool Talent1 { get; set; }
        private bool Talent2 { get; set; }
        private bool Talent3 { get; set; }
        public short ChosenTalent { get; private set; }

        public override TagCompound Save()
        {
            var tag = new TagCompound();
            tag.Set("talent1", Talent1);
            tag.Set("talent2", Talent2);
            tag.Set("talent3", Talent3);
            tag.Set("chosenTalent", ChosenTalent);
            return tag;
        }

        public override void Load(TagCompound tag)
        {
            Talent1 = tag.GetBool("talent1");
            Talent2 = tag.GetBool("talent2");
            Talent3 = tag.GetBool("talent3");
            ChosenTalent = tag.GetShort("chosenTalent");
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(Talent1);
            writer.Write(Talent2);
            writer.Write(Talent3);
            writer.Write(ChosenTalent);
        }

        public override void NetReceive(BinaryReader reader)
        {
            Talent1 = reader.ReadBoolean();
            Talent2 = reader.ReadBoolean();
            Talent3 = reader.ReadBoolean();
            ChosenTalent = reader.ReadInt16();
        }

        public override bool CanRightClick()
        {
            if (Item.playerIndexTheItemIsReservedFor == Main.myPlayer)
            {
                if (Main.mouseItem.type == ModContent.Find<ModItem>(Talent1Name).Type ||
                    Main.mouseItem.type == ModContent.Find<ModItem>(Talent2Name).Type ||
                    Main.mouseItem.type == ModContent.Find<ModItem>(Talent3Name).Type)
                {
                    return true;
                }

                if ((Talent1 ? 1 : 0) + (Talent2 ? 1 : 0) + (Talent3 ? 1 : 0) > 1)
                {
                    return ChosenTalent >= 0;
                }
            }

            return false;
        }

        public override void RightClick(Player player)
        {
            if (Item.playerIndexTheItemIsReservedFor == Main.myPlayer)
            {
                if (Main.mouseItem.type == ModContent.Find<ModItem>(Talent1Name).Type && !Talent1)
                {
                    Talent1 = true;
                    ChosenTalent = 1;
                    Main.mouseItem = new Item();
                }
                else if (Main.mouseItem.type == ModContent.Find<ModItem>(Talent2Name).Type && !Talent2)
                {
                    Talent2 = true;
                    ChosenTalent = 2;
                    Main.mouseItem = new Item();
                }
                else if (Main.mouseItem.type == ModContent.Find<ModItem>(Talent3Name).Type && !Talent3)
                {
                    Talent3 = true;
                    ChosenTalent = 3;
                    Main.mouseItem = new Item();
                }
                else if (Main.mouseItem.type == ItemID.GravityGlobe && ChosenTalent != -1 && Talent1 && Talent2 &&
                         Talent3)
                {
                    ChosenTalent = -1;
                }
                else if (Main.mouseItem.type == 0)
                {
                    int ogTalent = ChosenTalent;
                    if (ChosenTalent == 1)
                    {
                        if (Talent2)
                        {
                            ChosenTalent = 2;
                        }
                        else if (Talent3)
                        {
                            ChosenTalent = 3;
                        }
                    }
                    else if (ChosenTalent == 2)
                    {
                        if (Talent3)
                        {
                            ChosenTalent = 3;
                        }
                        else if (Talent1)
                        {
                            ChosenTalent = 1;
                        }
                    }
                    else if (ChosenTalent == 3)
                    {
                        if (Talent1)
                        {
                            ChosenTalent = 1;
                        }
                        else if (Talent2)
                        {
                            ChosenTalent = 2;
                        }
                    }

                    if (ChosenTalent != ogTalent)
                    {
                        SoundEngine.PlaySound(SoundLoader.customSoundType, (int) Main.player[Item.playerIndexTheItemIsReservedFor].position.X,
                            (int) Main.player[Item.playerIndexTheItemIsReservedFor].position.Y,
                            Mod.GetSoundSlot(SoundType.Custom, "Sounds/TalentChange"));
                    }
                }
            }
        }

        public override bool ConsumeItem(Player player)
        {
            return false;
        }


        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (ChosenTalent == 1 || ChosenTalent == -1)
            {
                tooltips.Add(new TooltipLine(Mod, "talent1",
                    ModContent.Find<ModItem>(Talent1Name).DisplayName.GetDefault().Replace("Talent: ", "") + ":")
                {
                    overrideColor = TalentColor
                });
                tooltips.Add(new TooltipLine(Mod, "1", ModContent.Find<ModItem>(Talent1Name).Tooltip.GetDefault().Split('\n')[1]));
            }

            if (ChosenTalent == 2 || ChosenTalent == -1)
            {
                tooltips.Add(new TooltipLine(Mod, "talent2",
                    ModContent.Find<ModItem>(Talent2Name).DisplayName.GetDefault().Replace("Talent: ", "") + ":")
                {
                    overrideColor = TalentColor
                });
                tooltips.Add(new TooltipLine(Mod, "2", ModContent.Find<ModItem>(Talent2Name).Tooltip.GetDefault().Split('\n')[1]));
            }

            if (ChosenTalent == 3 || ChosenTalent == -1)
            {
                tooltips.Add(new TooltipLine(Mod, "talent3",
                    ModContent.Find<ModItem>(Talent3Name).DisplayName.GetDefault().Replace("Talent: ", "") + ":")
                {
                    overrideColor = TalentColor
                });
                tooltips.Add(new TooltipLine(Mod, "3", ModContent.Find<ModItem>(Talent3Name).Tooltip.GetDefault().Split('\n')[1]));
            }

            if (ChosenTalent > 0 && Talent1 && Talent2 || Talent1 && Talent3 || Talent2 && Talent3)
            {
                tooltips.Add(new TooltipLine(Mod, "talents", "[Right click to swap talent]"));
            }
        }
    }
}