
# NetExContext

This library is a wrapper for EntityFramework dbContext with added values as exception for postgreSQL error codes.
<br>

<br>

## Integration
Replace your existing ```DbContext``` class with ```NetExContext``` (or your `IdentityDbContext` with `NetExContext.NetExIdentityContext`) as follows:

#### Before:
 
```csharp
    public partial class StorageBroker : DbContext, IStorageBroker
    {
        public StorageBroker(DbContextOptions<StorageBroker> options)
            : base(options) => this.Database.Migrate();
    }

```

#### After:
```csharp
    public partial class StorageBroker : NetExContext, IStorageBroker
    {
        public StorageBroker(DbContextOptions<StorageBroker> options)
            : base(options) => this.Database.Migrate();
    }

```

<br>