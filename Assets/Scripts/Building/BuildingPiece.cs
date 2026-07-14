using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Game.Building
{
    /// <summary>
    /// Attached to the actual spawned building object in the world.
    /// Tracks health, construction state, and upgrades.
    /// </summary>
    public class BuildingPiece : MonoBehaviour
    {
        public BuildingData Data { get; private set; }
        public int CurrentHealth { get; private set; }
        public bool IsConstructed { get; private set; }

        private List<SnapPoint> snapPoints = new List<SnapPoint>();

        public void Initialize(BuildingData data, bool instantBuild = false)
        {
            Data = data;
            CurrentHealth = data.MaxHealth;
            snapPoints = new List<SnapPoint>(GetComponentsInChildren<SnapPoint>());

            if (instantBuild || data.BuildTimeSeconds <= 0f)
            {
                FinishConstruction();
            }
            else
            {
                StartCoroutine(ConstructionRoutine());
            }
        }

        private IEnumerator ConstructionRoutine()
        {
            IsConstructed = false;
            // E.g., show scaffolding or transparent mesh here
            yield return new WaitForSeconds(Data.BuildTimeSeconds);
            FinishConstruction();
        }

        private void FinishConstruction()
        {
            IsConstructed = true;
            // Swap to solid material, enable colliders, etc.
            BuildingEvents.OnBuildingPlaced?.Invoke(this);
        }

        public void TakeDamage(int damage)
        {
            if (!IsConstructed) return;
            CurrentHealth -= damage;
            
            if (CurrentHealth <= 0)
            {
                DestroyBuilding();
            }
        }

        public void DestroyBuilding()
        {
            BuildingEvents.OnBuildingDestroyed?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
