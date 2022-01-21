using Microsoft.EntityFrameworkCore;

namespace NetExContexts.Shared.Services
{
    public partial class NetExContextService
    {
        private static void ValidateInnerException(DbUpdateException dbUpdateException)
        {
            if (dbUpdateException.InnerException == null)
            {
                throw dbUpdateException;
            }
        }
    }
}
