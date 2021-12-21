using IG.Demonstrative.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IG.Demonstrative.Context;
using Microsoft.EntityFrameworkCore;
using IG.Demonstrative.Services.Customers;

namespace IG.Demonstrative.Services
{
    public static class ExtensionMethods
    {
        public static void AddDemonstrativeServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories(configuration);
            services.AddScoped<ICustomerQueryService, CustomerQueryService>();
            services.AddScoped<ICustomerCreationService, CustomerCreationService>();
            services.AddScoped<ICustomerUpdateService, CustomerUpdateService>();
            services.AddScoped<ICustomerDeletionService, CustomerDeletionService>();
        }

        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();

            var db = scope.ServiceProvider.GetService<MainContext>();
            db.Database.Migrate();
            db.SaveChanges();

            return host;
        }
    }
}
