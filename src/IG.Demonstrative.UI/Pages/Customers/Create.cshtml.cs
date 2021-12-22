using IG.Demonstrative.Models;
using IG.Demonstrative.Services.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace IG.Demonstrative.UI.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerCreationService service;

        public CreateModel(ICustomerCreationService service)
        {
            this.service = service;
        }

        [BindProperty]
        public CustomerEditModel Data { get; set; } = new CustomerEditModel();

        public string ErrorMessage { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = await service.CreateAsync(Data);
                    return Redirect($"/cliente/{id}");
                }
                catch (ValidationException ex)
                {
                    ErrorMessage = ex.Message;
                }
            }

            return Page();
        }
    }
}
