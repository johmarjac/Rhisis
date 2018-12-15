using Ether.Network.Packets;
using Rhisis.Core.Common;

namespace Rhisis.Network.Packets.World.Taskbar
{
    public class AddTaskbarAppletPacket
    {
        public int SlotIndex { get; }

        public ShortcutType ShortcutType { get; }

        public uint ObjId { get; }

        public ShortcutObjType ObjType { get; }

        public uint ObjIndex { get; }

        public uint UserId { get; }

        public uint ObjData { get; }

        public string Text { get; }

        public AddTaskbarAppletPacket(INetPacketStream packet)
        {
            SlotIndex = packet.Read<byte>();
            ShortcutType = (ShortcutType)packet.Read<uint>();
            ObjId = packet.Read<uint>();
            ObjType = (ShortcutObjType)packet.Read<uint>();
            ObjIndex = packet.Read<uint>();
            UserId = packet.Read<uint>();
            ObjData = packet.Read<uint>();

            if (ShortcutType == ShortcutType.Chat)
                Text = packet.Read<string>();
        }
    }
}