using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Presentation.Razor.Areas.Identity.Pages.ViewModels;
using System.Text;

namespace PhoneBook.Presentation.Razor.Areas.Identity.Pages.Account
{

    public class RegisterModel : PageModel
    {

        #region Properties
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; }
        #endregion

        #region constuctor
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RegisterModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
                var result = await _userService.RegisterUserAsync(_mapper.Map<RegisterUserDTO>(RegisterViewModel));

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        if (error.Code.Contains("Password"))
                        {
                            ModelState.AddModelError("RegisterViewModel.Password", error.Description);
                        }
                        else if (error.Code.Contains("UserName"))
                        {
                            ModelState.AddModelError("RegisterViewModel.UserName", error.Description);
                        }
                        else
                        {
                            ModelState.AddModelError("RegisterViewModel.UserName", error.Description);
                        }
                    }
                    return Page();
                }
                var emailConfirmationToken = await _userService.GetEmailConfirmationTokenAsync(RegisterViewModel.Email);
                emailConfirmationToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(emailConfirmationToken));
                var urlEmailConfirmation = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", email = RegisterViewModel.Email, code = emailConfirmationToken },
                    protocol: Request.Scheme);
                var user = await _userService.GetUserWithEmailAsync(RegisterViewModel.Email);
                user.UrlEmailConfirmation = urlEmailConfirmation;
                await _userService.UpdateUserAsync(user);
                TempData["InfoMessage"] = "لطفا ایمیل را تایید کنید";
                TempData["SuccessMessage"] = "با موفقیت ثبت نام شدید ";
                return RedirectToPage("Login");
            }
            else
            {
                return Page();

            }
        }



        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnGetIsUserNameInUse(string UserName)
        {
            var user = await _userService.IsUserNameInUseAsync(UserName);
            if (user == null)
                return new JsonResult(true);
            return new JsonResult("نام کاربری وارد شده توسط شخص دیگری انتخاب شده است");
        }
    }
}
