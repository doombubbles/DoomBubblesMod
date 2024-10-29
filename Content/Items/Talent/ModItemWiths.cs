using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DoomBubblesMod.Utils;
using Terraria.Audio;
using Terraria.ModLoader.IO;

namespace DoomBubblesMod.Content.Items.Talent;

public abstract class ModItemWithTalents<T1, T2, T3> : ModItemWithTalents
    where T1 : TalentItem where T2 : TalentItem where T3 : TalentItem
{
    public override TalentItem Talent1Item => GetInstance<T1>();
    public override TalentItem Talent2Item => GetInstance<T2>();
    public override TalentItem Talent3Item => GetInstance<T3>();
}

public abstract class ModItemWithTalents : ModItem
{
    
    public abstract TalentItem Talent1Item { get; }
    public abstract TalentItem Talent2Item { get; }
    public abstract TalentItem Talent3Item { get; }

    public TalentItem TalentItem(int i) => i switch
    {
        0 => Talent1Item,
        1 => Talent2Item,
        2 => Talent3Item,
        _ => throw new ArgumentOutOfRangeException(nameof(i), i, null)
    };
    
    public abstract int SoldBy { get; }
    
    protected abstract Color? TalentColor { get; }

    private bool Talent1Unlocked { get; set; }
    private bool Talent2Unlocked { get; set; }
    private bool Talent3Unlocked { get; set; }

    private IEnumerable<bool> TalentsUnlocked
    {
        get
        {
            yield return Talent1Unlocked;
            yield return Talent1Unlocked;
            yield return Talent3Unlocked;
        }
    }

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

        return false;
    }

    public override void RightClick(Player player)
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
                SoundEngine.PlaySound(Mod.Sound("TalentChange"),
                    Main.player[Item.playerIndexTheItemIsReservedFor].position);
            }
        }
    }

    public override bool ConsumeItem(Player player)
    {
        return false;
    }


    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        if (ChosenTalent is 1 or -1)
        {
            tooltips.Add(new TooltipLine(Mod, "talent1", Talent1Item.Name.ToNameFormat() + ":")
            {
                OverrideColor = TalentColor
            });
            tooltips.Add(new TooltipLine(Mod, "1", Talent1Item.Description));
        }

        if (ChosenTalent is 2 or -1)
        {
            tooltips.Add(new TooltipLine(Mod, "talent2", Talent2Item.Name.ToNameFormat() + ":")
            {
                OverrideColor = TalentColor
            });
            tooltips.Add(new TooltipLine(Mod, "2", Talent2Item.Description));
        }

        if (ChosenTalent is 3 or -1)
        {
            tooltips.Add(new TooltipLine(Mod, "talent3", Talent3Item.Name.ToNameFormat() + ":")
            {
                OverrideColor = TalentColor
            });
            tooltips.Add(new TooltipLine(Mod, "3", Talent3Item.Description));
        }

        if (ChosenTalent > 0 && TalentsUnlocked.Count(b => b) == 2)
        {
            tooltips.Add(new TooltipLine(Mod, "talents", "[Right click to swap talent]"));
        }
    }
}