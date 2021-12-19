using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Z.BulkOperations;
using Microsoft.Azure.Devices.Common.Exceptions;
using IG.Demonstrative.Context;

namespace IG.Demonstrative.Contexts
{
    internal class SqlServerContext : MainContext
    {
        private readonly IConfiguration configuration;

        public SqlServerContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer(LoadConnectionString(configuration, ProviderType.SqlServer, ConfigPrefix));
        }

        public static string LoadConnectionString(IConfiguration configuration, ProviderType providerType, string sectionName)
        {
            string text = $"{sectionName}_{providerType}";
            string value = configuration[text];
            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationNotFoundException(text);
            }

            return value;
        }
    }
}
