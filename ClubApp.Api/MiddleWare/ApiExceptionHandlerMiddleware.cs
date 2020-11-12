using ClubApp.Api.Extensions;
using ClubApp.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubApp.Api.MiddleWare
{
    public class ApiExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ClubAppExceptionBase iex)
            {
                await HandleIndagoExceptionAsync(context, iex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleIndagoExceptionAsync(HttpContext context, ClubAppExceptionBase iex)
        {
            context.Response.StatusCode = iex.StatusCode;
            context.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Status = iex.StatusCode,
                Detail = iex.Message,
                Type = $"https://httpstatuses.com/{iex.StatusCode}"
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails), Encoding.UTF8);
            await LogExceptionAsync(context, iex, iex.StatusCode);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var message = "Application error occured";

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Detail = message,
                Type = $"https://httpstatuses.com/{statusCode}"
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails), Encoding.UTF8);
            await LogExceptionAsync(context, ex, statusCode, message);
        }

        private async Task LogExceptionAsync(HttpContext context, Exception ex, int responseStatusCode, string responsePhrase = null)
        {
            var httpRequestDetails = await context.GetRequestDetailsAsync();

            StringBuilder format = new StringBuilder();
            format.AppendLine("Exception Type: {exType}");
            format.AppendLine("Exception Message: {exMessage}");
            format.AppendLine("Inner Exception Message: {innerExMessage}");
            format.AppendLine("Request: {@httpRequest}");
            format.AppendLine("Response Status Code: {httpResponseStatusCode}");
            format.AppendLine("Response Phrase: {httpResponsePhrase}");
            format.AppendLine("Trace: {exTrace}");

            Log.Error(format.ToString(), ex.GetType().FullName, ex.Message, ex.InnerException?.Message, httpRequestDetails, responseStatusCode, responsePhrase, ex.StackTrace);
        }
    }
}
