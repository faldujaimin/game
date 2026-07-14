using UnityEngine;

namespace Game.Inventory.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Data")]
    public class ItemData : ScriptableObject
    {
        [Header("Basic Info")]
        public string ID = System.Guid.NewGuid().ToString(); // Useful for networking and save systems
        public string ItemName = "New Item";
        
        [TextArea(3, 5)]
        public string Description = "Item description.";
        public Sprite Icon;

        [Header("Categorization")]
        public ItemCategory Category = ItemCategory.Resource;
        public ItemRarity Rarity = ItemRarity.Common;

        [Header("Stats")]
        public int MaxStack = 99;
        public float Weight = 0.1f;
        public int Value = 1;
    }
}
