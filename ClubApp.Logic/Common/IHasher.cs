using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Logic.Common
{
    public interface IHasher
    {
        Task<string> HashAsync(string source);

        Task<bool> VerifyAsync(string source, string hash);
    }
}
