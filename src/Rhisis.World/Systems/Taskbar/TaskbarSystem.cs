using Microsoft.Extensions.Logging;
using Rhisis.Core.Common;
using Rhisis.Core.Common.Game.Structures;
using Rhisis.Core.DependencyInjection;
using Rhisis.World.Game.Core;
using Rhisis.World.Game.Core.Systems;
using Rhisis.World.Game.Entities;
using Rhisis.World.Systems.Taskbar.EventArgs;
using System;

namespace Rhisis.World.Systems.Taskbar
{
    [System(SystemType.Notifiable)]
    public class TaskbarSystem : ISystem
    {
        private static ILogger Logger => DependencyContainer.Instance.Resolve<ILogger<TaskbarSystem>>();

        public const int MaxTaskbarApplets = 18;
        public const int MaxTaskbarItems = 9;
        public const int MaxTaskbarItemLevels = 8;
        public const int MaxTaskbarQueue = 5;

        public WorldEntityType Type => WorldEntityType.Player;

        public void Execute(IEntity entity, SystemEventArgs args)
        {
            if (!(entity is IPlayerEntity player))
                return;

            if (!args.CheckArguments())
            {
                Logger.LogWarning("Invalid arguments received.");
                return;
            }

            switch (args)
            {
                case AddTaskbarAppletEventArgs e:
                    HandleAddTaskbarApplet(player, e);
                    break;
                case RemoveTaskbarAppletEventArgs e:
                    HandleRemoveTaskbarApplet(player, e);
                    break;
                case AddTaskbarItemEventArgs e:
                    HandleAddTaskbarItem(player, e);
                    break;
                case RemoveTaskbarItemEventArgs e:
                    HandleRemoveTaskbarItem(player, e);
                    break;
                case TaskbarSkillEventArgs e:
                    HandleTaskbarSkill(player, e);
                    break;
            }
        }

        private void HandleAddTaskbarApplet(IPlayerEntity player, AddTaskbarAppletEventArgs e)
        {
            player.Taskbar.Applets.CreateShortcut(new Shortcut(e.SlotIndex, e.Type, e.ObjId, e.ObjType, e.ObjIndex, e.UserId, e.ObjData, e.Text));
            Logger.LogDebug("Created Applet Shortcut of type {0} on slot {1} for player {2}", Enum.GetName(typeof(ShortcutType), e.Type), e.SlotIndex, player.Object.Name);
        }

        private void HandleRemoveTaskbarApplet(IPlayerEntity player, RemoveTaskbarAppletEventArgs e)
        {
            player.Taskbar.Applets.RemoveShortcut(e.SlotIndex);
            Logger.LogDebug("Removed Applet Shortcut on slot {0} of player {1}", e.SlotIndex, player.Object.Name);
        }

        private void HandleAddTaskbarItem(IPlayerEntity player, AddTaskbarItemEventArgs e)
        {
            player.Taskbar.Items.CreateShortcut(new Shortcut(e.SlotIndex, e.Type, e.ObjId, e.ObjType, e.ObjIndex, e.UserId, e.ObjData, e.Text), e.SlotLevelIndex);
            Logger.LogDebug("Created Item Shortcut of type {0} on slot {1} for player {2}", Enum.GetName(typeof(ShortcutType), e.Type), e.SlotIndex, player.Object.Name);
        }

        private void HandleRemoveTaskbarItem(IPlayerEntity player, RemoveTaskbarItemEventArgs e)
        {
            player.Taskbar.Items.RemoveShortcut(e.SlotLevelIndex, e.SlotIndex);
            Logger.LogDebug("Removed Item Shortcut on slot {0}-{1} of player {2}", e.SlotLevelIndex, e.SlotIndex, player.Object.Name);
        }

        private void HandleTaskbarSkill(IPlayerEntity player, TaskbarSkillEventArgs e)
        {
            player.Taskbar.Queue.ClearQueue();
            player.Taskbar.Queue.CreateShortcuts(e.Skills);
            Logger.LogDebug("Handled Actionslot Shortcuts of player {0}", player.Object.Name);
        }
    }
}