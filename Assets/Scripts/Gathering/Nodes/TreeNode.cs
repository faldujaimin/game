using UnityEngine;

namespace Game.Gathering.Nodes
{
    public class TreeNode : ResourceNode
    {
        [Header("Tree Visuals")]
        public GameObject treeMesh;
        public GameObject stumpMesh;
        public Animator treeAnimator; // For falling animation

        protected override void OnDepleteVisuals()
        {
            if (treeAnimator != null)
            {
                treeAnimator.SetTrigger("Fall");
                // Delay mesh swap until animation finishes if needed
            }
            else
            {
                // Instant swap if no animation
                if (treeMesh != null) treeMesh.SetActive(false);
                if (stumpMesh != null) stumpMesh.SetActive(true);
            }
        }

        protected override void OnResetVisuals()
        {
            if (treeAnimator != null)
            {
                treeAnimator.SetTrigger("Grow"); // Or reset state
            }
            
            if (treeMesh != null) treeMesh.SetActive(true);
            if (stumpMesh != null) stumpMesh.SetActive(false);
        }
    }
}
