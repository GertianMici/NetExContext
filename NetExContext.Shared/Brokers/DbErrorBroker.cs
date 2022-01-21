using Npgsql;

namespace NetExContexts.Shared.Brokers
{
    public class DbErrorBroker : IDbErrorBroker
    {
        public int GetDbErrorCode(PostgresException exception) => exception.ErrorCode;
    }
}
