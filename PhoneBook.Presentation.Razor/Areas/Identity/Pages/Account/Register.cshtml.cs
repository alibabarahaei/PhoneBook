using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.InterfaceServices;

namespace PhoneBook.Presentation.Razor.Areas.Identity.Pages.Account
{
    [BindProperties]
    public class RegisterModel : PageModel
    {


        #region Properties
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [StringLength(40, ErrorMessage = "طول {0} باید بین {2} و {1} باشد", MinimumLength = 6)]
        [PageRemote(PageHandler = "IsUserNameInUse", HttpMethod = "Get")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [MinLength(8, ErrorMessage = "{0} باید بیشتر از 7 کاراکتر باشد")]
        [StringLength(30, ErrorMessage = "طول {0} باید بین {2} و {1} باشد", MinimumLength = 8)]
        public string Password { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [Compare("Password", ErrorMessage = "رمز عبور شما تطابق ندارد")]
        public string ConfirmPassword { get; set; }

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
                    UserName = UserName,
                   
                    Password = Password
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
