using Rhisis.World.Game.Core.Systems;

namespace Rhisis.World.Systems.NpcBuff.EventArgs
{
    public class NpcBuffEventArgs : SystemEventArgs
    {
        public string NpcId { get; }

        public NpcBuffEventArgs(string npcId)
        {
            NpcId = npcId;
        }

        public override bool CheckArguments()
        {
            return !string.IsNullOrWhiteSpace(NpcId);
        }
    }
}
