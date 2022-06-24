using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DoomBubblesMod.Common.Configs;

public class ClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;
    
    [DefaultValue(false)]
    [Label("Permanent rainbow cursor")]
    public bool PermanentRainbowCursor { get; set; }

    [DefaultValue(true)]
    [Label("Smooth DPS Reading")]
    [Tooltip("Smooths out the reading ot the DPS meter")]
    public bool SmoothDPSReading { get; set; }
    
    [DefaultValue(true)]
    [Label("Fix Alchemist NPC Grammar")]
    [Tooltip("Improves the grammar on certain tooltips in the AlchemistNPCLite mod")]
    public bool FixAlchemistNPCGrammar { get; set; }
    
    
    [DefaultValue(3)]
    [Range(1, 15)]
    [Slider]
    [DrawTicks]
    [Increment(1)]
    [Label("DPS Seconds")]
    [Tooltip("How many seconds the DPS meter tracks for. Lower for more immediately accurate, higher for more consistent.")]
    public int DpsSeconds { get; set; }
}