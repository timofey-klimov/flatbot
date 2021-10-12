using Infrastructure.Interfaces.Jobs.Dto;

namespace WepApp.JobManagers.Dto
{
    public class CanExecuteResult
    {
        public bool CanExecute { get; }

        public JobStatusDto? Status { get; }

        public CanExecuteResult(bool canExexute, JobStatusDto? status)
        {
            CanExecute = canExexute;
            Status = status;
        }

        public static CanExecuteResult JobCanExecute() => new CanExecuteResult(true, null);

        public static CanExecuteResult JobCannotExecute(JobStatusDto status) => new CanExecuteResult(false, status);
    }
}
