﻿namespace FDMC.Infrastructure.Extension
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Handlers.Contracts;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Middleware;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(
            this IApplicationBuilder builder)
            => builder.UseMiddleware<DatabaseMigrationMiddleware>();
        
        public static IApplicationBuilder UseHtmlContentType(
            this IApplicationBuilder builder)
            => builder.UseMiddleware<HtmlContentTypeMiddleware>();


        public static IApplicationBuilder UseRequestHandlers(
            this IApplicationBuilder builder)
        {
            var handlers = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && typeof(IHandler).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHandler>()
                .OrderBy(h => h.Order);

            foreach (var handler in handlers)
            {
                builder.MapWhen(handler.Condition,
                    app => app.Run(handler.RequestHandler));
            }

            return builder;
        }

        public static void UseNotFoundHandler(
            this IApplicationBuilder builder)
        {
            builder.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("404 Page Was Not Found :/");
            });
        }
    }

}
