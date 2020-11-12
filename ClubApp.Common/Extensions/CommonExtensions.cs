using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClubApp.Common.Extensions
{
    public static class CommonExtensions
    {
        public static string GetMD5Hash(this string source)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrWhiteSpace(source))
            {
                return null;
            }

            StringBuilder str = new StringBuilder();

            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(source));

                for (int i = 0; i < hash.Length; i++)
                {
                    str.Append(hash[i].ToString("x2"));
                }
            }

            return str.ToString();
        }

        public static string ToCommonDateTimeString(this DateTime source)
        {
            return source.ToString(Consts.COMMON_DATETIME_PATTERN);
        }

        public static string ToFileDateTimeString(this DateTime source)
        {
            return source.ToString(Consts.FILE_DATETIME_PATTERN);
        }

        public static bool IsSuccessStatusCode(this HttpStatusCode statusCode)
        {
            return ((int)statusCode >= 200) && ((int)statusCode <= 299);
        }
    }
}
