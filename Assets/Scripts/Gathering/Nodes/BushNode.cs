using UnityEngine;

namespace Game.Gathering.Nodes
{
    public class BushNode : ResourceNode
    {
        [Header("Bush Visuals")]
        public GameObject fullBushMesh;
        public GameObject emptyBushMesh;

        protected override void OnDepleteVisuals()
        {
            if (fullBushMesh != null) fullBushMesh.SetActive(false);
            if (emptyBushMesh != null) emptyBushMesh.SetActive(true);
        }

        protected override void OnResetVisuals()
        {
            if (fullBushMesh != null) fullBushMesh.SetActive(true);
            if (emptyBushMesh != null) emptyBushMesh.SetActive(false);
        }
    }
}
