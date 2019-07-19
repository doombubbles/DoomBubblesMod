using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Color = Microsoft.Xna.Framework.Color;

namespace DoomBubblesMod.Items
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
        protected short ChosenTalent { get; private set; }
        
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();
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

        public override void NetRecieve(BinaryReader reader)
        {
            Talent1 = reader.ReadBoolean();
            Talent2 = reader.ReadBoolean();
            Talent3 = reader.ReadBoolean();
            ChosenTalent = reader.ReadInt16();
        }
        
        public override bool CanRightClick()
        {
            if (item.owner == Main.myPlayer)
            {
                if (!Main.mouseRightRelease)
                {
                    return false;
                }
                
                if (Main.mouseItem.type == mod.ItemType(Talent1Name) && !Talent1)
                {
                    Talent1 = true;
                    ChosenTalent = 1;
                    Main.mouseItem = new Item();
                }
                else if (Main.mouseItem.type == mod.ItemType(Talent2Name) && !Talent2)
                {
                    Talent2 = true;
                    ChosenTalent = 2;
                    Main.mouseItem = new Item();
                }
                else if (Main.mouseItem.type == mod.ItemType(Talent3Name) && !Talent3)
                {
                    Talent3 = true;
                    ChosenTalent = 3;
                    Main.mouseItem = new Item();
                } else if (Main.mouseItem.type == ItemID.GravityGlobe && ChosenTalent != -1 && Talent1 && Talent2 && Talent3)
                {
                    ChosenTalent = -1;
                } else if (Main.mouseItem.type == 0)
                {
                    int ogTalent = ChosenTalent;
                    if (ChosenTalent == 1)
                    {
                        if (Talent2)
                        {
                            ChosenTalent = 2;
                        } else if (Talent3)
                        {
                            ChosenTalent = 3;
                        }
                    } else if (ChosenTalent == 2)
                    {
                        if (Talent3)
                        {
                            ChosenTalent = 3;
                        } else if (Talent1)
                        {
                            ChosenTalent = 1;
                        }
                    } else if (ChosenTalent == 3)
                    {
                        if (Talent1)
                        {
                            ChosenTalent = 1;
                        } else if (Talent2)
                        {
                            ChosenTalent = 2;
                        }
                    }

                    if (ChosenTalent != ogTalent)
                    {
                        Main.PlaySound(SoundLoader.customSoundType, (int)Main.player[item.owner].position.X, (int)Main.player[item.owner].position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/TalentChange"));
                    }
                    
                } 
            }
            return false;
        }
        
        
        

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            if (ChosenTalent == 1 || ChosenTalent == -1)
            {
                tooltips.Add(new TooltipLine(mod, "talent1", mod.GetItem(Talent1Name).DisplayName.GetDefault().Replace("Talent: ", "") + ":")
                {
                    overrideColor = TalentColor
                });
                tooltips.Add(new TooltipLine(mod, "1", mod.GetItem(Talent1Name).Tooltip.GetDefault().Split('\n')[1]));
            }
            if (ChosenTalent == 2 || ChosenTalent == -1)
            {
                tooltips.Add(new TooltipLine(mod, "talent2", mod.GetItem(Talent2Name).DisplayName.GetDefault().Replace("Talent: ", "") + ":")
                {
                    overrideColor = TalentColor
                });
                tooltips.Add(new TooltipLine(mod, "2", mod.GetItem(Talent2Name).Tooltip.GetDefault().Split('\n')[1]));
            }
            if (ChosenTalent == 3 || ChosenTalent == -1)
            {
                tooltips.Add(new TooltipLine(mod, "talent3", mod.GetItem(Talent3Name).DisplayName.GetDefault().Replace("Talent: ", "") + ":")
                {
                    overrideColor = TalentColor
                });
                tooltips.Add(new TooltipLine(mod, "3", mod.GetItem(Talent3Name).Tooltip.GetDefault().Split('\n')[1]));
            }

            if (ChosenTalent > 0 && (Talent1 && Talent2) || (Talent1 && Talent3 || (Talent2 && Talent3)))
            {
                tooltips.Add(new TooltipLine(mod, "talents", "[Right click to swap talent]"));
            }
        }
    }
}