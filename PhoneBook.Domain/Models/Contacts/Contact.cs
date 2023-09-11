using PhoneBook.Domain.Models.Base;
using PhoneBook.Domain.Models.User;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Domain.Models.Contacts
{
    public class Contact:BaseEntity
    {

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(25, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = " نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(25, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string LastName { get; set; }

        [Display(Name = " شماره مخاطب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(25, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "تصویر مخاطب")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PathContactImage { get; set; }

        public char? Gender { get; set; } = GenderTypes.Unknown;


        #region relations

        public ApplicationUser User { get; set; }

        #endregion
    }
}
