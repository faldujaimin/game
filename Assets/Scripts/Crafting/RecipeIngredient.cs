using System;
using Game.Inventory.Items;

namespace Game.Crafting
{
    [Serializable]
    public class RecipeIngredient
    {
        public ItemData Item;
        public int Amount = 1;
    }
}
