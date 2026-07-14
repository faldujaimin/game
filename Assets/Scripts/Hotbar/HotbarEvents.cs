using System;
namespace Game.Hotbar
{
    public static class HotbarEvents
    {
        public static event Action<int, HotbarSlot> OnHotbarSlotChanged;
        public static event Action<int> OnHotbarSelectedChanged;
    }
}
