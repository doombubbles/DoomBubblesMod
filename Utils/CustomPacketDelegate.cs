using System;
using System.IO;
using System.Reflection;

namespace DoomBubblesMod.Utils;

/// <summary>
/// A class that holds a getter and setter for a specific Property of a custom packet
/// </summary>
public abstract class CustomPacketDelegate
{
    public static CustomPacketDelegate<T> Create<T>(PropertyInfo info, Type type) where T : CustomPacket<T>
    {
        var cpd = (CustomPacketDelegate<T>) Activator.CreateInstance(type.MakeGenericType(typeof(T)));
        cpd!.Init(info);
        return cpd;
    }
}

/// <summary>
/// <inheritdoc cref="CustomPacketDelegate"/>
/// </summary>
/// <typeparam name="T">The packet type that this is a delegate for</typeparam>
public abstract class CustomPacketDelegate<T> : CustomPacketDelegate where T : CustomPacket<T>
{
    public virtual void Read(BinaryReader reader, T customPacket)
    {
    }

    public virtual void Write(ModPacket packet, T customPacket)
    {
    }

    public abstract void Init(PropertyInfo property);
}

/// <summary>
/// <inheritdoc cref="CustomPacketDelegate"/>
/// </summary>
/// <typeparam name="TPacket">The packet type that this is a delegate for</typeparam>
/// <typeparam name="TType">The type that this is a delegate for</typeparam>
public abstract class CustomPacketDelegate<TPacket, TType> : CustomPacketDelegate<TPacket>
    where TPacket : CustomPacket<TPacket>
{
    private Action<TPacket, TType> setter;
    private Func<TPacket, TType> getter;

    public sealed override void Init(PropertyInfo property)
    {
        setter = (Action<TPacket, TType>)
            Delegate.CreateDelegate(typeof(Action<TPacket, TType>), null, property.GetSetMethod()!);
        getter = (Func<TPacket, TType>)
            Delegate.CreateDelegate(typeof(Func<TPacket, TType>), null, property.GetGetMethod()!);
    }

    /// <summary>
    /// Reads information from the binary reader and stores it in the packet
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="customPacket"></param>
    public sealed override void Read(BinaryReader reader, TPacket customPacket) => setter(customPacket, Read(reader));
    
    /// <summary>
    /// Gets the information from the packet and writes it into the ModPacket
    /// </summary>
    /// <param name="packet"></param>
    /// <param name="customPacket"></param>
    public sealed override void Write(ModPacket packet, TPacket customPacket) => Write(packet, getter(customPacket));

    protected abstract TType Read(BinaryReader reader);
    protected abstract void Write(ModPacket packet, TType value);
}