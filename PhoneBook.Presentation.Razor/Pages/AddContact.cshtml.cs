using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Application.InterfaceServices;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using PhoneBook.Application.DTOs.Contact;

namespace PhoneBook.Presentation.Razor.Pages
{


    [BindProperties]
    public class AddContactModel : PageModel
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
        public char Gender { get; set; }


        [Display(Name = "تصویر پروفایل")]
        public IFormFile ProfileImage { get; set; }

        #endregion

        #region constructor

        private readonly IUserService _userService;
        private readonly IContactService _contactService;

        public AddContactModel(IUserService userService, IContactService contactService)
        {

            _userService = userService;
            _contactService = contactService;

        }


        #endregion




        public void OnGet()
        {
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

                    var newConatct = new AddContactDTO()
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        PhoneNumber = PhoneNumber,
                        User = user,
                        Gender = Gender,
                        ContactImage = ProfileImage
                    };
                    var result = await _contactService.AddContactAsync(newConatct);

                    if (result == ContactResult.Success)
                    {
                        TempData["SuccessMessage"] = "اطلاعات شما با موفقیت تغییر کرد";
                        return RedirectToPage();
                    }
                    else
                    {
                        TempData["WarningMessage"] = "خطا";
                        FirstName = null;
                        LastName = null;
                        PhoneNumber = null;
                        return Page();
                    }

                }

                TempData["WarningMessage"] = "در سایت ورود کنید";
                return Page();
            }

            return Page();


        }


    }
}
