using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Presentation.Razor.Areas.Identity.Pages.ViewModels
{
    public class LoginViewModel
    {


        #region Properties

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "{0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "یادآوری کلمه عبور")] 
        public bool RememberMe { get; set; } = false;


        ////[Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        //public string Captcha { get; set; }

        #endregion
    }
}
