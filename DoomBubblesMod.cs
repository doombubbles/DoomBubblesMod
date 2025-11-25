global using Terraria;
global using Terraria.ModLoader;
global using Terraria.ID;
global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using static Terraria.ModLoader.ModContent;
global using Terraria.DataStructures;
global using Terraria.Localization;
global using EasyNetworkingLib;
global using static DoomBubblesMod.DoomBubblesMod;
using System.Collections.Generic;
using System.IO;
using DoomBubblesMod.Utils;
using Microsoft.Xna.Framework.Input;


namespace DoomBubblesMod;

public class DoomBubblesMod : Mod
{
    public static ModKeybind PowerStoneHotKey { get; private set; }
    public static ModKeybind SpaceStoneHotKey { get; private set; }
    public static ModKeybind RealityStoneHotKey { get; private set; }
    public static ModKeybind SoulStoneHotKey { get; private set; }
    public static ModKeybind TimeStoneHotKey { get; private set; }
    public static ModKeybind MindStoneHotKey { get; private set; }

    public static Mod ThoriumMod => ModLoader.TryGetMod("ThoriumMod", out var mod) ? mod : null;
    public static Mod CalamityMod => ModLoader.TryGetMod("CalamityMod", out var mod) ? mod : null;
    public static Mod SetBonusAccessories => ModLoader.TryGetMod("SetBonusAccessories", out var mod) ? mod : null;

    public static List<Color> RainbowColors =>
    [
        Color.Red, Color.Orange, Color.Yellow, Color.LimeGreen, Color.Blue, Color.Indigo, Color.Violet
    ];

    public DoomBubblesMod()
    {
        PreJITFilter = new DeclaringPreJITFilter();
    }

    public override void Load()
    {
        PowerStoneHotKey = KeybindLoader.RegisterKeybind(this, "Power Stone", Keys.F2);
        SpaceStoneHotKey = KeybindLoader.RegisterKeybind(this, "Space Stone", Keys.F3);
        RealityStoneHotKey = KeybindLoader.RegisterKeybind(this, "Reality Stone", Keys.F4);
        SoulStoneHotKey = KeybindLoader.RegisterKeybind(this, "Soul Stone", Keys.F5);
        TimeStoneHotKey = KeybindLoader.RegisterKeybind(this, "Time Stone", Keys.F6);
        MindStoneHotKey = KeybindLoader.RegisterKeybind(this, "Mind Stone", Keys.F7);

        if (!Main.dedServ)
        {
            //TextureAssets.Projectile[ProjectileID.MoonlordBullet]. TODO texture changing
            //Main.dustTexture = GetTexture("Dusts/Dust"); TODO dust changing
        }
    }


    public override void Unload()
    {
        PowerStoneHotKey = null;
        SpaceStoneHotKey = null;
        RealityStoneHotKey = null;
        SoulStoneHotKey = null;
        TimeStoneHotKey = null;
        MindStoneHotKey = null;
    }

    public override void HandlePacket(BinaryReader reader, int whoAmI)
    {
        ModCustomPacket.Handle(this, reader, whoAmI);
    }
}