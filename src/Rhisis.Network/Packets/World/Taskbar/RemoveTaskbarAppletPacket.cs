using Ether.Network.Packets;

namespace Rhisis.Network.Packets.World.Taskbar
{
    public class RemoveTaskbarAppletPacket
    {
        public int SlotIndex { get; }

        public RemoveTaskbarAppletPacket(INetPacketStream packet)
        {
            SlotIndex = packet.Read<byte>();
        }
    }
}