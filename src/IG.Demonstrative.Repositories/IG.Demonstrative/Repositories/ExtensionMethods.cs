using IG.Demonstrative.Context;
using IG.Demonstrative.Contexts;
using Microsoft.Azure.Devices.Common.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl.AdoJobStore;
using Z.BulkOperations;

namespace IG.Demonstrative.Repositories
{
    public static class ExtensionMethods
    {

        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            switch (configuration.LoadProvider(MainContext.ConfigPrefix))
            {
                case ProviderType.SqlServer:
                    services.AddDbContext<MainContext, SqlServerContext>();
                    break;
            }

            services.AddDbContext<SqlServerContext>();
        }


        public static ProviderType LoadProvider(this IConfiguration configuration, string sectionName)
        {
            string text = sectionName + "_Provider";
            string value = configuration[text];
            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationNotFoundException(text);
            }

            if (!Enum.TryParse(value, out ProviderType result))
            {
                throw new InvalidConfigurationException(text);
            }

            return result;
        }

    }
}
