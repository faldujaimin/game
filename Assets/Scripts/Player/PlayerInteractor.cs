using UnityEngine;
using Game.Core.Interaction;
using Game.Managers;

namespace Game.Player
{
    /// <summary>
    /// Attached to the player or player's camera. Handles physics raycasting for interactions.
    /// </summary>
    public class PlayerInteractor : MonoBehaviour
    {
        [Header("Settings")]
        public float interactRange = 3f;
        public float interactCooldown = 0.5f;
        public LayerMask interactLayer;
        public Transform raycastOrigin; // Usually the Main Camera

        private float _lastInteractTime;
        private IInteractable _currentInteractable;

        private void Update()
        {
            CheckForInteractable();
            HandleInput();
        }

        private void CheckForInteractable()
        {
            if (raycastOrigin == null) return;

            Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);
            
            if (Physics.Raycast(ray, out RaycastHit hit, interactRange, interactLayer))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                
                // Validate if it's interactable right now
                if (interactable != null && interactable.CanInteract(gameObject))
                {
                    SetCurrentInteractable(interactable);
                    return;
                }
            }
            
            // If we hit nothing, or something we can't interact with
            SetCurrentInteractable(null);
        }

        private void HandleInput()
        {
            // Hardcoded KeyCode.E for now, easily swappable to Input System
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_currentInteractable != null && Time.time >= _lastInteractTime + interactCooldown)
                {
                    _lastInteractTime = Time.time;
                    
                    // Execute Interaction
                    _currentInteractable.Interact(gameObject);
                    
                    // Notify Manager (for UI feedback, audio, or networking)
                    if (InteractionManager.Instance != null)
                    {
                        InteractionManager.Instance.NotifyInteraction(_currentInteractable, gameObject);
                    }
                }
            }
        }

        private void SetCurrentInteractable(IInteractable interactable)
        {
            if (_currentInteractable != interactable)
            {
                _currentInteractable = interactable;
                
                // Tell the InteractionManager so the UI can update
                if (InteractionManager.Instance != null)
                {
                    InteractionManager.Instance.SetFocus(_currentInteractable);
                }
            }
        }
    }
}
