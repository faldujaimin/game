using System;
using Game.Inventory.Items;

namespace Game.Crafting
{
    public static class CraftingEvents
    {
        // Funcs are used as callbacks to ask systems (like Inventory) questions without direct references
        public static Func<ItemData, int, bool> RequestCheckIngredient;
        public static Func<ItemData, int, bool> RequestConsumeIngredient;
        public static Action<ItemData, int> RequestAddOutput;

        // Queue events for UI
        public static event Action<CraftingJob> OnJobStarted;
        public static event Action<CraftingJob> OnJobCompleted;
        public static event Action<CraftingJob> OnJobCancelled;
        public static event Action<CraftingJob> OnJobFailed;
    }
}
