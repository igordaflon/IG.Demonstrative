using IG.Demonstrative.Models;
using IG.Demonstrative.Services.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IG.Demonstrative.UI.Pages.Customers
{
    public class DeleteModel : PageModel
    {
        private readonly ICustomerQueryService queryService;
        private readonly ICustomerDeletionService deletionService;

        public DeleteModel(ICustomerQueryService queryService, ICustomerDeletionService deletionService)
        {
            this.queryService = queryService;
            this.deletionService = deletionService;
        }

        public CustomerItem Data { get; set; }

        public string ErrorMessage { get; set; }

        public string CannotProceed { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0 || !await LoadAsync(id))
                return NotFound();

            return Page();
        }

        private async Task<bool> LoadAsync(int id)
        {
            Data = await queryService.GetAsync(id);

            if (Data == null)
                return false;

            var result = await deletionService.CanBeDeletedAsync(id);
            CannotProceed = string.Join("\r\n", result.Errors.Select(i => i.Message));

            return true;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id <= 0)
                return NotFound();

            if (ModelState.IsValid)
            {
                var result = await deletionService.DeleteAsync(id);
                if (result.IsSuccess)
                    return Redirect("/clientes");

                ErrorMessage = string.Join("\r\n", result.Errors.Select(i => i.Message));
            }

            if (!await LoadAsync(id))
                return NotFound();

            return Page();
        }
    }
}
