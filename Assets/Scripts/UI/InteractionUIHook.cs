using UnityEngine;
using Game.Core.Interaction;
using Game.Managers;

namespace Game.UI
{
    /// <summary>
    /// Hooks into the InteractionManager to display UI prompts.
    /// Completely decoupled from the Player, which is ideal for Multiplayer architecture.
    /// </summary>
    public class InteractionUIHook : MonoBehaviour
    {
        // In a real project, you'd reference TMPro.TextMeshProUGUI here:
        // public TMPro.TextMeshProUGUI promptText;
        
        [Header("Debug State")]
        public bool isShowingPrompt;
        public string currentPromptText;

        private void Start()
        {
            if (InteractionManager.Instance != null)
            {
                InteractionManager.Instance.OnFocusChanged += HandleFocusChanged;
            }
            HidePrompt();
        }

        private void OnDestroy()
        {
            if (InteractionManager.Instance != null)
            {
                InteractionManager.Instance.OnFocusChanged -= HandleFocusChanged;
            }
        }

        private void HandleFocusChanged(IInteractable interactable)
        {
            if (interactable != null)
            {
                ShowPrompt(interactable.GetInteractText());
            }
            else
            {
                HidePrompt();
            }
        }

        private void ShowPrompt(string text)
        {
            isShowingPrompt = true;
            currentPromptText = $"[E] {text}"; // Automatically formats the key prompt
            
            // Example real implementation:
            // promptText.text = currentPromptText;
            // promptText.gameObject.SetActive(true);
            
            Debug.Log($"UI: Shown -> {currentPromptText}");
        }

        private void HidePrompt()
        {
            isShowingPrompt = false;
            currentPromptText = string.Empty;
            
            // Example real implementation:
            // promptText.gameObject.SetActive(false);
        }
    }
}
