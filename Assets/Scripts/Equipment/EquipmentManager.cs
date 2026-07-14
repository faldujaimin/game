using UnityEngine;

namespace Game.Equipment
{
    public class EquipmentManager : Singleton<EquipmentManager>
    {
        public EquipmentContainer Container { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Container = new EquipmentContainer();
        }

        // Future enhancements will go here:
        // - Recalculate Player Stats when equipment changes
        // - Update Player Meshes (e.g., showing the Sword in the hand)
        // - Handling Durability degradation
    }
}
