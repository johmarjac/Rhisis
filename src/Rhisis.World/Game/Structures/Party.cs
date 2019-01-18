using Ether.Network.Packets;
using Rhisis.Core.DependencyInjection;
using Rhisis.Core.Structures.Configuration;
using Rhisis.World.Game.Common;
using Rhisis.World.Game.Components;
using Rhisis.World.Game.Entities;
using System;
using System.Linq;

namespace Rhisis.World.Game.Structures
{
    public class Party
    {
        /// <summary>
        /// Gets the Party Id.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets or sets the Party Name.
        /// </summary>
        public string Name { get; set; }
               
        /// <summary>
        /// Gets or sets the Party Level.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the Party Experience.
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Gets or sets the Party Points.
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Gets or sets the Experience Share Mode.
        /// </summary>
        public PartyExperienceShareMode ExperienceShareMode { get; set; }

        /// <summary>
        /// Gets or sets the Item Share Mode.
        /// </summary>
        public PartyItemShareMode ItemShareMode { get; set; }

        /// <summary>
        /// Gets a value indicating whether the party is advanced.
        /// </summary>
        public bool IsAdvanced => Level >= 10;

        /// <summary>
        /// Gets the Party Members.
        /// </summary>
        public PartyMemberContainerComponent PartyMemberContainer { get; }

        /// <summary>
        /// Gets a value indicating whether the party is full or not.
        /// </summary>
        public bool IsFull => PartyMemberContainer.MemberCount == PartyMemberContainer.MaxPartyMembers;
               
        /// <summary>
        /// Creates a new <see cref="Party"/> instance.
        /// </summary>
        /// <param name="id"></param>
        public Party(int id)
        {
            var worldConfiguration = DependencyContainer.Instance.Resolve<WorldConfiguration>();

            Id = id;
            Name = "Party";
            Level = 1;
            Experience = 0;
            Points = 0;
            PartyMemberContainer = new PartyMemberContainerComponent(worldConfiguration.PartyConfiguration.MaxPartyMemberCount);
        }

        /// <summary>
        /// Gets the party leader.
        /// </summary>
        /// <returns></returns>
        public IPlayerEntity PartyLeader => PartyMemberContainer.FirstOrDefault(x => x.PlayerData.Id == PartyLeaderId);

        /// <summary>
        /// Gets or sets the Party Leader Id.
        /// </summary>
        public int PartyLeaderId { get; set; }
        
        /// <summary>
        /// Tries to add the specified player to the party.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool AddMember(IPlayerEntity player, bool isPartyLeader = false)
        {
            if (player.Party.IsInParty)
                return false;

            if(PartyMemberContainer.AddMember(player))
            {
                if (isPartyLeader)
                    PartyLeaderId = player.PlayerData.Id;

                player.Party.Party = this;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to remove the specified player from the party.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public bool RemoveMember(IPlayerEntity member)
        {
            if (!member.Party.IsInParty)
                return false;

            if (member.Party.Party == this)
            {
                if(PartyMemberContainer.RemoveMember(member))
                {
                    if(member.PlayerData.Id == PartyLeaderId)
                    {
                        PartyLeaderId = PartyMemberContainer.FirstOrDefault().PlayerData.Id;
                    }
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Serializes the party to the packet.
        /// </summary>
        /// <param name="packet"></param>
        public void Serialize(INetPacketStream packet)
        {
            packet.Write(PartyMemberContainer.MemberCount);

            packet.Write(Id);
            packet.Write(Convert.ToInt32(IsAdvanced));
            packet.Write(PartyMemberContainer.MemberCount);
            packet.Write(Level);
            packet.Write(Experience);
            packet.Write(Points);
            packet.Write((int)ExperienceShareMode);
            packet.Write((int)ItemShareMode);
            packet.Write(0); // id Duell party?

            for (int i = 0; i < 5; i++) // i < MAX_PARTYMODE wtf?
                packet.Write(0); // m_nModeTime[i]

            if (IsAdvanced)
                packet.Write(Name);

            for(int i = 0; i < PartyMemberContainer.MaxPartyMembers; i++)
            {
                var member = PartyMemberContainer[i];
                if (member == null)
                    continue;

                packet.Write(member.PlayerData.Id);
                packet.Write(0); // bRemove
            }
        }
    }
}
