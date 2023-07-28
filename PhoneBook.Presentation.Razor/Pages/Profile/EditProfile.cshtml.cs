using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Application.InterfaceServices;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PhoneBook.Presentation.Razor.Pages.Profile
{
    [Authorize]
    [BindProperties]
    public class EditProfileModel : PageModel
    {

        #region properties

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }


        [Display(Name = "شَماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} عدد باشد")]
        [DataType("Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public char Gender { get; set; }

        public string? PathProfileImageBefore { get; set; }

        [Display(Name = "تصویر پروفایل")]
        
        public IFormFile? ProfileImage { get; set; }

        #endregion

        #region constructor

        private readonly IUserService _userService;


        public EditProfileModel(IUserService userService)
        {

            _userService = userService;

        }


        #endregion






        public async Task OnGet()
        {


            var user = await _userService.GetUserAsync(new GetUserDTO()
            {
                User = User
            });
            FirstName = user.FirstName;
            LastName = user.LastName;
            PhoneNumber = user.PhoneNumber;
            PathProfileImageBefore = user.PathProfileImage;
            Gender = user.Gender ?? 'U';

        }



        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost()
        {



            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var newUser = new EditProfileDTO()
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        PhoneNumber = PhoneNumber,
                        User = User,
                        Gender = Gender,
                        ProfileImage = ProfileImage
                    };
                    var result = await _userService.EditProfileAsync(newUser);

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "اطلاعات شما با موفقیت تغییر کرد";
                        return RedirectToPage("../ListContacts");
                    }
                    else
                    {
                        TempData["WarningMessage"] = "خطا";
                        FirstName = null;
                        LastName = null;
                        PhoneNumber = null;
                        return RedirectToPage("~/ListContacts");
                    }

                }

                TempData["WarningMessage"] = "در سایت ورود کنید";
                return Page();
            }

            return Page();


        }
    }
}
