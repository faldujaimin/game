using System;
using System.Collections.Generic;
using UnityEngine;
using Game.Inventory.Items;

namespace Game.Inventory
{
    /// <summary>
    /// The core backend logic for an Inventory. 
    /// Pure C# for optimal testing and multiplayer serialization.
    /// </summary>
    [Serializable]
    public class InventoryContainer
    {
        public int MaxSlots = 20;
        public float MaxWeight = 100f;
        public float CurrentWeight { get; private set; }

        public List<InventorySlot> Slots = new List<InventorySlot>();

        public InventoryContainer(int maxSlots, float maxWeight)
        {
            MaxSlots = maxSlots;
            MaxWeight = maxWeight;
            
            for (int i = 0; i < maxSlots; i++)
            {
                Slots.Add(new InventorySlot());
            }
        }

        public void RecalculateWeight()
        {
            float total = 0f;
            foreach (var slot in Slots)
            {
                if (!slot.IsEmpty)
                {
                    total += slot.Item.Data.Weight * slot.Item.CurrentStack;
                }
            }
            CurrentWeight = total;
            InventoryEvents.OnWeightChanged?.Invoke(CurrentWeight, MaxWeight);
        }

        // --- Core Features ---

        /// <summary>
        /// Adds an item, automatically stacking. Returns the amount that overflowed (could not fit).
        /// </summary>
        public int AddItem(ItemData itemData, int amount)
        {
            if (itemData == null || amount <= 0) return amount; 

            // 1. Weight Validation
            float totalWeightToAdd = itemData.Weight * amount;
            if (CurrentWeight + totalWeightToAdd > MaxWeight)
            {
                int maxCanCarry = Mathf.FloorToInt((MaxWeight - CurrentWeight) / itemData.Weight);
                if (maxCanCarry <= 0) return amount; // Reject all
                amount = maxCanCarry; // Cap to max carry
            }

            int amountLeft = amount;

            // 2. Try to merge with existing non-full stacks
            for (int i = 0; i < MaxSlots; i++)
            {
                if (amountLeft <= 0) break;
                if (!Slots[i].IsEmpty && Slots[i].Item.Data == itemData && !Slots[i].IsFull)
                {
                    int spaceLeft = Slots[i].Item.Data.MaxStack - Slots[i].Item.CurrentStack;
                    int amountToAdd = Mathf.Min(spaceLeft, amountLeft);

                    Slots[i].Item.CurrentStack += amountToAdd;
                    amountLeft -= amountToAdd;
                    
                    InventoryEvents.OnSlotChanged?.Invoke(i, Slots[i]);
                }
            }

            // 3. Add to empty slots if still have amountLeft
            if (amountLeft > 0)
            {
                for (int i = 0; i < MaxSlots; i++)
                {
                    if (amountLeft <= 0) break;
                    if (Slots[i].IsEmpty)
                    {
                        int amountToAdd = Mathf.Min(itemData.MaxStack, amountLeft);
                        Slots[i].Item = new ItemInstance(itemData, amountToAdd);
                        amountLeft -= amountToAdd;
                        
                        InventoryEvents.OnSlotChanged?.Invoke(i, Slots[i]);
                    }
                }
            }

            int amountAdded = amount - amountLeft;
            if (amountAdded > 0)
            {
                RecalculateWeight();
                InventoryEvents.OnItemAdded?.Invoke(new ItemInstance(itemData, amountAdded), amountAdded);
                InventoryEvents.OnInventoryChanged?.Invoke();
            }

            return amountLeft; // Return overflow
        }

