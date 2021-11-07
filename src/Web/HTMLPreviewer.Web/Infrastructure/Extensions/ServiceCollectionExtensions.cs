namespace HTMLPreviewer.Web.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using HTMLPreviewer.Data;
    using HTMLPreviewer.Services.Data;
    using HTMLPreviewer.Services.Data.Interfaces;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAntiforgerySecurity(
            this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            return services;
        }

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
               .AddDbContext<HTMLPreviewerDbContext>(
                   options =>
                   {
                       options.UseSqlServer(configuration.GetDefaultConnectionString());
                   });

            return services;
        }

        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            services
               .AddTransient<IHTMLExampleService, HTMLExampleService>();

            return services;
        }
    }
}
