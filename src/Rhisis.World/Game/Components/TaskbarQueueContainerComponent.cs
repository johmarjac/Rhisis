using Ether.Network.Packets;
using Rhisis.Core.Common;
using Rhisis.Core.Common.Game.Structures;
using Rhisis.World.Systems.Taskbar;
using System.Collections.Generic;
using System.Linq;

namespace Rhisis.World.Game.Components
{
    public class TaskbarQueueContainerComponent
    {
        public int MaxCapacity { get; }

        public List<Shortcut> Shortcuts { get; }

        public TaskbarQueueContainerComponent(int maxCapacity)
        {
            MaxCapacity = maxCapacity;
            Shortcuts = new List<Shortcut>(new Shortcut[maxCapacity]);
        }

        public void ClearQueue()
        {
            Shortcuts.ForEach(x => x = null);
        }

        public void CreateShortcuts(List<Shortcut> shortcuts)
        {
            if (shortcuts.Count > Shortcuts.Count)
                return;

            for (int i = 0; i < shortcuts.Count; i++)
                Shortcuts[i] = shortcuts.FirstOrDefault(x => x.SlotIndex == i);
        }

        public int Count
        {
            get
            {
                return Shortcuts.Count(x => x != null && x.ShortcutType != ShortcutType.None);
            }
        }

        public void Serialize(INetPacketStream packet)
        {
            packet.Write(Count);
            for (int i = 0; i < MaxCapacity; i++)
            {
                if (Shortcuts[i] != null && Shortcuts[i].ShortcutType != ShortcutType.None)
                {
                    Shortcuts[i].Serialize(packet);
                }
            }
        }
    }
}