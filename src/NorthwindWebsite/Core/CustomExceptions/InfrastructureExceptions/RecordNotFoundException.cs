namespace NorthwindWebsite.Core.CustomExceptions.InfrastructureExceptions;

public class RecordNotFoundException : Exception
{
    public RecordNotFoundException(string message)
        : base(message)
    {
    }
}
