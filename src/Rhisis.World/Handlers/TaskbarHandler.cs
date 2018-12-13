using Ether.Network.Packets;
using Rhisis.Core.Common;
using Rhisis.Network;
using Rhisis.Network.Packets;
using Rhisis.Network.Packets.World.Taskbar;
using Rhisis.World.Systems.Taskbar;
using Rhisis.World.Systems.Taskbar.EventArgs;

namespace Rhisis.World.Handlers
{
    public static class TaskbarHandler
    {
        [PacketHandler(PacketType.ADDAPPLETTASKBAR)]
        public static void OnAddTaskbarApplet(WorldClient client, INetPacketStream packet)
        {
            var addTaskbarShortcutPacket = new AddTaskbarShortcutPacket(packet);
            var addTaskbarShortcutEventArgs = new AddTaskbarAppletEventArgs(addTaskbarShortcutPacket.SlotIndex, addTaskbarShortcutPacket.ShortcutType, addTaskbarShortcutPacket.ObjId, addTaskbarShortcutPacket.ObjType, addTaskbarShortcutPacket.ObjIndex, addTaskbarShortcutPacket.UserId, addTaskbarShortcutPacket.ObjData, addTaskbarShortcutPacket.Text);

            client.Player.NotifySystem<TaskbarSystem>(addTaskbarShortcutEventArgs);
        }

        [PacketHandler(PacketType.REMOVEAPPLETTASKBAR)]
        public static void OnRemoveTaskbarApplet(WorldClient client, INetPacketStream packet)
        {
            var removeTaskbarAppletPacket = new RemoveTaskbarAppletPacket(packet);
            var removeTaskbarAppletEventArgs = new RemoveTaskbarAppletEventArgs(removeTaskbarAppletPacket.SlotIndex);

            client.Player.NotifySystem<TaskbarSystem>(removeTaskbarAppletEventArgs);
        }

        [PacketHandler(PacketType.ADDITEMTASKBAR)]
        public static void OnAddTaskbarItem(WorldClient client, INetPacketStream packet)
        {
            var addTaskbarItemPacket = new AddTaskbarItemPacket(packet);
            var addTaskbarItemEventArgs = new AddTaskbarItemEventArgs(addTaskbarItemPacket.SlotLevelIndex, addTaskbarItemPacket.SlotIndex, addTaskbarItemPacket.ShortcutType, addTaskbarItemPacket.ObjId, addTaskbarItemPacket.ObjType, addTaskbarItemPacket.ObjIndex, addTaskbarItemPacket.UserId, addTaskbarItemPacket.ObjData, addTaskbarItemPacket.Text);
                       
            client.Player.NotifySystem<TaskbarSystem>(addTaskbarItemEventArgs);
        }

        [PacketHandler(PacketType.REMOVEITEMTASKBAR)]
        public static void OnRemoveTaskbarItem(WorldClient client, INetPacketStream packet)
        {
            var removeTaskbarItemPacket = new RemoveTaskbarItemPacket(packet);
            var removeTaskbarItemEventArgs = new RemoveTaskbarItemEventArgs(removeTaskbarItemPacket.SlotLevelIndex, removeTaskbarItemPacket.SlotIndex);

            client.Player.NotifySystem<TaskbarSystem>(removeTaskbarItemEventArgs);
        }
    }
}