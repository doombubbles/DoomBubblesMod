using System.IO;

namespace DoomBubblesMod.Utils;

#region Simple Types

internal class CustomPacketByte<T> : CustomPacketDelegate<T, byte> where T : CustomPacket<T>
{
    protected override byte Read(BinaryReader reader) => reader.ReadByte();
    protected override void Write(ModPacket packet, byte value) => packet.Write(value);
}

internal class CustomPacketBool<T> : CustomPacketDelegate<T, bool> where T : CustomPacket<T>
{
    protected override bool Read(BinaryReader reader) => reader.ReadBoolean();
    protected override void Write(ModPacket packet, bool value) => packet.Write(value);
}

internal class CustomPacketShort<T> : CustomPacketDelegate<T, short> where T : CustomPacket<T>
{
    protected override short Read(BinaryReader reader) => reader.ReadInt16();
    protected override void Write(ModPacket packet, short value) => packet.Write(value);
}

internal class CustomPacketInt<T> : CustomPacketDelegate<T, int> where T : CustomPacket<T>
{
    protected override int Read(BinaryReader reader) => reader.ReadInt32();
    protected override void Write(ModPacket packet, int value) => packet.Write(value);
}

internal class CustomPacketLong<T> : CustomPacketDelegate<T, long> where T : CustomPacket<T>
{
    protected override long Read(BinaryReader reader) => reader.ReadInt64();
    protected override void Write(ModPacket packet, long value) => packet.Write(value);
}

internal class CustomPacketString<T> : CustomPacketDelegate<T, string> where T : CustomPacket<T>
{
    protected override string Read(BinaryReader reader) => reader.ReadString();
    protected override void Write(ModPacket packet, string value) => packet.Write(value);
}

internal class CustomPacketChar<T> : CustomPacketDelegate<T, char> where T : CustomPacket<T>
{
    protected override char Read(BinaryReader reader) => reader.ReadChar();
    protected override void Write(ModPacket packet, char value) => packet.Write(value);
}

#endregion


#region Complex Types

internal class CustomPacketPlayer<T> : CustomPacketDelegate<T, Player> where T : CustomPacket<T>
{
    protected override Player Read(BinaryReader reader) => Main.player.GetOrDefault(reader.ReadInt32());

    protected override void Write(ModPacket packet, Player value) => packet.Write(value?.whoAmI ?? -1);
}

internal class CustomPacketNPC<T> : CustomPacketDelegate<T, NPC> where T : CustomPacket<T>
{
    protected override NPC Read(BinaryReader reader) => Main.npc.GetOrDefault(reader.ReadInt32());
    protected override void Write(ModPacket packet, NPC value) => packet.Write(value?.whoAmI ?? -1);
}

internal class CustomPacketProjectile<T> : CustomPacketDelegate<T, Projectile> where T : CustomPacket<T>
{
    protected override Projectile Read(BinaryReader reader) => Main.projectile.GetOrDefault(reader.ReadInt32());
    protected override void Write(ModPacket packet, Projectile value) => packet.Write(value?.whoAmI ?? -1);
}

#endregion