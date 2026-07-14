using System;
using System.Collections.Generic;
using Game.Inventory.Items;

namespace Game.Equipment
{
    [Serializable]
    public class EquipmentContainer
    {
        public Dictionary<EquipmentType, EquipmentSlot> Slots = new Dictionary<EquipmentType, EquipmentSlot>();

        public EquipmentContainer()
        {
            foreach (EquipmentType type in Enum.GetValues(typeof(EquipmentType)))
            {
                Slots[type] = new EquipmentSlot(type);
            }
        }

        public bool Equip(EquipmentType targetSlot, ItemInstance itemToEquip, out ItemInstance previousItem)
        {
            previousItem = null;
            if (itemToEquip == null) return false;

            // In a full implementation, you would validate the itemToEquip here
            // e.g., if (itemToEquip.Data.EquipmentType != targetSlot) return false;

            previousItem = Slots[targetSlot].Item;
            Slots[targetSlot].Item = itemToEquip;
            
            EquipmentEvents.OnEquipmentChanged?.Invoke(targetSlot, Slots[targetSlot]);
            return true;
        }

        public ItemInstance Unequip(EquipmentType targetSlot)
        {
            var item = Slots[targetSlot].Item;
            Slots[targetSlot].Clear();
            
            EquipmentEvents.OnEquipmentChanged?.Invoke(targetSlot, Slots[targetSlot]);
            return item;
        }
    }
}
