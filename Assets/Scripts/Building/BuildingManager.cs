using UnityEngine;

namespace Game.Building
{
    public class BuildingManager : Singleton<BuildingManager>
    {
        public void ConstructBuilding(BuildingData data, Vector3 position, Quaternion rotation, SnapPoint targetSnapPoint = null)
        {
            if (data == null) return;

            // 1. Double check validation
            if (!PlacementValidator.CanPlace(data, position, rotation, targetSnapPoint != null)) return;

            // 2. Consume Materials via Events
            if (BuildingEvents.RequestConsumeMaterial != null)
            {
                foreach (var cost in data.ConstructionCost)
                {
                    BuildingEvents.RequestConsumeMaterial.Invoke(cost.Item, cost.Amount);
                }
            }

            // 3. Spawn Object
            GameObject newObj = Instantiate(data.Prefab, position, rotation);
            
            // 4. Initialize Logic
            BuildingPiece piece = newObj.GetComponent<BuildingPiece>();
            if (piece == null) piece = newObj.AddComponent<BuildingPiece>();
            
            piece.Initialize(data);

            // 5. Occupy Snap Point
            if (targetSnapPoint != null)
            {
                targetSnapPoint.Occupy();
            }

            Debug.Log($"Constructed: {data.DisplayName}");
        }
    }
}
