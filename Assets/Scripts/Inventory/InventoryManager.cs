using UnityEngine;
using Game.Inventory.Items;
using Game.Gathering;
using System.Collections.Generic;

namespace Game.Inventory
{
    /// <summary>
    /// The MonoBehaviour component that lives on the Player. 
    /// Acts as the bridge between Unity (Events, GameObjects) and the pure C# logic.
    /// </summary>
    public class InventoryManager : MonoBehaviour
    {
        [Header("Settings")]
        public int initialSlots = 20;
        public float initialMaxWeight = 100f;
        
        // Backend Logic 
        public InventoryContainer Container { get; private set; }

        private void Awake()
        {
            Container = new InventoryContainer(initialSlots, initialMaxWeight);
        }

        private void OnEnable()
        {
            // Connect to Gathering System automatically
            ResourceNode.OnResourceGathered += HandleResourceGathered;
        }

        private void OnDisable()
        {
            ResourceNode.OnResourceGathered -= HandleResourceGathered;
        }

        private void HandleResourceGathered(List<LootDrop> drops, Vector3 dropPosition)
        {
            foreach (var drop in drops)
            {
                if (drop.Item == null) continue;

                // Roll how much we got from the drop
                int amountToGive = Random.Range(drop.MinAmount, drop.MaxAmount + 1);

                // Add to inventory logic
                int remainder = Container.AddItem(drop.Item, amountToGive);

                if (remainder > 0)
                {
                    Debug.LogWarning($"Inventory Full/Overweight! Dropped {remainder}x {drop.Item.ItemName} at {dropPosition}");
                    // In a full game, instantiate physical item prefabs at dropPosition for the remainder here.
                }
            }
        }
        
        public void DropItemFromSlot(int slotIndex, int amount)
        {
            var slot = Container.Slots[slotIndex];
            if (slot.IsEmpty) return;

            // In a full game, instantiate the physical prefab in front of the player
            // Instantiate(slot.Item.Data.WorldPrefab, transform.position + transform.forward, Quaternion.identity);

            slot.Item.CurrentStack -= amount;
            if (slot.Item.CurrentStack <= 0) slot.Clear();
            
            Container.RecalculateWeight();
            
            // Notify UI
            InventoryEvents.OnSlotChanged?.Invoke(slotIndex, slot);
            InventoryEvents.OnInventoryChanged?.Invoke();
        }
    }
}
