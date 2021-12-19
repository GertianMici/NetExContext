using Microsoft.EntityFrameworkCore;

namespace NetExContext.Shared.Services
{
    public interface INetExContextService
    {
        void ThrowCustomException(DbUpdateException dbUpdateException);
    }
}
