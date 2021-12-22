using FluentResults;
using IG.Demonstrative.Models;

namespace IG.Demonstrative.Services.Customers
{
    public interface ICustomerDeletionService
    {
        Task<Result> CanBeDeletedAsync(int id);
        Task<Result> DeleteAsync(int id);
    }
}