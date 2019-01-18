using System.Collections.Generic;
using System.Linq;
using Group = Rhisis.World.Game.Structures.Party;

namespace Rhisis.World.Systems.Party
{
    public class PartyManager
    {
        /// <summary>
        /// Gets the parties
        /// </summary>
        public List<Group> Parties { get; }

        /// <summary>
        /// Creates a new <see cref="PartyManager"/> instance.
        /// </summary>
        public PartyManager()
        {
            Parties = new List<Group>();
        }

        /// <summary>
        /// Gets a party by party id.
        /// </summary>
        /// <param name="partyId"></param>
        /// <returns></returns>
        public Group GetPartyById(int partyId) => Parties.FirstOrDefault(x => x.Id == partyId);

        /// <summary>
        /// Creates a new party.
        /// </summary>
        public bool CreateParty(out Group party)
        {
            party = new Group(Parties.Count + 1);
            Parties.Add(party);
            return true;
        }

        /// <summary>
        /// Tries to remove the specified party.
        /// </summary>
        /// <param name="party"></param>
        /// <returns></returns>
        public bool RemoveParty(Group party)
        {
            return Parties.Remove(party);
        }
    }
}
