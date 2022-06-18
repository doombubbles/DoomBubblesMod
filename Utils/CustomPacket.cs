using System;
using System.Collections.Generic;
using System.IO;

namespace DoomBubblesMod.Utils;

/// <summary>
/// A definable ModType for a specific set of information that's send-able in multiplayer via packets
/// </summary>
public abstract class CustomPacket : ModType
{
    internal static Dictionary<Type, CustomPacketInfo> packetInfo = new();

    public int Type { get; internal set; }

    public override void Load()
    {
        packetInfo ??= new Dictionary<Type, CustomPacketInfo>();
        base.Load();
    }

    public override void Unload()
    {
        base.Unload();
        packetInfo = null;
    }

    /// <summary>
    /// Applies the results of this packet
    /// </summary>
    public abstract void HandlePacket();

    /// <summary>
    /// Initialize the default values of your custom packet properties
    /// </summary>
    protected virtual void SetDefaults()
    {
    }

    public sealed override void SetupContent()
    {
    }

    public sealed override void SetStaticDefaults()
    {
    }

    public abstract void Receive(BinaryReader reader, int sender);

    public abstract void Send(int toClient = -1, int ignoreClient = -1);

    /// <summary>
    ///     Causes all clients and the server to handle the results of this packet
    ///     <br />
    ///     Works for single-player or multiplayer
    /// </summary>
    public void HandleForAll()
    {
        switch (Main.netMode)
        {
            case NetmodeID.MultiplayerClient:
                Send(-1, Main.myPlayer);
                break;
            case NetmodeID.Server:
                Send();
                break;
        }
        
        HandlePacket();
    }
}

/// <inheritdoc cref="CustomPacket"/>
public abstract class CustomPacket<T> : CustomPacket where T : CustomPacket<T>
{
    protected sealed override void Register()
    {
        var packetType = GetType();
        var info = new CustomPacketInfo<T>(Mod, packetType);
        Type = info.type = packetInfo.Count;

        packetInfo[packetType] = info;

        ModTypeLookup<CustomPacket>.Register(this);

        Mod.Logger.Info($"Registered ModPacket {GetType().Name} with {info.NumProperties} properties as type {info.type}");
    }

    public sealed override void Receive(BinaryReader reader, int sender)
    {
        if (packetInfo[GetType()] is CustomPacketInfo<T> info)
        {
            // Mod.Logger.Info($"Receiving packet {GetType().Name} on netMode {Main.netMode}");

            SetDefaults();
            info.ReadAll(reader, (T) this);
            if (Main.netMode == NetmodeID.Server)
            {
                Send(-1, sender);
            }
            HandlePacket();
            
            // Mod.Logger.Info($"Handled packet {GetType().Name} on netMode {Main.netMode}");
        }
        else
        {
            Mod.Logger.Error($"Failed receiving packet {GetType().Name} because packet info is wrong type");
        }
    }

    public sealed override void Send(int toClient = -1, int ignoreClient = -1)
    {
        if (packetInfo[GetType()] is CustomPacketInfo<T> info)
        {
            // info.Mod.Logger.Info($"Sending packet {GetType().Name} on netMode {Main.netMode} with type {info.type}");

            var packet = GetInstance<DoomBubblesMod>().GetPacket();
            packet.Write(info.type);
            info.WriteAll(packet, (T) this);
            packet.Send(toClient, ignoreClient);
        }
        else
        {
            GetInstance<DoomBubblesMod>().Logger
                .Error($"Failed sending packet {GetType().Name} because packet info is wrong type");
        }
    }
}

/// <summary>
/// A class that holds the static information about a custom packet, including the generated delegates for
/// getting/setting values without doing repeated Reflection
/// </summary>
public class CustomPacketInfo
{
    public Mod Mod { get; protected init; }
    public int type;

    protected static List<Type> DelegateTypes { get; set; }
}