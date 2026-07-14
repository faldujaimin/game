using UnityEngine;
using System.Collections;

namespace Game.SaveSystem
{
    public class AutoSaveManager : MonoBehaviour
    {
        public float autoSaveIntervalSeconds = 300f; // 5 minutes
        public string autoSaveSlotName = "AutoSave";

        private void Start()
        {
            StartCoroutine(AutoSaveRoutine());
        }

        private IEnumerator AutoSaveRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(autoSaveIntervalSeconds);
                if (SaveManager.Instance != null)
                {
                    Debug.Log("[AutoSave] Triggering Auto-Save...");
                    SaveManager.Instance.SaveGame(autoSaveSlotName);
                }
            }
        }
    }
}
