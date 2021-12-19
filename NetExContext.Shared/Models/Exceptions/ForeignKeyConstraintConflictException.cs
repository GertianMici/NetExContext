namespace NetExContext.Shared.Models.Exceptions
{
    public class ForeignKeyConstraintConflictException : Exception
    {
        public ForeignKeyConstraintConflictException(string? message) : base(message)
        {
        }
    }
}
