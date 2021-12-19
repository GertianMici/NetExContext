using Npgsql;

namespace NetExContext.Shared.Brokers
{
    public class DbErrorBroker : IDbErrorBroker
    {
        public int GetDbErrorCode(PostgresException exception) => exception.ErrorCode;
    }
}
