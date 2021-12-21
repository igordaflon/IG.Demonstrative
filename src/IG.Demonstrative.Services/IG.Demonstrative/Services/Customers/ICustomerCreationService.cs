using IG.Demonstrative.Models;

namespace IG.Demonstrative.Services.Customers
{
    public interface ICustomerCreationService
    {
        Task<int> CreateAsync(CustomerEditModel data);
    }
}