using UnityEngine;
using Game.Inventory;

namespace Game.Hotbar
{
    public class HotbarManager : MonoBehaviour
    {
        public HotbarContainer Container { get; private set; }
        private InventoryManager inventoryManager;

        private void Awake()
        {
            Container = new HotbarContainer(10);
            inventoryManager = GetComponent<InventoryManager>();
        }

        private void Update()
        {
            HandleInput();
        }

        private void HandleInput()
        {
            // Number keys 1-9
            for (int i = 1; i <= 9; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    Container.SetSelected(i - 1);
                }
            }
            // 0 key maps to slot 10
            if (Input.GetKeyDown(KeyCode.Alpha0)) Container.SetSelected(9);

            // Mouse wheel scrolling
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                int next = Container.SelectedIndex - 1;
                if (next < 0) next = Container.MaxSlots - 1;
                Container.SetSelected(next);
            }
            else if (scroll < 0f)
            {
                int next = Container.SelectedIndex + 1;
                if (next >= Container.MaxSlots) next = 0;
                Container.SetSelected(next);
            }

            // Quick Use
            if (Input.GetMouseButtonDown(0))
            {
                UseSelectedItem();
            }
        }

        private void UseSelectedItem()
        {
            if (inventoryManager == null) return;

            int invIndex = Container.Slots[Container.SelectedIndex].InventoryIndex;
            if (invIndex == -1) return;

            var invSlot = inventoryManager.Container.Slots[invIndex];
            if (invSlot.IsEmpty) return;

            Debug.Log($"Using/Equipping: {invSlot.Item.Data.ItemName}");
            
            // Logic to consume food, equip sword, or swing tool goes here based on Item Category
        }
    }
}
