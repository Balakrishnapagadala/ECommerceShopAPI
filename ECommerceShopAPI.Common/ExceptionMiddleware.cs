using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceShopAPI.Common
{
        /// <summary>
        /// Middleware for exception handling
        /// </summary>
        public class ExceptionMiddleware
        {
            private readonly ILogger<ExceptionMiddleware> _logger;
            private readonly RequestDelegate _next;

            public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
            {
                _logger = logger;
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);

                    await HandleExceptionAsync(context, ex);
                }
            }

            private async Task HandleExceptionAsync(HttpContext context, Exception ex)
            {
                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new CustomResponse(context.Response.StatusCode, ex.Message, "Internal Server Error");
                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
}
