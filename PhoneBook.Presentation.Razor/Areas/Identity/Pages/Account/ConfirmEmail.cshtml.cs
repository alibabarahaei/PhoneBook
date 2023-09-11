using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using PhoneBook.Application.InterfaceServices;
using System.Text;

namespace PhoneBook.Presentation.Razor.Areas.Identity.Pages.Account
{
    public class ConfirmEmailModel : PageModel
    {


        #region constuctor
        private readonly IUserService _userService;

        public ConfirmEmailModel(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        public async Task<IActionResult> OnGet(string email, string code)
        {
            if (email == null || code == null)
            {
                ViewData["result"] = "لینک خراب است";
                TempData["WarningMessage"] = "لینک خراب است";
            }
            else
            {
                var user = await _userService.GetUserWithEmailAsync(email);
                if (user == null)
                {
                    ViewData["result"] = "ایمیل مورد نظر پیدا نشد";
                    TempData["WarningMessage"] = "ایمیل مورد نظر پیدا نشد";
                }
                else
                {
                    code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                    var result = await _userService.ConfirmEmailAsync(user, code);
                    if (result.Succeeded)
                    {
                        ViewData["result"] = "ایمیل با موفقیت تایید شد";
                        TempData["SuccessMessage"] = "ایمیل با موفقیت تایید شد";
                    }
                    else
                    {
                        ViewData["result"] = "ایمیل  تایید نشد";
                        TempData["WarningMessage"] = "ایمیل  تایید نشد";
                    }

                }
            }
            return Page();
        }


    }
}
