using System.Collections.Generic;
using UnityEngine;

namespace Game.Gathering
{
    [CreateAssetMenu(fileName = "New Resource Data", menuName = "Gathering/Resource Node Data")]
    public class ResourceNodeData : ScriptableObject
    {
        [Header("Identity")]
        public string ResourceID = System.Guid.NewGuid().ToString();
        public string DisplayName = "Resource";

        [Header("Health & Gathering")]
        public int MaxHealth = 100;
        public ToolType RequiredTool = ToolType.Hand;
        public float RespawnTimeSeconds = 60f;

        [Header("Loot")]
        public List<LootDrop> LootTable = new List<LootDrop>();
        public int ExperienceReward = 10;

        [Header("Visuals & Audio")]
        public AudioClip HitSound;
        public GameObject DestroyEffect;
        public string InteractionAnimationTrigger = "Gather";
    }
}
