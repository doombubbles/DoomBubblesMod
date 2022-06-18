using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DoomBubblesMod.Common.Configs;

public class ClientConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;
    
    [DefaultValue(false)]
    [Label("Permanent rainbow cursor")]
    public bool PermanentRainbowCursor { get; set; }
}