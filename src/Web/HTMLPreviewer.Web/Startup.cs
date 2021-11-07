namespace HTMLPreviewer.Web
{
    using System.Reflection;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;

    using HTMLPreviewer.Data;
    using HTMLPreviewer.Web.Infrastructure.Extensions;
    using HTMLPreviewer.Services.Mapping;
    using HTMLPreviewer.Services.Data.Models;

    using AspNetCoreHero.ToastNotification;
    using AspNetCoreHero.ToastNotification.Extensions;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDatabase(this.Configuration)
                .AddApplicationServices()
                .AddAntiforgerySecurity()
                .AddControllersWithViews();

            services
                .AddNotyf(config =>
                {
                    config.DurationInSeconds = 3;
                    config.IsDismissable = false;
                    config.Position = NotyfPosition.TopRight; 
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ModelValidation).GetTypeInfo().Assembly);

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider
                    .GetRequiredService<HTMLPreviewerDbContext>();

                dbContext.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app
                    .UseExceptionHandler("/Home/Error")
                    .UseHsts();
            }

            app
                .UseNotyf()
                .UseStatusCodePagesWithRedirects("/Home/NotFoundPage?statusCode={0}")
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}
