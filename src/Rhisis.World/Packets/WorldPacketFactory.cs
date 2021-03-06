﻿using Ether.Network.Packets;
using Rhisis.Network;
using Rhisis.Network.Packets;
using Rhisis.World.Game.Core;
using Rhisis.World.Game.Entities;
using System.Collections.Generic;
using System.Linq;
using Rhisis.Core.Data;

namespace Rhisis.World.Packets
{
    public static partial class WorldPacketFactory
    {
        public static void SendToVisible(INetPacketStream packet, IEntity entity, bool sendToPlayer = false)
        {
            IEnumerable<IPlayerEntity> visiblePlayers = from x in entity.Object.Entities
                                                        where x.Type == WorldEntityType.Player
                                                        select x as IPlayerEntity;

            foreach (IPlayerEntity visiblePlayer in visiblePlayers)
                visiblePlayer.Connection.Send(packet);

            if (sendToPlayer && entity is IPlayerEntity player)
                player.Connection.Send(packet);
        }

        public static void SendFollowTarget(IEntity entity, IEntity targetEntity, float distance)
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(entity.Id, SnapshotType.MOVERSETDESTOBJ);
                packet.Write(targetEntity.Id);
                packet.Write(distance);

                SendToVisible(packet, entity);
            }
        }

        public static void SendUpdateAttributes(IEntity entity, DefineAttributes attribute, int newValue)
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(entity.Id, SnapshotType.SETPOINTPARAM);
                packet.Write((int)attribute);
                packet.Write(newValue);
                
                SendToVisible(packet, entity, true);
            }
        }

        public static void SendWorldMsg(IPlayerEntity entity, string text)
        {
            using (var packet = new FFPacket())
            {
                packet.StartNewMergedPacket(entity.Id, SnapshotType.WORLDMSG, 0xFFFFFF00);
                packet.Write(text);

                entity.Connection.Send(packet);
            }
        }
    }
}
