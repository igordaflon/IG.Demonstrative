using IG.Demonstrative.Models;

namespace IG.Demonstrative.Services.Customers
{
    public interface ICustomerQueryService
    {
        Task<IReadOnlyList<CustomerItem>> GetAllAsync();
        Task<CustomerItem> GetAsync(int id);
        Task<IReadOnlyList<HistoryItem>> GetHistoryAsync(int id);
    }
}