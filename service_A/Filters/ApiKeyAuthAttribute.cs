﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace service_A.Filters
{
    /// <summary>
    ///  Класс ApiKeyAuthAttribute
    ///  ограничивает доступ по секретному api ключу
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            if(!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey)){
                context.Result = new UnauthorizedResult();
                return;
            }
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>(ApiKeyHeaderName);
            if (!potentialApiKey.ToString().Contains(apiKey)) {
                context.Result = new UnauthorizedResult();
                return;
            }
            await next();
        }
    }
}
