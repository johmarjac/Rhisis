using Ether.Network.Packets;
using System;

namespace Rhisis.Network.Packets.World
{
    /// <summary>
    /// Defines the <see cref="NpcBuffPacket"/> structure.
    /// </summary>
    public struct NpcBuffPacket : IEquatable<NpcBuffPacket>
    {
        public string NpcId { get; }

        /// <summary>
        /// Creates a new <see cref="NpcBuffPacket"/> instance.
        /// </summary>
        /// <param name="packet"></param>
        public NpcBuffPacket(INetPacketStream packet)
        {
            NpcId = packet.Read<string>();
        }

        /// <summary>
        /// Compares two <see cref="NpcBuffPacket"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(NpcBuffPacket other)
        {
            return NpcId == other.NpcId;
        }
    }
}
