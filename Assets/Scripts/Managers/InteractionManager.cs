using UnityEngine;
using Game.Core.Interaction;
using System;

namespace Game.Managers
{
    /// <summary>
    /// Central hub for interaction events. Decouples the player raycasting logic from the UI.
    /// Crucial for multiplayer so local UI updates without tightly coupling to the network player object.
    /// </summary>
    public class InteractionManager : Singleton<InteractionManager>
    {
        // Event fired when the local player's crosshair focuses on a new interactable
        public event Action<IInteractable> OnFocusChanged;
        
        // Event fired when a successful interaction occurs
        public event Action<IInteractable, GameObject> OnInteractSuccess;

        private IInteractable _currentFocus;

        public void SetFocus(IInteractable newFocus)
        {
            if (_currentFocus != newFocus)
            {
                _currentFocus = newFocus;
                OnFocusChanged?.Invoke(_currentFocus);
            }
        }

        public void NotifyInteraction(IInteractable interactable, GameObject interactor)
        {
            OnInteractSuccess?.Invoke(interactable, interactor);
            
            // Multiplayer Future-Proofing: 
            // If this is a client, we might trigger a network message here to ask the server 
            // to validate and execute the interaction globally.
        }
    }
}
