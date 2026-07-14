using System;
using Game.Inventory.Items;

namespace Game.Inventory
{
    /// <summary>
    /// Pure C# class representing a single slot in an inventory.
    /// Does not inherit from MonoBehaviour.
    /// </summary>
    [Serializable]
    public class InventorySlot
    {
        public ItemInstance Item;

        public bool IsEmpty => Item == null || Item.IsEmpty();
        public bool IsFull => !IsEmpty && Item.CurrentStack >= Item.Data.MaxStack;

        public void Clear()
        {
            Item = null;
        }
    }
}
