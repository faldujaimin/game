using System;

namespace Game.Hotbar
{
    [Serializable]
    public class HotbarSlot
    {
        // References the index of the slot in the InventoryContainer
        // -1 means the hotbar slot is empty
        public int InventoryIndex = -1;
        
        public bool IsEmpty => InventoryIndex == -1;
    }
}
