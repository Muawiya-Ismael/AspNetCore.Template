﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using MvcTemplate.Resources;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MvcTemplate.Components.Mvc
{
    public class ErrorPagesMiddleware
    {
        private ILogger Logger { get; }
        private LinkGenerator Link { get; }
        private RequestDelegate Next { get; }

        public ErrorPagesMiddleware(RequestDelegate next, LinkGenerator link, ILogger<ErrorPagesMiddleware> logger)
        {
            Link = link;
            Next = next;
            Logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "An unhandled exception has occurred while executing the request.");

                if (context.Request.Headers != null && context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json; charset=utf-8";

                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new { status = "error", data = new { message = Resource.ForString("SystemError") } }));
                }
                else
                {
                    context.Response.Redirect(Link.GetPathByAction(context, "Error", "Home", new { area = "" }));
                }
            }
        }
    }
}
