using FluentResults;
using IG.Demonstrative.Context;
using IG.Demonstrative.Entities;
using IG.Demonstrative.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace IG.Demonstrative.Services.Customers
{
    public class CustomerDeletionService : ICustomerDeletionService
    {
        private readonly MainContext context;

        public CustomerDeletionService(MainContext context)
        {
            this.context = context;
        }

        public async Task<Result> CanBeDeletedAsync(int id)
        {
            if (id <= 0)
                return Result.Fail("ID inválido");

            var isDeleted = await context.Customer
                .Where(c => c.Id == id)
                .Select(c => (bool?)c.IsDeleted)
                .FirstOrDefaultAsync();

            if (!isDeleted.HasValue)
                return Result.Fail($"Cliente de ID {id} não encontrado");

            if (isDeleted.Value)
                return Result.Fail($"O registro já está excluído");

            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(int id, ReasonModel reason)
        {
            var check = await CanBeDeletedAsync(id);
            if (check.IsFailed)
                return check;

            var description = string.IsNullOrEmpty(reason?.Description) ? "Registro excluído" : $"Registro excluído: {reason}";

            await context.Customer
                .Where(i => i.Id == id)
                .UpdateAsync(i => new Customer
                {
                    IsDeleted = true
                });

            var history = new CustomerHistory
            {
                CustomerId = id,
                Description = description,
                DateTime = DateTime.Now
            };

            context.CustomerHistory.Add(history);

            await context.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
