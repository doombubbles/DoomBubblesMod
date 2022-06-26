using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace DoomBubblesMod.Utils;

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


/// <inheritdoc cref="CustomPacketInfo"/>
public class CustomPacketInfo<T> : CustomPacketInfo where T : CustomPacket<T>
{
    private readonly List<CustomPacketSerializer<T>> delegates;

    public int NumProperties => delegates.Count;

    public CustomPacketInfo(Mod mod, IReflect packetType)
    {
        Mod = mod;
        var properties = packetType.GetProperties(BindingFlags.Public |
                                                  BindingFlags.NonPublic |
                                                  BindingFlags.DeclaredOnly |
                                                  BindingFlags.Instance);

        DelegateTypes ??= AssemblyManager
            .GetLoadableTypes(GetInstance<DoomBubblesMod>().Code)
            .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(CustomPacketSerializer)))
            .ToList();

        delegates = properties
            .Join(DelegateTypes, info => info.PropertyType, t => t.BaseType!.GenericTypeArguments[1],
                CustomPacketSerializer.Create<T>)
            .OrderBy(d => d.GetType().Name)
            .ToList();
    }

    /// <summary>
    /// Reads all information from the binary reader and stores it in the custom packet
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="customPacket"></param>
    public void ReadAll(BinaryReader reader, T customPacket) => delegates.ForEach(d => d.Read(reader, customPacket));

    /// <summary>
    /// Writes all custom information in the custom packet into the ModPacket
    /// </summary>
    /// <param name="packet"></param>
    /// <param name="customPacket"></param>
    public void WriteAll(ModPacket packet, T customPacket) => delegates.ForEach(d => d.Write(packet, customPacket));
}