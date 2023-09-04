using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Domain.Models.User;
using PhoneBook.Presentation.Razor.Areas.Identity.Pages.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PhoneBook.Presentation.Razor.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {



        #region Properties
        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }
        #endregion

        #region constuctor
        private readonly IUserService _userService;
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        
        }
        #endregion


        public void OnGet()
        {
            
        }



        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(new LoginUserDTO()
                {
                    UserName = LoginViewModel.UserName,
                    Password = LoginViewModel.Password,
                    RememberMe = LoginViewModel.RememberMe
                });

                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        TempData["WarningMessage"] = "اکانت شما تا اطلاع ثانوی قفل شده است";
                        //ModelState.AddModelError("UserName", "اکانت شما تا اطلاع ثانوی قفل شده است");
                    }
                    else
                    {
                        TempData["WarningMessage"] = "نام کاربری یا رمز عبور اشتباه هست";
                        LoginViewModel.UserName = "";
                        LoginViewModel.RememberMe = false;
                        //ModelState.AddModelError("UserName", "نام کاربری یا رمز عبور اشتباه هست");
                    }
                    return Page();
                }
                TempData["SuccessMessage"] = "با موفقیت ثبت نام شدید";
                return RedirectToPage("../Index");
            }
            return Page();
        }
    }
}
