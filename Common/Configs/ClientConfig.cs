using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DoomBubblesMod.Common.Configs;

public class ClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [DefaultValue(true)]
    [Label("Easy Item Swapping")]
    [Tooltip(
        "Lets you more easily switch between which item you're using by making holding down the associated\n " +
        "hotbar key actually switch to the item after the current animation finishes, not just queue it up.")]
    public bool EasyItemSwapping { get; set; }

    [DefaultValue(false)] [Label("Permanent rainbow cursor")]
    public bool PermanentRainbowCursor { get; set; }

    [DefaultValue(true)] [Label("No Spelunker Tint")] [Tooltip("Makes the spelunker potion not change the colors of ores.\nNOTE: Fancy Lighting mod overwrites this.")]
    public bool NoSpelunkerTint { get; set; }
    
    [DefaultValue(true)] [Label("Smooth DPS Reading")] [Tooltip("Smooths out the reading ot the DPS meter")]
    public bool SmoothDPSReading { get; set; }

    [DefaultValue(3)]
    [Range(1, 15)]
    [Slider]
    [DrawTicks]
    [Increment(1)]
    [Label("DPS Seconds")]
    [Tooltip(
        "How many seconds the DPS meter tracks for. Lower for more immediately accurate, higher for more consistent.")]
    public int DpsSeconds { get; set; }


    [DefaultValue(true)]
    [Label("Fix Alchemist NPC Grammar")]
    [Tooltip("Improves the grammar on certain tooltips in the AlchemistNPCLite mod")]
    public bool FixAlchemistNPCGrammar { get; set; }


    [DefaultValue(true)]
    [Label("Calamity Throwing Damage")]
    [Tooltip(
        "If used with Calamity Mod, will replace all tooltip instances of \"rogue damage\" with \"throwing damage\". They are already compatible with each other.")]
    public bool CalamityThrowingDamage { get; set; }
}