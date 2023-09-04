using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Presentation.Razor.Areas.Identity.Pages.ViewModels
{
    public class RegisterViewModel
    {


        #region Properties
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [StringLength(40, ErrorMessage = "طول {0} باید بین {2} و {1} باشد", MinimumLength = 6)]
        [PageRemote(PageHandler = "IsUserNameInUse", HttpMethod = "Get")]
        public string UserName { get; set; }


       
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        [StringLength(50, ErrorMessage = "طول {0} باید بین {2} و {1} باشد", MinimumLength = 6)]
        public string Email { get; set; }



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


    }
}
