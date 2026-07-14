using UnityEngine;

namespace Game.Inventory.UI
{
    /// <summary>
    /// Pure Logic hook for the UI. Subscribes to events and updates visual components.
    /// Does not store any inventory data itself.
    /// </summary>
    public class InventoryUIAdapter : MonoBehaviour
    {
        private void OnEnable()
        {
            InventoryEvents.OnInventoryChanged += RefreshUI;
            InventoryEvents.OnSlotChanged += UpdateSlot;
            InventoryEvents.OnWeightChanged += UpdateWeight;
        }

        private void OnDisable()
        {
            InventoryEvents.OnInventoryChanged -= RefreshUI;
            InventoryEvents.OnSlotChanged -= UpdateSlot;
            InventoryEvents.OnWeightChanged -= UpdateWeight;
        }

        private void RefreshUI()
        {
            // Loop through all UI slots and sync them with InventoryContainer
            Debug.Log("UI: Refreshing Entire Inventory Grid");
        }

        private void UpdateSlot(int slotIndex, InventorySlot slot)
        {
            // Only update the single slot that changed (highly performant)
            if (slot.IsEmpty)
            {
                Debug.Log($"UI: Slot {slotIndex} cleared.");
            }
            else
            {
                Debug.Log($"UI: Slot {slotIndex} updated -> {slot.Item.CurrentStack}x {slot.Item.Data.ItemName}");
            }
        }

        private void UpdateWeight(float currentWeight, float maxWeight)
        {
            // e.g. weightText.text = $"{currentWeight:F1} / {maxWeight:F1} kg";
            Debug.Log($"UI: Weight Updated -> {currentWeight}/{maxWeight}");
        }
    }
}
