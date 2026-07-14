using UnityEngine;
using System.Collections.Generic;

namespace Game.Building
{
    /// <summary>
    /// Attached to specific socket points on a building prefab (e.g., the 4 corners of a foundation).
    /// </summary>
    public class SnapPoint : MonoBehaviour
    {
        public List<BuildingCategory> AllowedCategories = new List<BuildingCategory>();
        public bool IsOccupied;

        public void Occupy()
        {
            IsOccupied = true;
        }

        public void Free()
        {
            IsOccupied = false;
        }
    }
}
