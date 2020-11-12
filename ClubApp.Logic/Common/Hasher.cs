using ClubApp.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ClubApp.Logic.Common
{
    public class Hasher : IHasher
    {
        private const int ALGORITHM_ITERATIONS = 10000;
        private const int SALT_SIZE = 16;
        private const int HASH_SIZE = 32;

        public Task<string> HashAsync(string source)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var algorithm = new Rfc2898DeriveBytes(source, SALT_SIZE, ALGORITHM_ITERATIONS, HashAlgorithmName.SHA512))
                {
                    var hash = Convert.ToBase64String(algorithm.GetBytes(HASH_SIZE));
                    var salt = Convert.ToBase64String(algorithm.Salt);

                    return $"{salt}.{hash}";
                }
            });
        }

        public Task<bool> VerifyAsync(string source, string hashedValue)
        {
            return Task.Factory.StartNew(() =>
            {
                var parts = hashedValue.Split('.', 2);

                if (parts.Length != 2)
                {
                    throw new ClubAppCommonException("Unexpected hash format");
                }

                var salt = Convert.FromBase64String(parts[0]);
                var hash = Convert.FromBase64String(parts[1]);

                using (var algorithm = new Rfc2898DeriveBytes(source, salt, ALGORITHM_ITERATIONS, HashAlgorithmName.SHA512))
                {
                    var hashToCheck = algorithm.GetBytes(HASH_SIZE);

                    return hashToCheck.SequenceEqual(hash);
                }
            });
        }
    }
}
