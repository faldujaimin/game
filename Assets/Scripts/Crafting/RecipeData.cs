using System.Collections.Generic;
using UnityEngine;
using Game.Inventory.Items;
using Game.Gathering;

namespace Game.Crafting
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe Data")]
    public class RecipeData : ScriptableObject
    {
        [Header("Identity")]
        public string RecipeID = System.Guid.NewGuid().ToString();
        public string DisplayName = "New Recipe";

        [Header("Output")]
        public ItemData OutputItem;
        public int OutputAmount = 1;
        public float CraftTimeSeconds = 2f;

        [Header("Requirements")]
        public List<RecipeIngredient> RequiredIngredients = new List<RecipeIngredient>();
        public CraftingStationType StationRequired = CraftingStationType.Hand;
        public ToolType RequiredTool = ToolType.None;
        
        [Header("Progression")]
        public bool RequiresKnowledge = false; // Does the player need to unlock a blueprint?
        public int RequiredPlayerLevel = 1;
        public int ExperienceReward = 5;
        
        [Range(0f, 100f)]
        public float SuccessChance = 100f; // Can be affected by player stats later
    }
}
