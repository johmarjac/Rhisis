using Ether.Network.Packets;

namespace Rhisis.Network.Packets.World.Taskbar
{
    public class RemoveTaskbarItemPacket
    {
        public int SlotLevelIndex { get; }

        public int SlotIndex { get; }

        public RemoveTaskbarItemPacket(INetPacketStream packet)
        {
            SlotLevelIndex = packet.Read<byte>();
            SlotIndex = packet.Read<byte>();
        }
    }
}