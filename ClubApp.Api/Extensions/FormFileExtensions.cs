using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Api.Extensions
{
    public static class FormFileExtensions
    {
        public static async Task<string> ReadAsStringAsync(this IFormFile file)
        {
            try
            {
                string fileContent = string.Empty;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    fileContent = await reader.ReadToEndAsync();
                }

                return fileContent;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
