namespace FDMC
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Data;
    using Infrastructure.Extension;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FDMCDbContext>(options =>
                options.UseSqlServer("Server=.;Database=FDMC;Integrated Security=true;"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
         =>
            app.UseDatabaseMigration()
            .UseStaticFiles()
            .UseHtmlContentType()
            .UseRequestHandlers()
            .UseNotFoundHandler();
    }
}
