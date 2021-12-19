using IG.Demonstrative.Entities;
using Microsoft.EntityFrameworkCore;

namespace IG.Demonstrative.Context
{
    public class MainContext : DbContext
    {

        public const string ConfigPrefix = "Demonstrative";

        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerHistory> CustomerHistory { get; set; }
    }
}
