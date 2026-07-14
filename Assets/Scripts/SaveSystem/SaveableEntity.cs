using UnityEngine;
using System.Collections.Generic;

namespace Game.SaveSystem
{
    /// <summary>
    /// Attached to GameObjects in the scene. 
    /// Finds all ISaveable components on this object and automatically registers them.
    /// </summary>
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string entityID = System.Guid.NewGuid().ToString();
        public string EntityID => entityID;
        
        [SerializeField] private string prefabID; // If spawned dynamically, this tells the loader what to spawn
        public string PrefabID => prefabID;

        private void OnEnable()
        {
            SaveRegistry.RegisterEntity(this);
        }

        private void OnDisable()
        {
            SaveRegistry.UnregisterEntity(this);
        }

        public EntitySaveData GenerateSaveData()
        {
            EntitySaveData data = new EntitySaveData
            {
                EntityID = this.entityID,
                PrefabID = this.prefabID
            };

            // Grab data from every ISaveable on this GameObject
            foreach (var saveable in GetComponents<ISaveable>())
            {
                // In a real project with NewtonSoft JSON:
                // data.ComponentData[saveable.GetSaveID()] = JsonConvert.SerializeObject(saveable.SaveState());
            }

            return data;
        }

        public void RestoreSaveData(EntitySaveData data)
        {
            foreach (var saveable in GetComponents<ISaveable>())
            {
                if (data.ComponentData.TryGetValue(saveable.GetSaveID(), out string jsonState))
                {
                    // var state = JsonConvert.DeserializeObject(jsonState, TYPE);
                    // saveable.LoadState(state);
                }
            }
        }
    }
}
