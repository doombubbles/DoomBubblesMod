using System;
using System.IO;
using System.Reflection;

namespace DoomBubblesMod.Utils;

/// <summary>
/// A class that holds a getter and setter for a specific Property of a custom packet
/// </summary>
public abstract class CustomPacketSerializer
{
    public static CustomPacketSerializer<T> Create<T>(PropertyInfo info, Type type) where T : CustomPacket<T>
    {
        var cpd = (CustomPacketSerializer<T>) Activator.CreateInstance(type.MakeGenericType(typeof(T)));
        cpd!.Init(info);
        return cpd;
    }

    protected abstract void Init(PropertyInfo property);
}

/// <summary>
/// <inheritdoc cref="CustomPacketSerializer"/>
/// </summary>
/// <typeparam name="T">The packet type that this is a delegate for</typeparam>
public abstract class CustomPacketSerializer<T> : CustomPacketSerializer where T : CustomPacket<T>
{
    /// <summary>
    /// Reads information from the binary reader and stores it in the packet
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="customPacket"></param>
    public virtual void Read(BinaryReader reader, T customPacket)
    {
    }

    /// <summary>
    /// Gets the information from the packet and writes it into the ModPacket
    /// </summary>
    /// <param name="packet"></param>
    /// <param name="customPacket"></param>
    public virtual void Write(ModPacket packet, T customPacket)
    {
    }
}

/// <summary>
/// <inheritdoc cref="CustomPacketSerializer"/>
/// </summary>
/// <typeparam name="TPacket">The packet type that this is a delegate for</typeparam>
/// <typeparam name="TType">The type that this is a delegate for</typeparam>
public abstract class CustomPacketSerializer<TPacket, TType> : CustomPacketSerializer<TPacket>
    where TPacket : CustomPacket<TPacket>
{
    private Action<TPacket, TType> setter;
    private Func<TPacket, TType> getter;

    protected sealed override void Init(PropertyInfo property)
    {
        setter = (Action<TPacket, TType>)
            Delegate.CreateDelegate(typeof(Action<TPacket, TType>), null, property.GetSetMethod(true)!);
        getter = (Func<TPacket, TType>)
            Delegate.CreateDelegate(typeof(Func<TPacket, TType>), null, property.GetGetMethod(true)!);
    }

    public sealed override void Read(BinaryReader reader, TPacket customPacket) => setter(customPacket, Read(reader));
    
    public sealed override void Write(ModPacket packet, TPacket customPacket) => Write(packet, getter(customPacket));

    protected abstract TType Read(BinaryReader reader);
    protected abstract void Write(ModPacket packet, TType value);
}