        public bool RemoveItem(ItemData itemData, int amount)
        {
            if (!ContainsItem(itemData, amount)) return false;

            int amountLeftToRemove = amount;

            for (int i = 0; i < MaxSlots; i++)
            {
                if (amountLeftToRemove <= 0) break;

                if (!Slots[i].IsEmpty && Slots[i].Item.Data == itemData)
                {
                    int amountToRemoveFromSlot = Mathf.Min(Slots[i].Item.CurrentStack, amountLeftToRemove);
                    Slots[i].Item.CurrentStack -= amountToRemoveFromSlot;
                    amountLeftToRemove -= amountToRemoveFromSlot;

                    if (Slots[i].Item.CurrentStack <= 0)
                    {
                        Slots[i].Clear();
                    }

                    InventoryEvents.OnSlotChanged?.Invoke(i, Slots[i]);
                }
            }

            RecalculateWeight();
            InventoryEvents.OnItemRemoved?.Invoke(new ItemInstance(itemData, amount), amount);
            InventoryEvents.OnInventoryChanged?.Invoke();
            return true;
        }

        public bool ContainsItem(ItemData itemData, int requiredAmount)
        {
            int totalFound = 0;
            foreach (var slot in Slots)
            {
                if (!slot.IsEmpty && slot.Item.Data == itemData)
                {
                    totalFound += slot.Item.CurrentStack;
                    if (totalFound >= requiredAmount) return true;
                }
            }
            return false;
        }

        // --- UI Operations ---

        public void SwapSlots(int indexA, int indexB)
        {
            if (indexA < 0 || indexA >= MaxSlots || indexB < 0 || indexB >= MaxSlots) return;

            var temp = Slots[indexA].Item;
            Slots[indexA].Item = Slots[indexB].Item;
            Slots[indexB].Item = temp;

            InventoryEvents.OnSlotChanged?.Invoke(indexA, Slots[indexA]);
            InventoryEvents.OnSlotChanged?.Invoke(indexB, Slots[indexB]);
            InventoryEvents.OnInventoryChanged?.Invoke();
        }

        public void SplitStack(int sourceIndex, int destinationIndex, int amount)
        {
            if (sourceIndex < 0 || sourceIndex >= MaxSlots || destinationIndex < 0 || destinationIndex >= MaxSlots) return;
            
            var sourceSlot = Slots[sourceIndex];
            var destSlot = Slots[destinationIndex];

            if (sourceSlot.IsEmpty || sourceSlot.Item.CurrentStack <= amount) return;
            if (!destSlot.IsEmpty) return; // Must split into an empty slot

            destSlot.Item = new ItemInstance(sourceSlot.Item.Data, amount, sourceSlot.Item.Condition);
            sourceSlot.Item.CurrentStack -= amount;

            InventoryEvents.OnSlotChanged?.Invoke(sourceIndex, sourceSlot);
            InventoryEvents.OnSlotChanged?.Invoke(destinationIndex, destSlot);
            InventoryEvents.OnInventoryChanged?.Invoke();
        }

        public void MergeStacks(int sourceIndex, int destinationIndex)
        {
            if (sourceIndex < 0 || sourceIndex >= MaxSlots || destinationIndex < 0 || destinationIndex >= MaxSlots) return;
            
            var sourceSlot = Slots[sourceIndex];
            var destSlot = Slots[destinationIndex];

            if (sourceSlot.IsEmpty || destSlot.IsEmpty || sourceSlot.Item.Data != destSlot.Item.Data) return;

            int spaceLeft = destSlot.Item.Data.MaxStack - destSlot.Item.CurrentStack;
            int amountToMove = Mathf.Min(spaceLeft, sourceSlot.Item.CurrentStack);

            if (amountToMove > 0)
            {
                destSlot.Item.CurrentStack += amountToMove;
                sourceSlot.Item.CurrentStack -= amountToMove;

                if (sourceSlot.Item.CurrentStack <= 0) sourceSlot.Clear();

                InventoryEvents.OnSlotChanged?.Invoke(sourceIndex, sourceSlot);
                InventoryEvents.OnSlotChanged?.Invoke(destinationIndex, destSlot);
                InventoryEvents.OnInventoryChanged?.Invoke();
            }
        }
    }
}
