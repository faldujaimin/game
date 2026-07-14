using UnityEngine;

namespace Game.Building
{
    /// <summary>
    /// Pure C# static class holding placement validation logic.
    /// Easily accessible by both Client (for ghost color) and Server (for anti-cheat).
    /// </summary>
    public static class PlacementValidator
    {
        public static bool CanPlace(BuildingData data, Vector3 position, Quaternion rotation, bool isSnapped)
        {
            if (data == null) return false;

            // 1. Check Snap Requirements
            if (data.RequiresSnapPoint && !isSnapped) return false;

            // 2. Validate Terrain (if not snapped)
            if (!isSnapped)
            {
                Ray ray = new Ray(position + Vector3.up, Vector3.down);
                if (!Physics.Raycast(ray, out RaycastHit hit, 2f, data.AllowedTerrain))
                {
                    return false; // Not on valid terrain
                }
            }

            // 3. Collision Detection (OverlapBox)
            Vector3 extents = new Vector3(0.9f, 0.9f, 0.9f); // Hardcoded scale for example
            Collider[] colliders = Physics.OverlapBox(position + Vector3.up, extents, rotation);
            
            foreach (var col in colliders)
            {
                // Ignore terrain layers and triggers
                if (!col.isTrigger && ((1 << col.gameObject.layer) & data.AllowedTerrain) == 0)
                {
                    return false; // Hitting another building, player, tree, etc.
                }
            }

            // 4. Validate Materials via Events
            if (BuildingEvents.RequestCheckMaterial != null)
            {
                foreach (var cost in data.ConstructionCost)
                {
                    if (!BuildingEvents.RequestCheckMaterial.Invoke(cost.Item, cost.Amount))
                    {
                        return false; // Missing materials
                    }
                }
            }
            else return false; // Fail safe if no inventory system is hooked up

            return true;
        }
    }
}
