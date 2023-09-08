using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Domain.Models.User;

namespace PhoneBook.Presentation.Razor.Pages
{

    [Authorize]
    public class IndexModel : PageModel
    {


        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<RedirectToPageResult> OnGet()
        {

            return RedirectToPage("ListContacts");
        }
        public async Task<RedirectToPageResult> ConfirmEmailOnGet()
        {

            //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);


            return RedirectToPage("ListContacts");
        }
    }
}