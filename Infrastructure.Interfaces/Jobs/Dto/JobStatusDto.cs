namespace Infrastructure.Interfaces.Jobs.Dto
{
    public enum JobStatusDto
    {
        Success = 0,
        Fail = 1,
        Running = 2,
        Concurrent = 3,
        DateTimeNotInRange = 4
    }
}
