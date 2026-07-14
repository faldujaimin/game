using System;
using Game.Inventory.Items;

namespace Game.Equipment
{
    [Serializable]
    public class EquipmentSlot
    {
        public EquipmentType SlotType;
        public ItemInstance Item;

        public bool IsEmpty => Item == null || Item.IsEmpty();

        public EquipmentSlot(EquipmentType type)
        {
            SlotType = type;
        }

        public void Clear()
        {
            Item = null;
        }
    }
}
