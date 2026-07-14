using System;
namespace Game.Equipment
{
    public static class EquipmentEvents
    {
        public static event Action<EquipmentType, EquipmentSlot> OnEquipmentChanged;
    }
}
