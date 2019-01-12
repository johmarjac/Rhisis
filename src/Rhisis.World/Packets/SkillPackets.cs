using Rhisis.Network;
using Rhisis.Network.Packets;
using Rhisis.World.Game.Common;
using Rhisis.World.Game.Entities;
using System;

namespace Rhisis.World.Packets
{
    public partial class WorldPacketFactory
    {
        public static void SendSetSkillState(IPlayerEntity player, ILivingEntity target, BuffType buffType, ushort buffId, int buffLevel, uint buffTime)
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(player.Id, SnapshotType.SETSKILLSTATE);
                packet.Write(target.Id);
                packet.Write((ushort)buffType);
                packet.Write(buffId);

                if (buffType == BuffType.BuffItem2)
                {
                    var time = buffLevel - (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                    packet.Write(time);
                }
                else
                    packet.Write(buffLevel);

                packet.Write(buffTime);

                SendToVisible(packet, player, true);
            }
        }
    }
}
