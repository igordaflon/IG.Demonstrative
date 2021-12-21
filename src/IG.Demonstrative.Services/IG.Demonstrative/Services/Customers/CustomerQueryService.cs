using IG.Demonstrative.Context;
using IG.Demonstrative.Entities;
using IG.Demonstrative.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IG.Demonstrative.Services.Customers
{
    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly MainContext context;

        public CustomerQueryService(MainContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Retona os dados de um cliente
        /// </summary>
        public async Task<CustomerItem> GetAsync(int id)
        {
            return await context.Customer
                .Where(i => i.Id == id)
                .Select(GetSelectExpression())
                .FirstOrDefaultAsync();
        }


        /// <summary>
        /// Esse método retorna uma ReadOnlyList com todos os clientes ativos
        /// </summary>
        public async Task<IReadOnlyList<CustomerItem>> GetAllAsync()
        {
            return await context.Customer
                .Where(i => !i.IsDeleted)
                .OrderBy(i => i.Name)
                .Select(GetSelectExpression())
                .Take(100)
                .ToListAsync();
        }

        /// <summary>
        /// Esse método retorna uma ReadOnlyList com histórico de alterações de um cliente
        /// </summary>
        public async Task<IReadOnlyList<HistoryItem>> GetHistoryAsync(int id)
        {
            return await context.CustomerHistory
                .Where(i => i.CustomerId == id)
                .OrderByDescending(i => i.Id)
                .Select(i => new HistoryItem
                {
                    Id = i.Id,
                    DateTime = i.DateTime,
                    Description = i.Description
                })
                .Take(100)
                .ToListAsync();
        }



        private static Expression<Func<Customer, CustomerItem>> GetSelectExpression()
        {
            return i => new CustomerItem
            {
                Id = i.Id,
                Name = i.Name,
                IsActive = i.IsActive,
                IsDeleted = i.IsDeleted,
                RegistrationDate = i.RegistrationDate
            };
        }
    }
}
