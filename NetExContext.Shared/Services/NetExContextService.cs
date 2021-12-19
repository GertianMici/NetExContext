using Microsoft.EntityFrameworkCore;
using NetExContext.Shared.Brokers;
using Npgsql;

namespace NetExContext.Shared.Services
{
    public partial class NetExContextService : INetExContextService
    {
        private readonly IDbErrorBroker dbErrorBroker;

        public NetExContextService(IDbErrorBroker dbErrorBroker)
            => this.dbErrorBroker = dbErrorBroker;
        

        public void ThrowCustomException(DbUpdateException dbUpdateException)
        {
            ValidateInnerException(dbUpdateException);
            PostgresException npgsqlException = GetNpgsqlException(dbUpdateException.InnerException);
            int errorCode = dbErrorBroker.GetDbErrorCode(npgsqlException);
            ConvertAndThrowCustomException(errorCode, npgsqlException.Message);

            throw dbUpdateException;
        }

        private static PostgresException GetNpgsqlException(Exception exception) 
            => (PostgresException)exception;  
    }
}
