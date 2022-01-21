using Microsoft.EntityFrameworkCore;

namespace NetExContexts.Shared.Services
{
    public interface INetExContextService
    {
        void ThrowCustomException(DbUpdateException dbUpdateException);
    }
}
