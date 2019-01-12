using Microsoft.Extensions.Logging;
using Rhisis.Core.DependencyInjection;
using Rhisis.Core.Structures.Configuration;
using Rhisis.World.Game.Common;
using Rhisis.World.Game.Core;
using Rhisis.World.Game.Core.Systems;
using Rhisis.World.Game.Entities;
using Rhisis.World.Packets;
using Rhisis.World.Systems.NpcBuff.EventArgs;
using System;

namespace Rhisis.World.Systems.NpcBuff
{
    [System(SystemType.Notifiable)]
    public class NpcBuffSystem : ISystem
    {
        private readonly ILogger Logger = DependencyContainer.Instance.Resolve<ILogger>();

        public WorldEntityType Type => WorldEntityType.Player;

        public void Execute(IEntity entity, SystemEventArgs args)
        {
            if (!(entity is IPlayerEntity player) || !(args is NpcBuffEventArgs buffEventArgs))
            {
                Logger.LogError("NpcBuffSystem: Invalid event arguments.");
                return;
            }

            if (!buffEventArgs.CheckArguments())
            {
                Logger.LogError("NpcBuffSystem: Invalid event action arguments.");
                return;
            }

            OnNpcBuff(player, buffEventArgs);
        }

        private void OnNpcBuff(IPlayerEntity player, NpcBuffEventArgs buffEventArgs)
        {
            var worldConfiguration = DependencyContainer.Instance.Resolve<WorldConfiguration>();

            WorldPacketFactory.SendSetSkillState(player, player, BuffType.BuffSkill, 46, worldConfiguration.NpcBuffs.NpcBuffLevel, (uint)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + 60);
        }
    }
}
