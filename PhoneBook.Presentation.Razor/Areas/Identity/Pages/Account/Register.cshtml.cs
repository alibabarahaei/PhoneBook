using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Presentation.Razor.Areas.Identity.Pages.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PhoneBook.Presentation.Razor.Areas.Identity.Pages.Account
{
    [BindProperties]
    public class RegisterModel : PageModel
    {


        #region Properties

        public RegisterViewModel RegisterViewModel { get; set; }
        #endregion




        #region constuctor
        private readonly IUserService _userService;
        
        public RegisterModel(IUserService userService)
        {

            _userService = userService;
           
        }
        #endregion




        public void OnGet()
        {
        }






        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync()
        {
          

            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(new RegisterUserDTO()
                {
                    UserName = RegisterViewModel.UserName,
                   
                    Password = RegisterViewModel.Password
                });
                if (!result.Succeeded)
                {
                   
                    foreach (var error in result.Errors)
                    {

                        if (error.Code.Contains("Password"))
                        {

                            ModelState.AddModelError("Password", error.Description);
                        }
                        else if (error.Code.Contains("UserName"))
                        {

                            ModelState.AddModelError("UserName", error.Description);
                        }
                        else
                        {
                            ModelState.AddModelError("UserName", error.Description);

                        }
                        return Page();

                    }



                    return Page();
                }
                TempData["SuccessMessage"] = "با موفقیت ثبت نام شدید";
                return RedirectToPage("Login");
            }
            else
            {
                return Page();

            }
        }
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnGetIsUserNameInUse(string userName)
        {

            var user = _userService.IsUserNameInUseAsync(userName);

            if (user == null)
                return new JsonResult(true);
            return new JsonResult("نام کاربری وارد شده توسط شخص دیگری انتخاب شده است");
        }


    }
}
