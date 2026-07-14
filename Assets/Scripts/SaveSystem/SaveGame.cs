using System;
using System.Collections.Generic;

namespace Game.SaveSystem
{
    /// <summary>
    /// The root save file structure.
    /// </summary>
    [Serializable]
    public class SaveGame
    {
        public string SaveVersion = "1.0.0";
        public string SaveTime;
        public string SaveSlot;

        // Global state (Time, Weather, Global Economy, etc.)
        public Dictionary<string, string> GlobalData = new Dictionary<string, string>();

        // Entity state (Buildings, Player, Inventories, Dropped Items)
        public List<EntitySaveData> Entities = new List<EntitySaveData>();
    }
}
