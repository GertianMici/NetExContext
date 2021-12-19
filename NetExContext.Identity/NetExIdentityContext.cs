using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetExContext.Shared.Brokers;
using NetExContext.Shared.Services;

namespace NetExContext.Identity
{
    public class NetExIdentityContext<TUser> : IdentityDbContext<TUser, IdentityRole, string> where TUser : IdentityUser
    {
        protected NetExIdentityContext() { }

        public NetExIdentityContext(DbContextOptions options) : base(options) { }
    }

    public class NetExIdentityContext<TUser, TRole, TKey> : NetExIdentityContext<TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        protected NetExIdentityContext() { }

        public NetExIdentityContext(DbContextOptions options) : base(options) { }
    }
    public class NetExIdentityContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken> : IdentityDbContext<TUser, TRole, TKey, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
    where TUserClaim : IdentityUserClaim<TKey>
    where TUserRole : IdentityUserRole<TKey>
    where TUserLogin : IdentityUserLogin<TKey>
    where TRoleClaim : IdentityRoleClaim<TKey>
    where TUserToken : IdentityUserToken<TKey>
    {
        private INetExContextService netExContextService;
        private IDbErrorBroker dbErrorBroker;

        protected NetExIdentityContext() =>
            InitializeServices();

        public NetExIdentityContext(DbContextOptions options) : base(options) =>
            InitializeServices();

        private void InitializeServices()
        {
            this.dbErrorBroker = new DbErrorBroker();
            this.netExContextService = new NetExContextService(dbErrorBroker);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException dbUpdateException)
            {
                netExContextService.ThrowCustomException(dbUpdateException);

                throw;
            }
        }
        public async override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
            }
            catch (DbUpdateException dbUpdateException)
            {
                netExContextService.ThrowCustomException(dbUpdateException);

                throw;
            }
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateException dbUpdateException)
            {
                netExContextService.ThrowCustomException(dbUpdateException);

                throw;
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            try
            {
                return base.SaveChanges(acceptAllChangesOnSuccess);
            }
            catch (DbUpdateException dbUpdateException)
            {
                netExContextService.ThrowCustomException(dbUpdateException);

                throw;
            }
        }
    }

}
