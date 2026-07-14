using UnityEngine;

namespace Game.Gathering.Nodes
{
    public class RockNode : ResourceNode
    {
        [Header("Rock Visuals")]
        public GameObject intactMesh;
        public GameObject shatteredMesh; // Debris

        protected override void OnDepleteVisuals()
        {
            if (intactMesh != null) intactMesh.SetActive(false);
            if (shatteredMesh != null) shatteredMesh.SetActive(true);
        }

        protected override void OnResetVisuals()
        {
            if (intactMesh != null) intactMesh.SetActive(true);
            if (shatteredMesh != null) shatteredMesh.SetActive(false);
        }
    }
}
