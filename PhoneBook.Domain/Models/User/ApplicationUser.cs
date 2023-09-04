using Microsoft.AspNetCore.Identity;
using PhoneBook.Domain.Models.Contacts;
using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace PhoneBook.Domain.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "نام")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? FirstName { get; set; }


        [Display(Name = "نام خانوادگی")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string? LastName { get; set; }


        [Display(Name = "تاریخ عضویت")] public DateTime CreationDate { get; set; } = DateTime.Now;

        [Display(Name = "تصویر پروفایل")] public string? PathProfileImage { get; set; }

        public char? Gender { get; set; } = GenderTypes.Unknown;



        #region relations

        
        public ICollection<Contact> Contacts { get; set; }

        #endregion


    }
}
