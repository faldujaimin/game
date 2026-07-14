using UnityEngine;

namespace Game.Core.Interaction
{
    public interface IInteractable
    {
        /// <summary>
        /// The text prompt to display on the UI (e.g., "Chop Tree" or "Open Chest")
        /// </summary>
        string GetInteractText();

        /// <summary>
        /// Validates if the interactor is allowed to interact right now.
        /// Useful for locked doors, checking inventory space, or multiplayer state checks.
        /// </summary>
        bool CanInteract(GameObject interactor);

        /// <summary>
        /// Executes the actual interaction logic.
        /// </summary>
        void Interact(GameObject interactor);
        
        /// <summary>
        /// Returns the transform of the interactable (useful for distance checks).
        /// </summary>
        Transform GetTransform();
    }
}
