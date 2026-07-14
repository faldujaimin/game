using System;
using UnityEngine;

namespace Game.Inventory.Items
{
    /// <summary>
    /// Represents an actual instance of an item in the game world or an inventory.
    /// Contains runtime data that changes, like stack count or durability.
    /// </summary>
    [Serializable]
    public class ItemInstance
    {
        public ItemData Data;
        public int CurrentStack;
        public float Condition; // E.g., Durability for tools/weapons

        public ItemInstance(ItemData data, int stack = 1, float condition = 100f)
        {
            Data = data;
            CurrentStack = Mathf.Clamp(stack, 1, data != null ? data.MaxStack : 1);
            Condition = condition;
        }

        public bool IsEmpty()
        {
            return Data == null || CurrentStack <= 0;
        }

        // Multiplayer / Saving note: 
        // When syncing or saving, you might only serialize Data.ID, CurrentStack, and Condition
        // to minimize network traffic and save file size.
    }
}
