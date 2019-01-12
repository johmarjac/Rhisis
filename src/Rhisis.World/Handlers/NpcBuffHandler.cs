using Ether.Network.Packets;
using Rhisis.Network;
using Rhisis.Network.Packets;
using Rhisis.Network.Packets.World;
using Rhisis.World.Systems.NpcBuff;
using Rhisis.World.Systems.NpcBuff.EventArgs;

namespace Rhisis.World.Handlers
{
    public static class NpcBuffHandler
    {
        [PacketHandler(PacketType.NPC_BUFF)]
        public static void OnNpcBuff(WorldClient client, INetPacketStream packet)
        {
            var npcBuffPacket = new NpcBuffPacket(packet);
            var npcBuffEventArgs = new NpcBuffEventArgs(npcBuffPacket.NpcId);

            client.Player.NotifySystem<NpcBuffSystem>(npcBuffEventArgs);
        }
    }
}
