namespace Infrastructure.Interfaces.Jobs.Dto
{
    public enum JobStatusDto
    {
        Success,
        Fail,
        Running,
        Concurrent,
        DateTimeNotInRange
    }
}
