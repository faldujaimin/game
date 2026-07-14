using System;
using Game.Inventory.Items;

namespace Game.Inventory
{
    /// <summary>
    /// Global events for the Inventory. The UI subscribes here to remain completely decoupled.
    /// </summary>
    public static class InventoryEvents
    {
        public static event Action<ItemInstance, int> OnItemAdded; 
        public static event Action<ItemInstance, int> OnItemRemoved;
        public static event Action OnInventoryChanged; // Generic refresh
        public static event Action<float, float> OnWeightChanged; // currentWeight, maxWeight
        public static event Action<int, InventorySlot> OnSlotChanged; // slotIndex, newSlotData
    }
}
