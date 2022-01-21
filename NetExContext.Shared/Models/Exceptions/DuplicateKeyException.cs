namespace NetExContexts.Shared.Models.Exceptions
{
    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(string message) : base(message) { }
    }
}
