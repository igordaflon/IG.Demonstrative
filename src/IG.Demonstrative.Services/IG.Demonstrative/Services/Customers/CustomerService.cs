using IG.Demonstrative.Context;
using IG.Demonstrative.Entities;
using IG.Demonstrative.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IG.Demonstrative.Services.Customers
{
    public class CustomerService : ICustomerService
    {

        private readonly MainContext context;

        public CustomerService(MainContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Esse método retorna uma ReadOnlyList com todos os clientes ativos
        /// </summary>
        public async Task<IReadOnlyList<CustomerItem>> GetAllAsync()
        {
            return await context.Customer
                .Where(i => !i.IsDeleted)
                .OrderBy(i => i.Name)
                .Select(i => new CustomerItem
                {
                    Id = i.Id,
                    Name = i.Name,
                    IsActive = i.IsActive,
                    IsDeleted = i.IsDeleted,
                    RegistrationDate = i.RegistrationDate
                })
                .Take(100)
                .ToListAsync();
        }

        /// <summary>
        /// Esse método cria um cliente novo a partir do model recebido
        /// </summary>
        public async Task<int> CreateAsync(CustomerEditModel data)
        {
            Helper.ValidateAnnotations(data);

            if (await context.Customer.AnyAsync(i => i.Name == data.Name && !i.IsDeleted))
                throw new ValidationException("O nome informado já existe");

            var message =
$@"Cliente criado
Nome: {data.Name}
Ativo: {(data.IsActive ? "Sim" : "Não")}";

            var customer = new Customer
            {
                Name = data.Name,
                IsActive = data.IsActive,
                RegistrationDate = DateTime.Now,
            };

            var history = new CustomerHistory
            {
                Description = message,
                CustomerId = customer.Id,
                DateTime = DateTime.Now,
            };

            context.Customer.Add(customer);
            context.CustomerHistory.Add(history);

            await context.SaveChangesAsync();

            return customer.Id;
        }
    }
}
