using System;
using System.Collections.Generic;

namespace Game.SaveSystem
{
    /// <summary>
    /// Pure C# wrapper for a specific entity's data in the world (e.g. a Tree or a Player)
    /// </summary>
    [Serializable]
    public class EntitySaveData
    {
        public string EntityID;
        public string PrefabID; // Needed if this entity must be spawned at load time (e.g. built walls)
        
        // ComponentID -> JSON serialized state
        // Dictionary allows us to save multiple ISaveables on a single GameObject
        public Dictionary<string, string> ComponentData = new Dictionary<string, string>();
    }
}
