namespace NetExContexts.Shared.Models.Exceptions
{
    public class InvalidObjectNameException : Exception
    {
        public InvalidObjectNameException(string? message) : base(message)
        {
        }
    }
}
