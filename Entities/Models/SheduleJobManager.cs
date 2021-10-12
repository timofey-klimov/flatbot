using Entities.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Models
{
    public abstract class SheduleJobManager : Entity<int>
    {
        public string JobManagerName { get; set; }

        public DateTime? PlanningRunTime { get; set; }

        public DateTime? RunTime { get; set; }

        public int Period { get; set; }

        public int OrderNumber { get; set; }

        public bool IsActive { get; set; }

        public bool IsRunning { get; set; }

        public ICollection<JobHistory> JobHistories { get; set; }

        public void CreateJobHistory()
        {
            var jobHistory = new JobHistory(null, JobStatus.Running, default);
            JobHistories.Add(jobHistory);
        }

        public void UpdateJobHistoryToSuccess(DateTime finishDate)
        {
            var jobHistory = JobHistories.Last();

            jobHistory.UpdateStateToSuccess(finishDate);
        }

        public void UpdateJobHistoryToFail(DateTime finishDate, string message)
        {
            var jobHistory = JobHistories.Last();

            jobHistory.UpdateStateToFail(finishDate, message);
        }

        public void UpdateJobHistoryToDateTimeNotInRange(DateTime dateTime)
        {
            var jobHistory = JobHistories.Last();
            jobHistory.UpdateStateToDateTimeNotInRange(dateTime);
        }

        public void UpdateJobHistoryToConcurrent(DateTime finishDate)
        {
            PlanningRunTime += TimeSpan.FromMinutes(10);
            var jobHistory = JobHistories.Last();
            jobHistory.UpdateStateToConcurrent(finishDate);
        }
    }
}
