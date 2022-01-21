using NetExContexts.Shared.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetExContexts.Shared.Services
{
    public partial class NetExContextService
    {
        private static void ConvertAndThrowCustomException(int code,string message)
        {
            switch (code)
            {
                case 207:
                    throw new InvalidColumnNameException(message);
                case 208: 
                    throw new InvalidObjectNameException(message);
                case 547:
                    throw new ForeignKeyConstraintConflictException(message);
                case 2601:
                    throw new DuplicateKeyWithUniqueIndexException(message);
                case 2627:
                    throw new DuplicateKeyException(message);
            }
        }
    }
}
