using System.Collections.Generic;

namespace Game.SaveSystem
{
    /// <summary>
    /// Pure static registry that tracks every saveable object currently active in the scene.
    /// Eliminates the need for expensive FindObjectsOfType() calls during saving.
    /// </summary>
    public static class SaveRegistry
    {
        // World objects (buildings, NPCs, items)
        public static HashSet<SaveableEntity> ActiveEntities = new HashSet<SaveableEntity>();
        
        // Singletons/Managers (Time, Weather, PlayerStats)
        public static HashSet<ISaveable> GlobalSaveables = new HashSet<ISaveable>(); 

        public static void RegisterEntity(SaveableEntity entity)
        {
            ActiveEntities.Add(entity);
        }

        public static void UnregisterEntity(SaveableEntity entity)
        {
            ActiveEntities.Remove(entity);
        }

        public static void RegisterGlobal(ISaveable saveable)
        {
            GlobalSaveables.Add(saveable);
        }

        public static void UnregisterGlobal(ISaveable saveable)
        {
            GlobalSaveables.Remove(saveable);
        }
    }
}
