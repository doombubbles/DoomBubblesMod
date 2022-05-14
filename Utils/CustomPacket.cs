using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DoomBubblesMod.Utils;

public abstract class CustomPacket : ModType
{
    internal static Dictionary<Type, ModPacketInfo> packetInfo = new();

    public int Type { get; internal set; }

    public override void Load()
    {
        packetInfo ??= new Dictionary<Type, ModPacketInfo>();
        base.Load();
    }

    public override void Unload()
    {
        base.Unload();
        packetInfo = null;
    }

    public abstract void HandlePacket(int playerId);

    public sealed override void SetupContent()
    {
        SetStaticDefaults();
    }

    public abstract void Receive(BinaryReader reader, int whoAmI);

    public abstract void Send(int toClient = -1, int ignoreClient = -1);

    /// <summary>
    ///     Causes all clients and the server to handle the results of this packet
    ///     <br />
    ///     Works for single-player or multiplayer
    /// </summary>
    public void HandleForAll()
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            Send(-1, Main.myPlayer);
        }

        HandlePacket(Main.myPlayer);
    }
}

public abstract class CustomPacket<T> : CustomPacket where T : CustomPacket<T>
{
    protected sealed override void Register()
    {
        var packetType = GetType();
        var packetInfo = new ModPacketInfo<T>
        {
            mod = Mod,
            type = CustomPacket.packetInfo.Count
        };
        Type = packetInfo.type;
        var properties = packetType.GetProperties(BindingFlags.Public |
                                                  BindingFlags.NonPublic |
                                                  BindingFlags.DeclaredOnly |
                                                  BindingFlags.Instance);

        packetInfo.byteSetters = GetSetters<byte>(properties);
        packetInfo.byteGetters = GetGetters<byte>(properties);

        packetInfo.boolSetters = GetSetters<bool>(properties);
        packetInfo.boolGetters = GetGetters<bool>(properties);

        packetInfo.intSetters = GetSetters<int>(properties);
        packetInfo.intGetters = GetGetters<int>(properties);

        CustomPacket.packetInfo[packetType] = packetInfo;

        ModTypeLookup<CustomPacket>.Register(this);

        Mod.Logger.Info($"Registered ModPacket {GetType().Name} with {properties.Length} properties");
    }

    public sealed override void Receive(BinaryReader reader, int whoAmI)
    {
        if (CustomPacket.packetInfo[GetType()] is ModPacketInfo<T> packetInfo)
        {
            Mod.Logger.Info($"Receiving packet {GetType().Name} on netMode {Main.netMode}");

            foreach (var byteSetter in packetInfo.byteSetters)
            {
                byteSetter((T) this, reader.ReadByte());
            }

            foreach (var boolSetter in packetInfo.boolSetters)
            {
                boolSetter((T) this, reader.ReadBoolean());
            }

            foreach (var intSetter in packetInfo.intSetters)
            {
                intSetter((T) this, reader.ReadInt32());
            }

            HandlePacket(whoAmI);
            Mod.Logger.Info($"Handled packet {GetType().Name} on netMode {Main.netMode}");
        }
        else
        {
            Mod.Logger.Error($"Failed receiving packet {GetType().Name} because packet info is wrong type");
        }
    }

    public sealed override void Send(int toClient = -1, int ignoreClient = -1)
    {
        if (CustomPacket.packetInfo[GetType()] is ModPacketInfo<T> packetInfo)
        {
            packetInfo.mod.Logger.Info($"Sending packet {GetType().Name} on netMode {Main.netMode}");

            var packet = GetInstance<DoomBubblesMod>().GetPacket();

            packet.Write(packetInfo.type);


            foreach (var byteGetter in packetInfo.byteGetters)
            {
                packet.Write(byteGetter((T) this));
            }

            foreach (var boolGetter in packetInfo.boolGetters)
            {
                packet.Write(boolGetter((T) this));
            }

            foreach (var intGetter in packetInfo.intGetters)
            {
                packet.Write(intGetter((T) this));
            }

            packet.Send(toClient, ignoreClient);
        }
        else
        {
            GetInstance<DoomBubblesMod>().Logger
                .Error($"Failed sending packet {GetType().Name} because packet info is wrong type");
        }
    }


    private static List<Action<T, TValue>> GetSetters<TValue>(IEnumerable<PropertyInfo> properties)
    {
        return properties
            .Where(info => info.PropertyType == typeof(TValue))
            .Select(info => Delegate.CreateDelegate(typeof(Action<T, TValue>), null, info.GetSetMethod()!))
            .Cast<Action<T, TValue>>()
            .ToList();
    }

    private static List<Func<T, TValue>> GetGetters<TValue>(IEnumerable<PropertyInfo> properties)
    {
        return properties
            .Where(info => info.PropertyType == typeof(TValue))
            .Select(info => Delegate.CreateDelegate(typeof(Func<T, TValue>), null, info.GetGetMethod()!))
            .Cast<Func<T, TValue>>()
            .ToList();
    }
}

public class ModPacketInfo
{
    public Mod mod;
    public int type;
}

public class ModPacketInfo<T> : ModPacketInfo where T : CustomPacket<T>
{
    public List<Func<T, bool>> boolGetters = new();

    public List<Action<T, bool>> boolSetters = new();
    public List<Func<T, byte>> byteGetters = new();
    public List<Action<T, byte>> byteSetters = new();
    public List<Func<T, int>> intGetters = new();

    public List<Action<T, int>> intSetters = new();
}