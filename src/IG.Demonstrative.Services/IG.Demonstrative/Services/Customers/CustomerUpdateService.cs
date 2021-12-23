using IG.Demonstrative.Context;
using IG.Demonstrative.Entities;
using IG.Demonstrative.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace IG.Demonstrative.Services.Customers
{
    public class CustomerUpdateService : ICustomerUpdateService
    {

        private readonly MainContext context;

        public CustomerUpdateService(MainContext context)
        {
            this.context = context;
        }

        public async Task<CustomerEditModel> GetEditModelAsync(int id)
        {
            return await context.Customer
                .Where(c => c.Id == id)
                .Select(c => new CustomerEditModel
                {
                    Name = c.Name,
                    IsActive = c.IsActive
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, CustomerEditModel data)
        {
            ValidationHelper.ValidateAnnotations(data);

            if (await context.Customer.AnyAsync(i => i.Id != id && i.Name == data.Name && !i.IsDeleted))
                throw new ValidationException("O nome informado já existe");

            var oldData = await GetEditModelAsync(id);
            if (oldData == null)
                throw new ValidationException("O id informado não existe");

            var description = CreateUpdateDescription(oldData, data);
            if (string.IsNullOrEmpty(description))
                throw new ValidationException("Nenhuma alteração foi feita no registro");

            await context.Customer
                .Where(i => i.Id == id)
                .UpdateAsync(i => new Customer
                {
                    Name = data.Name,
                    IsActive = data.IsActive
                });

            var history = new CustomerHistory
            {
                CustomerId = id,
                Description = description,
                DateTime = DateTime.Now
            };
            context.CustomerHistory.Add(history);

            await context.SaveChangesAsync();
        }

        private static string CreateUpdateDescription(CustomerEditModel oldData, CustomerEditModel newData)
        {
            var list = new List<string>();

            if (oldData.Name != newData.Name)
                list.Add($"Nome alterado de '{oldData.Name}' para '{newData.Name}'");

            if (oldData.IsActive != newData.IsActive)
                list.Add(newData.IsActive ? "Registro ativado" : "Registro inativado");

            return string.Join("\r\n", list);
        }

    }
}
