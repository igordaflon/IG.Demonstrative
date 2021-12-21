using IG.Demonstrative.Models;
using IG.Demonstrative.Services.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IG.Demonstrative.UI.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly ICustomerQueryService service;

        public IndexModel(ICustomerQueryService service)
        {
            this.service = service;
        }

        public IReadOnlyList<CustomerItem> Items { get; internal set; }

        public string Message { get; internal set; }

        public async Task OnGetAsync()
        {
            Items = await service.GetAllAsync();

            Message = Items.Count switch
            {
                0 => "Nenhum registro encontrado.",
                100 => "Apenas os 100 primeiros registros estão sendo exibidos.",
                _ => string.Empty
            };
        }
    }
}
