using Npgsql;

namespace NetExContext.Shared.Brokers
{
    public interface IDbErrorBroker
    {
        int GetDbErrorCode(PostgresException exception);
    }
}
