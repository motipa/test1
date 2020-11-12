using ClubApp.Api.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubApp.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static async Task<string> GetRequestBodyAsStringAsync(this HttpContext context)
        {
           // context.Request.EnableRewind();
            context.Request.Body.Position = 0;

            using (StreamReader reader = new StreamReader(context.Request.Body, Encoding.UTF8))
            {
                var bodyValue = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                return bodyValue;
            }
        }

        public static async Task<HttpRequestDetails> GetRequestDetailsAsync(this HttpContext context)
        {
            var httpDetails = new HttpRequestDetails
            {
                Controller = context.GetRouteValue("controller").ToString(),
                Action = context.GetRouteValue("action").ToString(),
                Route = context.Request.Path.Value,
                Method = context.Request.Method,
                QueryString = context.Request.QueryString.ToString(),
                Body = await context.GetRequestBodyAsStringAsync()
            };

            return httpDetails;
        }

        public static void SetContentDispositionHeader(this HttpResponse response, string fileName)
        {
            response.Headers.Add("Content-Disposition", $"attachment; filename={fileName}");
        }
    }
}
