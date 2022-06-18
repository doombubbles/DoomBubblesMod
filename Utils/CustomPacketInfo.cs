using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader.Core;

namespace DoomBubblesMod.Utils;

/// <inheritdoc cref="CustomPacketInfo"/>
public class CustomPacketInfo<T> : CustomPacketInfo where T : CustomPacket<T>
{
    private readonly List<CustomPacketDelegate<T>> delegates;

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
            .Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(CustomPacketDelegate)))
            .ToList();

        delegates = properties
            .Join(DelegateTypes, info => info.PropertyType, t => t.BaseType!.GenericTypeArguments[1],
                CustomPacketDelegate.Create<T>)
            .OrderBy(d => d.GetType().Name)
            .ToList();
    }

    public void ReadAll(BinaryReader reader, T customPacket) => delegates.ForEach(d => d.Read(reader, customPacket));

    public void WriteAll(ModPacket packet, T customPacket) => delegates.ForEach(d => d.Write(packet, customPacket));
}