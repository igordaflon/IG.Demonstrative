using IG.Demonstrative.Models;
using IG.Demonstrative.Services.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace IG.Demonstrative.UI.Pages.Customers
{
    public class EditModel : PageModel
    {

        private readonly ICustomerUpdateService customerUpdateService;

        public EditModel(ICustomerUpdateService customerUpdateService)
        {
            this.customerUpdateService = customerUpdateService;
        }

        [BindProperty]
        public CustomerEditModel Data { get; set; }

        public long Id { get; set; }

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
                return NotFound();

            Id = id;
            Data = await customerUpdateService.GetEditModelAsync(id);
            if (Data == null)
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await customerUpdateService.UpdateAsync(id, Data);
                    return Redirect($"/cliente/{id}");
                }
                catch (ValidationException ex)
                {
                    ErrorMessage = ex.Message;
                }
            }

            Id = id;

            return Page();
        }
    }
}
