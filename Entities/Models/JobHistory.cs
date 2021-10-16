using Entities.Enums;
using System;

namespace Entities.Models
{
    public class JobHistory : Entity<int>
    {
        public int SheduleJobManagerId { get; private set; }

        public DateTime? FinishTime { get; private set; }

        public JobStatus Status { get; private set; }

        public string Message { get; private set; }

        private JobHistory() { }

        public JobHistory(DateTime? finishDate, JobStatus status, string message)
        {
            FinishTime = finishDate;
            Status = status;
            Message = message;
        }

        public void UpdateStateToSuccess(DateTime date)
        {
            FinishTime = date;
            Status = JobStatus.Success;
            Message = "Success";
        }

        public void UpdateStateToFail(DateTime date, string message)
        {
            FinishTime = date;
            Status = JobStatus.Fail;
            Message = message;
        }

        public void UpdateStateToDateTimeNotInRange(DateTime date)
        {
            FinishTime = date;
            Status = JobStatus.DateTimeNotInRange;
        }

        public void UpdateStateToConcurrent(DateTime finishDate)
        {
            FinishTime = finishDate;
            Status = JobStatus.Concurrent;
        }
    }
}
