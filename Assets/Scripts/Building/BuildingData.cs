using System.Collections.Generic;
using UnityEngine;
using Game.Crafting; 

namespace Game.Building
{
    [CreateAssetMenu(fileName = "New Building", menuName = "Building/Building Data")]
    public class BuildingData : ScriptableObject
    {
        [Header("Identity")]
        public string BuildingID = System.Guid.NewGuid().ToString();
        public string DisplayName = "New Building";
        public BuildingCategory Category = BuildingCategory.Foundation;

        [Header("Assets")]
        public GameObject Prefab; 
        public GameObject GhostPrefab; // Hologram for placement preview

        [Header("Construction")]
        public List<RecipeIngredient> ConstructionCost = new List<RecipeIngredient>();
        public float BuildTimeSeconds = 0f;
        
        [Header("Progression")]
        public BuildingData UpgradeTarget;
        public string RequiredTechnology = ""; 
        public CraftingStationType RequiredCraftingStation = CraftingStationType.Hand;

        [Header("Health")]
        public int MaxHealth = 1000;

        [Header("Placement Rules")]
        public LayerMask AllowedTerrain;
        public bool RequiresSnapPoint = false;
        public List<BuildingCategory> CanSnapTo = new List<BuildingCategory>();
    }
}
