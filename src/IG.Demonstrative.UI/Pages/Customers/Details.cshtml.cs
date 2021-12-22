using IG.Demonstrative.Models;
using IG.Demonstrative.Services.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IG.Demonstrative.UI.Pages.Customers
{
    public class DetailsModel : PageModel
    {

        private readonly ICustomerQueryService customerQueryService;

        public DetailsModel(ICustomerQueryService customerQueryService)
        {
            this.customerQueryService = customerQueryService;
        }

        public CustomerItem Data { get; set; }
        public IReadOnlyList<HistoryItem> History { get; set; }

        public async Task<ActionResult> OnGetAsync(int id)
        {
            Data = await customerQueryService.GetAsync(id);
            if (Data == null)
                return NotFound();

            History = await customerQueryService.GetHistoryAsync(id);

            return Page();
        }
    }
}
