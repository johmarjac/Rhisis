using Rhisis.World.Game.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rhisis.World.Game.Components
{
    public class PartyMemberContainerComponent : IEnumerable<IPlayerEntity>
    {
        /// <summary>
        /// Gets the maximum amount of party members.
        /// </summary>
        public int MaxPartyMembers { get; }

        /// <summary>
        /// Gets the party members.
        /// </summary>
        public List<IPlayerEntity> Members { get; }

        /// <summary>
        /// Gets the amount of members in the party.
        /// </summary>
        public int MemberCount => Members.Count(x => x != null);

        /// <summary>
        /// Creates a new <see cref="PartyMemberContainerComponent"/> instance.
        /// </summary>
        /// <param name="maxPartyMembers"></param>
        public PartyMemberContainerComponent(int maxPartyMembers)
        {
            MaxPartyMembers = maxPartyMembers;
            Members = new List<IPlayerEntity>(new IPlayerEntity[maxPartyMembers]);
        }

        /// <summary>
        /// Gets a party member by player id.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        public IPlayerEntity GetMember(int playerId) => Members.FirstOrDefault(x => x.PlayerData != null && x.PlayerData.Id == playerId);

        /// <summary>
        /// Tries to add the specified player to the party.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool AddMember(IPlayerEntity player)
        {
            if (MemberCount == MaxPartyMembers)
                return false;

            var availableSlot = GetAvailableMemberSlot();
            if (availableSlot == -1)
                return false;

            this[availableSlot] = player;

            return true;
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

            for(int i = 0; i < MaxPartyMembers; i++)
            {
                if (this[i] == member)
                {
                    this[i] = null;
                    member.Party.Party = null;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets a value indicating a free member slot in the party.
        /// </summary>
        /// <returns>-1 if there could not be found an available slot.</returns>
        public int GetAvailableMemberSlot()
        {
            for(int i = 0; i < MaxPartyMembers; i++)
            {
                if (this[i] == null)
                    return i;
            }

            return -1;
        }

        /// <summary>
        /// Gets or sets the specified member.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IPlayerEntity this[int index]
        {
            get
            {
                if (index >= MaxPartyMembers)
                    return null;

                return Members[index];
            }
            set
            {
                if (index > MaxPartyMembers)
                    return;

                Members[index] = value;
            }
        }

        public IEnumerator<IPlayerEntity> GetEnumerator()
        {
            for(int i = 0; i < MaxPartyMembers; i++)
            {
                if (this[i] != null)
                    yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
