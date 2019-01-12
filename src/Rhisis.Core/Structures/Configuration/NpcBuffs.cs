using System.Runtime.Serialization;

namespace Rhisis.Core.Structures.Configuration
{
    [DataContract]
    public class NpcBuffs
    {
        public const int DefaultNpcBuffLevel = 7;

        /// <summary>
        /// Gets or sets the Npc Buff Level.
        /// </summary>
        [DataMember(Name = "npcBuffLevel")]
        public int NpcBuffLevel { get; set; }

        /// <summary>
        /// Creates a new <see cref="NpcBuffs"/> instance.
        /// </summary>
        public NpcBuffs()
        {
            NpcBuffLevel = DefaultNpcBuffLevel;
        }
    }
}
