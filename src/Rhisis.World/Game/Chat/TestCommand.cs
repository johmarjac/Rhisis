using Rhisis.Network;
using Rhisis.Network.Packets;
using Rhisis.World.Game.Entities;
using Rhisis.World.Packets;

namespace Rhisis.World.Game.Chat
{
    public class TestCommand
    {
        [ChatCommand(".test", Rhisis.Core.Common.AuthorityType.Administrator)]
        public static void OnTest(IPlayerEntity player, string[] parameters)
        {
            if (int.TryParse(parameters[0], out var nAP))
            {
                WorldPacketFactory.SendSetActionPoint(player, nAP);
            }
            else
                WorldPacketFactory.SendWorldMsg(player, "Not a valid integer.");
        }
    }
}