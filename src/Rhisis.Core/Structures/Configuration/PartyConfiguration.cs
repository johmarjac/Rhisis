using System.Runtime.Serialization;

namespace Rhisis.Core.Structures.Configuration
{
    [DataContract]
    public class PartyConfiguration
    {
        public const int DefaultMaxPartyMemberCount = 8;
        public const string DefaultPartyName = "Party";

        /// <summary>
        /// Gets or sets the maximum amount of members in a party.
        /// </summary>
        public int MaxPartyMemberCount { get; set; }

        /// <summary>
        /// Gets or sets the party standard name upon party creation.
        /// </summary>
        public string PartyStandardName { get; set; }

        /// <summary>
        /// Creates a new <see cref="PartyConfiguration"/> instance.
        /// </summary>
        public PartyConfiguration()
        {
            MaxPartyMemberCount = DefaultMaxPartyMemberCount;
            PartyStandardName = DefaultPartyName;
        }
    }
}
