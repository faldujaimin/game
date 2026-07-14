using System;
using UnityEngine;
using Game.Inventory.Items;

namespace Game.Gathering
{
    [Serializable]
    public class LootDrop
    {
        public ItemData Item;
        
        [Range(0f, 100f)]
        public float DropChance = 100f;
        
        public int MinAmount = 1;
        public int MaxAmount = 1;
    }
}
