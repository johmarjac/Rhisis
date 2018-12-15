using Ether.Network.Packets;

namespace Rhisis.Core.Common.Game.Structures
{
    public class Shortcut
    {
        public int SlotIndex { get; }

        public ShortcutType ShortcutType { get; }

        public uint ObjId { get; }

        public ShortcutObjType ObjType { get; }

        public uint ObjIndex { get; }

        public uint UserId { get; }

        public uint ObjData { get; }

        public string Text { get; }

        public Shortcut(int slotIndex, ShortcutType shortcutType, uint objId, ShortcutObjType shortcutObjType, uint objIndex, uint userId, uint objData, string text)
        {
            SlotIndex = slotIndex;
            ShortcutType = shortcutType;
            ObjId = objId;
            ObjType = shortcutObjType;
            ObjIndex = objIndex;
            UserId = userId;
            ObjData = objData;
            Text = text;
        }

        public void Serialize(INetPacketStream packet)
        {
            packet.Write(SlotIndex);
            packet.Write((uint)ShortcutType);
            packet.Write(ObjId);
            packet.Write((uint)ObjType);
            packet.Write(ObjIndex);
            packet.Write(UserId);
            packet.Write(ObjData);

            if (ShortcutType == ShortcutType.Chat)
                packet.Write(Text);
        }
    }
}