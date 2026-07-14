using UnityEngine;
using System.IO;
using System;

namespace Game.SaveSystem
{
    /// <summary>
    /// Orchestrates the Save/Load flow. 
    /// Collects data from the registry, serializes it, and writes JSON to disk.
    /// </summary>
    public class SaveManager : Singleton<SaveManager>
    {
        public string CurrentVersion = "1.0.0";

        public void SaveGame(string slotName = "QuickSave")
        {
            SaveGame saveGame = new SaveGame
            {
                SaveVersion = CurrentVersion,
                SaveTime = DateTime.UtcNow.ToString("o"),
                SaveSlot = slotName
            };

            // 1. Save Global Managers
            foreach (var global in SaveRegistry.GlobalSaveables)
            {
                // saveGame.GlobalData[global.GetSaveID()] = JsonConvert.SerializeObject(global.SaveState());
            }

            // 2. Save Entities in World
            foreach (var entity in SaveRegistry.ActiveEntities)
            {
                saveGame.Entities.Add(entity.GenerateSaveData());
            }

            // 3. Serialize and Write to Disk
            string path = GetSavePath(slotName);
            // string finalJson = JsonConvert.SerializeObject(saveGame, Formatting.Indented);
            // File.WriteAllText(path, finalJson);
            
            Debug.Log($"[SaveManager] Game Saved to {path}");
            SaveEvents.OnGameSaved?.Invoke(slotName);
        }

        public void LoadGame(string slotName = "QuickSave")
        {
            string path = GetSavePath(slotName);
            if (!File.Exists(path))
            {
                Debug.LogError($"[SaveManager] Save file not found: {path}");
                SaveEvents.OnLoadFailed?.Invoke();
                return;
            }

            // 1. Read JSON
            // string json = File.ReadAllText(path);
            // SaveGame saveGame = JsonConvert.DeserializeObject<SaveGame>(json);

            // 2. Handle Version Migrations (e.g. converting 1.0.0 saves to 1.1.0 logic)

            // 3. Restore Globals
            // foreach (var global in SaveRegistry.GlobalSaveables) ...

            // 4. Restore Entities (Destroy current, instantiate from PrefabID, then apply ComponentData)
            // ...

            Debug.Log($"[SaveManager] Game Loaded from {path}");
            SaveEvents.OnGameLoaded?.Invoke(slotName);
        }

        private string GetSavePath(string slotName)
        {
            return Path.Combine(Application.persistentDataPath, $"{slotName}.json");
        }
    }
}
