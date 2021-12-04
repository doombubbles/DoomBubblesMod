using System.Collections.Generic;
using System.IO;
using Terraria.Audio;
using Terraria.ModLoader.IO;
using Terraria.ID;

namespace DoomBubblesMod.Content.Items.Talent;

public abstract class ModItemWithTalents<T1, T2, T3> : ModItemWithTalents
    where T1 : ModItem where T2 : ModItem where T3 : ModItem
{
    public override ModItem Talent1Item => ModContent.GetInstance<T1>();
    public override ModItem Talent2Item => ModContent.GetInstance<T2>();
    public override ModItem Talent3Item => ModContent.GetInstance<T3>();
}

public abstract class ModItemWithTalents : ModItem
{
    public abstract ModItem Talent1Item { get; }
    public abstract ModItem Talent2Item { get; }
    public abstract ModItem Talent3Item { get; }
    protected abstract Color? TalentColor { get; }

    private bool Talent1Unlocked { get; set; }
    private bool Talent2Unlocked { get; set; }
    private bool Talent3Unlocked { get; set; }
    public short ChosenTalent { get; private set; }

    public override void SaveData(TagCompound tag)
    {
        tag["talent1"] = Talent1Unlocked;
        tag["talent2"] = Talent2Unlocked;
        tag["talent3"] = Talent3Unlocked;
        tag["chosenTalent"] = ChosenTalent;
    }

    public override void LoadData(TagCompound tag)
    {
        Talent1Unlocked = tag.GetBool("talent1");
        Talent2Unlocked = tag.GetBool("talent2");
        Talent3Unlocked = tag.GetBool("talent3");
        ChosenTalent = tag.GetShort("chosenTalent");
    }

    public override void NetSend(BinaryWriter writer)
    {
        writer.Write(Talent1Unlocked);
        writer.Write(Talent2Unlocked);
        writer.Write(Talent3Unlocked);
        writer.Write(ChosenTalent);
    }

    public override void NetReceive(BinaryReader reader)
    {
        Talent1Unlocked = reader.ReadBoolean();
        Talent2Unlocked = reader.ReadBoolean();
        Talent3Unlocked = reader.ReadBoolean();
        ChosenTalent = reader.ReadInt16();
    }

    public override bool CanRightClick()
    {
        if (Item.playerIndexTheItemIsReservedFor == Main.myPlayer)
        {
            if (Main.mouseItem.type == Talent1Item.Type ||
                Main.mouseItem.type == Talent2Item.Type ||
                Main.mouseItem.type == Talent3Item.Type)
            {
                return true;
            }

            if ((Talent1Unlocked ? 1 : 0) + (Talent2Unlocked ? 1 : 0) + (Talent3Unlocked ? 1 : 0) > 1)
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
            if (Main.mouseItem.type == Talent1Item.Type && !Talent1Unlocked)
            {
                Talent1Unlocked = true;
                ChosenTalent = 1;
                Main.mouseItem = new Item();
            }
            else if (Main.mouseItem.type == Talent2Item.Type && !Talent2Unlocked)
            {
                Talent2Unlocked = true;
                ChosenTalent = 2;
                Main.mouseItem = new Item();
            }
            else if (Main.mouseItem.type == Talent3Item.Type && !Talent3Unlocked)
            {
                Talent3Unlocked = true;
                ChosenTalent = 3;
                Main.mouseItem = new Item();
            }
            else if (Main.mouseItem.type == ItemID.GravityGlobe &&
                     ChosenTalent != -1 &&
                     Talent1Unlocked &&
                     Talent2Unlocked &&
                     Talent3Unlocked)
            {
                ChosenTalent = -1;
            }
            else if (Main.mouseItem.type == ItemID.None)
            {
                int ogTalent = ChosenTalent;
                if (ChosenTalent == 1)
                {
                    if (Talent2Unlocked)
                    {
                        ChosenTalent = 2;
                    }
                    else if (Talent3Unlocked)
                    {
                        ChosenTalent = 3;
                    }
                }
                else if (ChosenTalent == 2)
                {
                    if (Talent3Unlocked)
                    {
                        ChosenTalent = 3;
                    }
                    else if (Talent1Unlocked)
                    {
                        ChosenTalent = 1;
                    }
                }
                else if (ChosenTalent == 3)
                {
                    if (Talent1Unlocked)
                    {
                        ChosenTalent = 1;
                    }
                    else if (Talent2Unlocked)
                    {
                        ChosenTalent = 2;
                    }
                }

                if (ChosenTalent != ogTalent)
                {
                    SoundEngine.PlaySound(SoundLoader.GetLegacySoundSlot(Mod, "Sounds/TalentChange"),
                        Main.player[Item.playerIndexTheItemIsReservedFor].position);
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
                Talent1Item.DisplayName.GetDefault().Replace("Talent: ", "") + ":")
            {
                overrideColor = TalentColor
            });
            tooltips.Add(
                new TooltipLine(Mod, "1", Talent1Item.Tooltip.GetDefault().Split('\n')[1]));
        }

        if (ChosenTalent == 2 || ChosenTalent == -1)
        {
            tooltips.Add(new TooltipLine(Mod, "talent2",
                Talent2Item.DisplayName.GetDefault().Replace("Talent: ", "") + ":")
            {
                overrideColor = TalentColor
            });
            tooltips.Add(
                new TooltipLine(Mod, "2", Talent2Item.Tooltip.GetDefault().Split('\n')[1]));
        }

        if (ChosenTalent == 3 || ChosenTalent == -1)
        {
            tooltips.Add(new TooltipLine(Mod, "talent3",
                Talent3Item.DisplayName.GetDefault().Replace("Talent: ", "") + ":")
            {
                overrideColor = TalentColor
            });
            tooltips.Add(
                new TooltipLine(Mod, "3", Talent3Item.Tooltip.GetDefault().Split('\n')[1]));
        }

        if (ChosenTalent > 0 && Talent1Unlocked && Talent2Unlocked ||
            Talent1Unlocked && Talent3Unlocked ||
            Talent2Unlocked && Talent3Unlocked)
        {
            tooltips.Add(new TooltipLine(Mod, "talents", "[Right click to swap talent]"));
        }
    }
}