using System;
namespace Game.Crafting
{
    public enum JobState
    {
        Pending,
        InProgress,
        Paused,
        Completed,
        Failed,
        Cancelled
    }

    [Serializable]
    public class CraftingJob
    {
        public string JobID;
        public RecipeData Recipe;
        public float TimeRemaining;
        public JobState State;

        public CraftingJob(RecipeData recipe)
        {
            JobID = Guid.NewGuid().ToString();
            Recipe = recipe;
            TimeRemaining = recipe.CraftTimeSeconds;
            State = JobState.Pending;
        }
    }
}
