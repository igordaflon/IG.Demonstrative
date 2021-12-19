using IG.Demonstrative.Models;

namespace IG.Demonstrative.Services.Customers
{
    public interface ICustomerService
    {
        Task<int> CreateAsync(CustomerEditModel data);
        Task<IReadOnlyList<CustomerItem>> GetAllAsync();
    }
}