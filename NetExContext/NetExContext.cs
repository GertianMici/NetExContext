using Microsoft.EntityFrameworkCore;
using NetExContexts.Shared.Brokers;
using NetExContexts.Shared.Services;

namespace NetExContexts
{
    public class NetExContext : DbContext
    {
        private INetExContextService netExContextService;
        private IDbErrorBroker dbErrorBroker;
        protected NetExContext() =>
            InitializeServices();
        public NetExContext(DbContextOptions options) : base(options) =>
            InitializeServices();

        private void InitializeServices()
        {
            dbErrorBroker = new DbErrorBroker();
            netExContextService = new NetExContextService(dbErrorBroker);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException exception)
            {
                netExContextService.ThrowCustomException(exception);
                throw;
            }
        }
        public override async Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (DbUpdateException exception)
            {
                netExContextService.ThrowCustomException(exception);
                throw;
            }
        }
        public override int SaveChanges()
        {
            try
            {

                return base.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                netExContextService.ThrowCustomException(exception);
                throw;
            }
        }
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            try
            {

                return base.SaveChanges(acceptAllChangesOnSuccess);
            }
            catch (DbUpdateException exception)
            {
                netExContextService.ThrowCustomException(exception);
                throw;
            }
        }
    }
}
