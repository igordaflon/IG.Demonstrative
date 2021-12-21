using IG.Demonstrative.Models;

namespace IG.Demonstrative.Services.Customers
{
    public interface ICustomerUpdateService
    {
        Task<CustomerEditModel> GetEditModelAsync(int id);
        Task UpdateAsync(int id, CustomerEditModel data);
    }
}