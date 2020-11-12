using ClubApp.Api.MiddleWare;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClubApp.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ApiExceptionHandlerMiddleware>();
        }
    }
}
