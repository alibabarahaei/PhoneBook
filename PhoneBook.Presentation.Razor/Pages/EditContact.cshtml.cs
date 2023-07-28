using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneBook.Application.DTOs.Contact;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Services;
using PhoneBook.Domain.Models.Contacts;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PhoneBook.Presentation.Razor.Pages
{
    [Authorize]
    [BindProperties]
    public class EditContactModel : PageModel
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
        
        public IFormFile? ProfileImage { get; set; }

        public long ContactId { get; set; }

        public string? PathContactImage { get; set; }

        #endregion

        #region constructor

        private readonly IUserService _userService;
        private readonly IContactService _contactService;

        public EditContactModel(IUserService userService, IContactService contactService)
        {

            _userService = userService;
            _contactService = contactService;

        }


        #endregion






        public async Task<IActionResult> OnGet(long contactid)
        {
            if (contactid == null)
            {
                return RedirectToPage();
            }

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserAsync((new GetUserDTO()
                {
                    User = User
                }));
                var contact = await _contactService.GetContactByIdAsync(new GetContactByIdDTO()
                {
                    User = user,
                    ContactId = contactid
                });
                if (contact == null)
                {
                    return RedirectToPage();
                }

                ContactId = contactid;
                FirstName =contact.FirstName;
                LastName=contact.LastName;
                PhoneNumber = contact.PhoneNumber;
                PathContactImage = contact.PathContactImage;
                Gender = contact.Gender;
                return Page();
            }
            TempData["WarningMessage"] = "در سایت ورود کنید";
            return RedirectToPage();
        }


        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost()
        {



            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userService.GetUserAsync(new GetUserDTO()
                    {
                        User = User
                    });

                    var editConatct = new EditContactDTO()
                    {
                        ContactId = ContactId,
                        FirstName = FirstName,
                        LastName = LastName,
                        PhoneNumber = PhoneNumber,
                        Gender = Gender,
                        ContactImage = ProfileImage
                    };
                    var result = await _contactService.EditContactAsync(editConatct);

                    if (result == ContactResult.Success)
                    {
                        TempData["SuccessMessage"] = "اطلاعات شما با موفقیت تغییر کرد";
                        return RedirectToPage("ListContacts");
                    }
                    else
                    {
                        TempData["WarningMessage"] = "خطا";
                        FirstName = null;
                        LastName = null;
                        PhoneNumber = null;
                        return RedirectToPage("ListContacts");
                    }

                }

                TempData["WarningMessage"] = "در سایت ورود کنید";
                return Page();
            }

            return Page();


        }

    }
}
