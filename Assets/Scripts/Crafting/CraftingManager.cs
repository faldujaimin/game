using UnityEngine;

namespace Game.Crafting
{
    public class CraftingManager : Singleton<CraftingManager>
    {
        public CraftingQueue Queue { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Queue = new CraftingQueue();
        }

        private void Update()
        {
            Queue.Tick(Time.deltaTime);
        }

        public bool TryQueueRecipe(RecipeData recipe, CraftingStationType currentStation = CraftingStationType.Hand, int playerLevel = 1, bool hasKnowledge = true)
        {
            if (recipe == null) return false;

            // 1. Validate Station & Progression
            if (recipe.StationRequired != CraftingStationType.Hand && currentStation != recipe.StationRequired) return false;
            if (playerLevel < recipe.RequiredPlayerLevel) return false;
            if (recipe.RequiresKnowledge && !hasKnowledge) return false;

            // 2. Validate Tool (Would query Equipment events in full implementation)
            // if (!HasRequiredTool(recipe.RequiredTool)) return false;

            // 3. Validate Ingredients (Using decoupled delegates)
            if (CraftingEvents.RequestCheckIngredient != null)
            {
                foreach (var ing in recipe.RequiredIngredients)
                {
                    if (!CraftingEvents.RequestCheckIngredient.Invoke(ing.Item, ing.Amount))
                    {
                        Debug.LogWarning($"Missing ingredient: {ing.Item.ItemName}");
                        return false;
                    }
                }
            }
            else return false; // Fail safe if no inventory system is hooked up

            // 4. Consume Ingredients
            if (CraftingEvents.RequestConsumeIngredient != null)
            {
                foreach (var ing in recipe.RequiredIngredients)
                {
                    CraftingEvents.RequestConsumeIngredient.Invoke(ing.Item, ing.Amount);
                }
            }

            // 5. Queue Job
            CraftingJob newJob = new CraftingJob(recipe);
            Queue.AddJob(newJob);
            return true;
        }

        private void OnEnable()
        {
            CraftingEvents.OnJobCompleted += HandleJobCompleted;
        }

        private void OnDisable()
        {
            CraftingEvents.OnJobCompleted -= HandleJobCompleted;
        }

        private void HandleJobCompleted(CraftingJob job)
        {
            // RNG Success Roll
            float roll = Random.Range(0f, 100f);
            if (roll > job.Recipe.SuccessChance)
            {
                Debug.Log($"Crafting Failed due to chance: {job.Recipe.DisplayName}");
                CraftingEvents.OnJobFailed?.Invoke(job);
                return;
            }

            // Output Item
            CraftingEvents.RequestAddOutput?.Invoke(job.Recipe.OutputItem, job.Recipe.OutputAmount);
            
            // Output XP (Would hook into a PlayerStats system)
            // PlayerStatsEvents.AddXP?.Invoke(job.Recipe.ExperienceReward);
            
            Debug.Log($"Successfully crafted {job.Recipe.OutputAmount}x {job.Recipe.OutputItem.ItemName}");
        }
    }
}
