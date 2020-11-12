using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Common.Exceptions
{
    public abstract class ClubAppExceptionBase : Exception
    {
        public readonly int StatusCode;

        public ClubAppExceptionBase(int statusCode)
        {
            StatusCode = statusCode;
        }

        public ClubAppExceptionBase(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
