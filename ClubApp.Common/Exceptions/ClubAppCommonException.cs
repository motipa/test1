using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Common.Exceptions
{
    public class ClubAppCommonException :ClubAppExceptionBase
    {
        public ClubAppCommonException() : base(StatusCodes.Status500InternalServerError) { }

        public ClubAppCommonException(string message) : base(StatusCodes.Status500InternalServerError, message) { }
    }
}
