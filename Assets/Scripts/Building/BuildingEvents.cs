using System;
using Game.Inventory.Items;

namespace Game.Building
{
    public static class BuildingEvents
    {
        // Func delegates to decouple material checks from the Inventory system
        public static Func<ItemData, int, bool> RequestCheckMaterial;
        public static Func<ItemData, int, bool> RequestConsumeMaterial;

        // Broadcast events
        public static event Action<BuildingPiece> OnBuildingPlaced;
        public static event Action<BuildingPiece> OnBuildingDestroyed;
        public static event Action<BuildingPiece> OnBuildingUpgraded;
        
        public static event Action<BuildingData> OnBlueprintSelected;
    }
}
