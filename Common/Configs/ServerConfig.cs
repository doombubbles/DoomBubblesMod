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
    
    [DefaultValue(true)]
    [Label("Ropes through Valid Houses")]
    [Tooltip("Allows a house to still be valid even if theres up to 2 rope blocks in the walls")]
    public bool RopesThroughWalls { get; set; }

    public override bool AcceptClientChanges(ModConfig pendingConfig, int whoAmI, ref string message)
    {
        if (whoAmI == 0)
        {
            message = "Changes accepted!";
            return true;
        }

        message = "You had no right to do that.";
        return false;
    }

    public override bool NeedsReload(ModConfig pendingConfig)
    {
        if (pendingConfig is ServerConfig serverConfig && serverConfig.RopesThroughWalls != RopesThroughWalls)
        {
            return true;
        }

        return false;
    }
}