using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Core.Interaction;

namespace Game.Gathering
{
    public abstract class ResourceNode : MonoBehaviour, IInteractable
    {
        [Header("Resource Definition")]
        public ResourceNodeData NodeData;

        // Decoupled events! The Inventory system will listen to these, 
        // avoiding direct coupling between a Tree and the Player's backpack.
        public static event Action<List<LootDrop>, Vector3> OnResourceGathered;
        public static event Action<ResourceNode, int> OnResourceDestroyed; // Passes the node and XP

        protected int currentHealth;
        protected bool isDepleted;
        protected Collider nodeCollider;

        protected virtual void Awake()
        {
            nodeCollider = GetComponent<Collider>();
            ResetNode();
        }

        protected virtual void ResetNode()
        {
            if (NodeData == null) return;
            currentHealth = NodeData.MaxHealth;
            isDepleted = false;
            
            if (nodeCollider != null) nodeCollider.enabled = true;
            
            OnResetVisuals(); // Overridden by children (e.g., TreeNode resets stump)
        }

        // --- IInteractable Implementation ---

        public string GetInteractText()
        {
            if (isDepleted || NodeData == null) return string.Empty;
            return $"[{NodeData.RequiredTool}] Gather {NodeData.DisplayName}";
        }

        public bool CanInteract(GameObject interactor)
        {
            return !isDepleted;
        }

        public Transform GetTransform() => transform;

        public void Interact(GameObject interactor)
        {
            if (isDepleted) return;

            // In a full implementation, you would check interactor's currently equipped tool here.
            // For now, we assume the validation passes.
            bool hasRequiredTool = true; 
            
            if (hasRequiredTool)
            {
                Gather(interactor);
            }
            else
            {
                Debug.Log($"Requires {NodeData.RequiredTool} to gather.");
            }
        }

        // --- Gathering Flow ---

        protected virtual void Gather(GameObject interactor)
        {
            // 1. Damage Resource (Assume tool does 25 dmg per hit)
            int damage = 25; 
            currentHealth -= damage;

            // 2. Play Audio/Visuals
            if (NodeData.HitSound != null)
            {
                // AudioSource.PlayClipAtPoint(NodeData.HitSound, transform.position);
            }
            // Trigger animation on player here via events if needed

            // 3. Drop Partial Loot (Optional, depends on game design)
            GenerateLoot();

            // 4. Check Death
            if (currentHealth <= 0)
            {
                Deplete();
            }
        }

        protected void GenerateLoot()
        {
            List<LootDrop> drops = new List<LootDrop>();
            foreach (var loot in NodeData.LootTable)
            {
                float roll = UnityEngine.Random.Range(0f, 100f);
                if (roll <= loot.DropChance)
                {
                    drops.Add(loot);
                }
            }

            if (drops.Count > 0)
            {
                // Fire decoupled event. Inventory System handles adding it.
                OnResourceGathered?.Invoke(drops, transform.position);
            }
        }

        protected virtual void Deplete()
        {
            isDepleted = true;
            if (nodeCollider != null) nodeCollider.enabled = false;

            if (NodeData.DestroyEffect != null)
            {
                // Instantiate(NodeData.DestroyEffect, transform.position, Quaternion.identity);
            }

            // Grant XP
            OnResourceDestroyed?.Invoke(this, NodeData.ExperienceReward);

            // Handle Falling/Shattering visual changes
            OnDepleteVisuals();

            // Respawn
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            yield return new WaitForSeconds(NodeData.RespawnTimeSeconds);
            ResetNode();
        }

        // Hooks for specific nodes
        protected abstract void OnDepleteVisuals();
        protected abstract void OnResetVisuals();
    }
}
