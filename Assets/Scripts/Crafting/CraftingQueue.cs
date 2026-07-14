using System.Collections.Generic;

namespace Game.Crafting
{
    public class CraftingQueue
    {
        public List<CraftingJob> Jobs { get; private set; } = new List<CraftingJob>();
        public CraftingJob CurrentJob { get; private set; }
        public bool IsPaused { get; private set; }

        public void AddJob(CraftingJob job)
        {
            Jobs.Add(job);
            if (CurrentJob == null && !IsPaused)
            {
                ProcessNextJob();
            }
        }

        public void CancelJob(string jobID)
        {
            var job = Jobs.Find(j => j.JobID == jobID);
            if (job != null)
            {
                job.State = JobState.Cancelled;
                Jobs.Remove(job);
                CraftingEvents.OnJobCancelled?.Invoke(job); // UI and refunds can listen to this
            }
            
            if (CurrentJob != null && CurrentJob.JobID == jobID)
            {
                CurrentJob = null;
                ProcessNextJob();
            }
        }

        public void Pause()
        {
            IsPaused = true;
            if (CurrentJob != null && CurrentJob.State == JobState.InProgress)
            {
                CurrentJob.State = JobState.Paused;
            }
        }

        public void Resume()
        {
            IsPaused = false;
            if (CurrentJob != null && CurrentJob.State == JobState.Paused)
            {
                CurrentJob.State = JobState.InProgress;
            }
            else if (CurrentJob == null)
            {
                ProcessNextJob();
            }
        }

        public void Tick(float deltaTime)
        {
            if (IsPaused || CurrentJob == null) return;

            if (CurrentJob.State == JobState.InProgress)
            {
                CurrentJob.TimeRemaining -= deltaTime;

                if (CurrentJob.TimeRemaining <= 0)
                {
                    CompleteCurrentJob();
                }
            }
        }

        private void ProcessNextJob()
        {
            if (Jobs.Count == 0) return;

            CurrentJob = Jobs[0];
            Jobs.RemoveAt(0);
            
            CurrentJob.State = JobState.InProgress;
            CraftingEvents.OnJobStarted?.Invoke(CurrentJob);
        }

        private void CompleteCurrentJob()
        {
            CurrentJob.State = JobState.Completed;
            var jobToComplete = CurrentJob;
            CurrentJob = null;
            
            CraftingEvents.OnJobCompleted?.Invoke(jobToComplete);
            ProcessNextJob();
        }
    }
}
