using System.IO;

namespace DoomBubblesMod.Utils;

#region Simple Types

public class CustomPacketByte<T> : CustomPacketSerializer<T, byte> where T : CustomPacket<T>
{
    protected override byte Read(BinaryReader reader) => reader.ReadByte();
    protected override void Write(ModPacket packet, byte value) => packet.Write(value);
}

public class CustomPacketBool<T> : CustomPacketSerializer<T, bool> where T : CustomPacket<T>
{
    protected override bool Read(BinaryReader reader) => reader.ReadBoolean();
    protected override void Write(ModPacket packet, bool value) => packet.Write(value);
}

public class CustomPacketShort<T> : CustomPacketSerializer<T, short> where T : CustomPacket<T>
{
    protected override short Read(BinaryReader reader) => reader.ReadInt16();
    protected override void Write(ModPacket packet, short value) => packet.Write(value);
}

public class CustomPacketInt<T> : CustomPacketSerializer<T, int> where T : CustomPacket<T>
{
    protected override int Read(BinaryReader reader) => reader.ReadInt32();
    protected override void Write(ModPacket packet, int value) => packet.Write(value);
}

public class CustomPacketLong<T> : CustomPacketSerializer<T, long> where T : CustomPacket<T>
{
    protected override long Read(BinaryReader reader) => reader.ReadInt64();
    protected override void Write(ModPacket packet, long value) => packet.Write(value);
}

public class CustomPacketString<T> : CustomPacketSerializer<T, string> where T : CustomPacket<T>
{
    protected override string Read(BinaryReader reader) => reader.ReadString();
    protected override void Write(ModPacket packet, string value) => packet.Write(value);
}

public class CustomPacketChar<T> : CustomPacketSerializer<T, char> where T : CustomPacket<T>
{
    protected override char Read(BinaryReader reader) => reader.ReadChar();
    protected override void Write(ModPacket packet, char value) => packet.Write(value);
}

#endregion


#region Complex Types

public class CustomPacketPlayer<T> : CustomPacketSerializer<T, Player> where T : CustomPacket<T>
{
    protected override Player Read(BinaryReader reader) => Main.player.GetOrDefault(reader.ReadInt32());

    protected override void Write(ModPacket packet, Player value) => packet.Write(value?.whoAmI ?? -1);
}

public class CustomPacketNPC<T> : CustomPacketSerializer<T, NPC> where T : CustomPacket<T>
{
    protected override NPC Read(BinaryReader reader) => Main.npc.GetOrDefault(reader.ReadInt32());
    protected override void Write(ModPacket packet, NPC value) => packet.Write(value?.whoAmI ?? -1);
}

public class CustomPacketProjectile<T> : CustomPacketSerializer<T, Projectile> where T : CustomPacket<T>
{
    protected override Projectile Read(BinaryReader reader) => Main.projectile.GetOrDefault(reader.ReadInt32());
    protected override void Write(ModPacket packet, Projectile value) => packet.Write(value?.whoAmI ?? -1);
}

#endregion