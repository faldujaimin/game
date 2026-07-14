using System;

namespace Game.SaveSystem
{
    public static class SaveEvents
    {
        public static event Action<string> OnGameSaved; // slotName
        public static event Action<string> OnGameLoaded;
        public static event Action OnSaveFailed;
        public static event Action OnLoadFailed;
    }
}
