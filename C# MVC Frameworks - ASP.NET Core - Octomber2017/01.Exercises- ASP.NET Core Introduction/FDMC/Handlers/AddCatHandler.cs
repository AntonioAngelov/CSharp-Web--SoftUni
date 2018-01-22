namespace FDMC.Handlers
{
    using System;
    using Contracts;
    using Data;
    using Data.Models;
    using Infrastructure;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;

    public class AddCatHandler : IHandler
    {
        public int Order => 2;

        public Func<HttpContext, bool> Condition
            => ctx => ctx.Request.Path.Value == "/cat/add";

        public RequestDelegate RequestHandler =>
            async (context) =>
            {
                if (context.Request.Method == HttpMethod.Get)
                {
                    context.Response.Redirect("/cats-add-form.html");
                }
                else if (context.Request.Method == HttpMethod.Post)
                {
                    var db = context.RequestServices.GetService<FDMCDbContext>();

                    var formData = context.Request.Form;

                    var age = 0;
                    int.TryParse(formData["Age"], out age);

                    var cat = new Cat
                    {
                        Name = formData["Name"],
                        Breed = formData["Breed"],
                        ImageUrl = formData["ImageUrl"],
                        Age = age
                    };

                    db.Add(cat);

                    try
                    {

                        if (string.IsNullOrWhiteSpace(cat.Name)
                            || string.IsNullOrWhiteSpace(cat.Breed)
                            || string.IsNullOrWhiteSpace(cat.ImageUrl))
                        {
                            throw new InvalidOperationException("Invalid cat data.");
                        }

                        await db.SaveChangesAsync();

                        context.Response.Redirect("/");
                    }
                    catch
                    {
                        await context.Response.WriteAsync("<h2>Invalid cat data!</h2>");
                        await context.Response.WriteAsync(@"<a href=""/cat/add"">Back To Form</a>");
                    }

                }
            };
    }
}
