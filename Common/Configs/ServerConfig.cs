using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace DoomBubblesMod.Common.Configs;

public class ServerConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;

    [DefaultValue(true)]
    [Label("Disable Damage Variance")]
    [Tooltip("Disables the default +/- 15% usually applied to all damage")]
    public bool DisableDamageVariance { get; set; }

    public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
    {
        if (whoAmI == 0) {
            message = "Changes accepted!";
            return true;
        }
        message = "You had no right to do that.";
        return false;
    }
    
}