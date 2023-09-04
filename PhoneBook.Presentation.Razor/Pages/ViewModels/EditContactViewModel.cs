using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Presentation.Razor.Pages.ViewModels
{
    public class EditContactViewModel
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
        public char? Gender { get; set; }


        [Display(Name = "تصویر مخاطب")]

        public IFormFile? ContactImage { get; set; }

        public long ContactId { get; set; }

        public string? PathContactImage { get; set; }

        #endregion

    }
}
