using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneBook.Application.DTOs.Contact;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Application.InterfaceServices;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PhoneBook.Presentation.Razor.Pages
{
    public class ListContactsModel : PageModel
    {
        #region properties

        public FilterContactsDTO FilterContacts { get; set; }
        #endregion

        #region constructor

        private readonly IUserService _userService;
        private readonly IContactService _contactService;

        public ListContactsModel(IUserService userService, IContactService contactService)
        {

            _userService = userService;
            _contactService = contactService;

        }


        #endregion






        public async Task<IActionResult> OnGet(FilterContactsDTO filterContacts)
        {
       

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserAsync((new GetUserDTO()
                {
                    User = User
                }));
                filterContacts.User = user;
                var FilterContacts = await _contactService.FilterContactsAsync(filterContacts);
                if (FilterContacts == null)
                {
                    return RedirectToPage();
                }
                else
                {
                    ViewData["FilterContacts"]= FilterContacts;
                }

                return Page();
            }
            TempData["WarningMessage"] = "در سایت ورود کنید";
            return RedirectToPage();
        }

    }
}
