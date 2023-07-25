using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

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


    }
}
