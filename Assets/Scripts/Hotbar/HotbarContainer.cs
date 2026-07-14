using System;
using System.Collections.Generic;

namespace Game.Hotbar
{
    [Serializable]
    public class HotbarContainer
    {
        public int MaxSlots = 10;
        public List<HotbarSlot> Slots = new List<HotbarSlot>();
        public int SelectedIndex = 0;

        public HotbarContainer(int maxSlots = 10)
        {
            MaxSlots = maxSlots;
            for (int i = 0; i < maxSlots; i++)
            {
                Slots.Add(new HotbarSlot());
            }
        }

        public void AssignInventorySlot(int hotbarIndex, int inventoryIndex)
        {
            if (hotbarIndex < 0 || hotbarIndex >= MaxSlots) return;
            Slots[hotbarIndex].InventoryIndex = inventoryIndex;
            HotbarEvents.OnHotbarSlotChanged?.Invoke(hotbarIndex, Slots[hotbarIndex]);
        }

        public void ClearSlot(int hotbarIndex)
        {
            if (hotbarIndex < 0 || hotbarIndex >= MaxSlots) return;
            Slots[hotbarIndex].InventoryIndex = -1;
            HotbarEvents.OnHotbarSlotChanged?.Invoke(hotbarIndex, Slots[hotbarIndex]);
        }

        public void SetSelected(int index)
        {
            if (index < 0 || index >= MaxSlots) return;
            SelectedIndex = index;
            HotbarEvents.OnHotbarSelectedChanged?.Invoke(SelectedIndex);
        }
    }
}
