using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneBook.Application.InterfaceServices;

namespace PhoneBook.Presentation.Razor.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {

        #region constuctor
        private readonly IUserService _userService;


        public LogoutModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion



        public async Task<RedirectToPageResult> OnGet()
        {
            await _userService.SignOutAsync();
            return RedirectToPage("Login");
        }
    }
}
