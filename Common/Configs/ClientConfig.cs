using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DoomBubblesMod.Common.Configs;

public class ClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [DefaultValue(true)]
    public bool EasyItemSwapping { get; set; }

    [DefaultValue(false)]
    public bool PermanentRainbowCursor { get; set; }

    [DefaultValue(true)]
    public bool NoSpelunkerTint { get; set; }

    [DefaultValue(true)]
    public bool SmoothDPSReading { get; set; }

    [DefaultValue(3)]
    [Range(1, 15)]
    [Slider]
    [DrawTicks]
    [Increment(1)]
    public int DpsSeconds { get; set; }

    [DefaultValue(true)]
    public bool FixAlchemistNPCGrammar { get; set; }

    [DefaultValue(true)]
    public bool CalamityThrowingDamage { get; set; }
}