using Npgsql;

namespace NetExContexts.Shared.Brokers
{
    public interface IDbErrorBroker
    {
        int GetDbErrorCode(PostgresException exception);
    }
}
