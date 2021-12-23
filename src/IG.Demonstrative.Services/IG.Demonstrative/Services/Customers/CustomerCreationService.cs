using IG.Demonstrative.Context;
using IG.Demonstrative.Entities;
using IG.Demonstrative.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IG.Demonstrative.Services.Customers
{
    public class CustomerCreationService : ICustomerCreationService
    {
        private readonly MainContext context;

        public CustomerCreationService(MainContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Esse método cria um cliente novo a partir do model recebido
        /// </summary>
        public async Task<int> CreateAsync(CustomerEditModel data)
        {
            if(data == null)
                throw new ArgumentNullException(nameof(data));

            ValidationHelper.ValidateAnnotations(data);

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
            context.Customer.Add(customer);
            await context.SaveChangesAsync();

            var history = new CustomerHistory
            {
                Description = message,
                CustomerId = customer.Id,
                DateTime = DateTime.Now,
            };
            
            context.CustomerHistory.Add(history);
            await context.SaveChangesAsync();

            return customer.Id;
        }
    }
}
