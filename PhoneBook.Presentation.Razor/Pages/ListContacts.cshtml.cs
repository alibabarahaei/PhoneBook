using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneBook.Application.DTOs.Contact;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Presentation.Razor.Pages.ViewModels;
using PhoneBook.Presentation.Razor.Extensions;

namespace PhoneBook.Presentation.Razor.Pages
{
 
    public class ListContactsModel : SiteBasePage
    {
        #region properties

        public ListContactViewModel FilterContacts { get; set; }
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

                filterContacts.UserId = User.GetUserId();
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








        public async Task<IActionResult> OnGetRemoveContact(long contactId)
        {

            if (User.Identity.IsAuthenticated)
            {
                var result = await _contactService.DeleteContactAsync(new DeleteContactDTO()
                {
                    UserId = User.GetUserId(),
                    ContactId = contactId
                });
                if (result == ContactResult.Success)
                {
                    TempData["SuccessMessage"] = "مخاطب مورد نظر حذف شد";
                }
                else if(result==ContactResult.Error)
                {
                    TempData["WarningMessage"] = "حذف با موفقیت انجام نشد";
                }
                return RedirectToPage("ListContacts");
            }

            TempData["WarningMessage"] = "در سایت ورود کنید";
            return RedirectToPage();

        }



    }
}
