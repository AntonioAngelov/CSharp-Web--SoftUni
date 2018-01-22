namespace FDMC.Handlers
{
    using System;
    using Contracts;
    using Data;
    using Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class CatDetailsHandler : IHandler
    {
        public int Order => 3;

        public Func<HttpContext, bool> Condition =>
            ctx => ctx.Request.Path.Value.StartsWith("/cat")
                   && ctx.Request.Method == HttpMethod.Get;

        public RequestDelegate RequestHandler =>
            async (context) =>
            {
                var urlParameters = context
                    .Request
                    .Path
                    .Value
                    .Split('/', StringSplitOptions.RemoveEmptyEntries);

                if (urlParameters.Length < 2)
                {
                    context.Response.Redirect("/");
                    return;
                }

                var catId = 0;
                int.TryParse(urlParameters[1], out catId);

                var db = context.RequestServices.GetRequiredService<FDMCDbContext>();

                using (db)
                {
                    var cat = await db.Cats.FindAsync(catId);

                    if (cat == null)
                    {
                        context.Response.Redirect("/");
                        return;
                    }

                    await context.Response.WriteAsync($"<h1>{cat.Name}</h1>");
                    await context.Response.WriteAsync(
                        $@"<img src=""{cat.ImageUrl}"" alt=""{cat.Name}"" width=""300""/>");
                    await context.Response.WriteAsync($"<p><strong>Age: {cat.Age}</strong></p>");
                    await context.Response.WriteAsync($"<p><strong>Breed: {cat.Breed}</strong></p>");
                }

            };
    }
}
