using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application.DTOs.Contact;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Presentation.Razor.Extensions;
using PhoneBook.Presentation.Razor.Pages.ViewModels;

namespace PhoneBook.Presentation.Razor.Pages
{

    public class EditContactModel : SiteBasePage
    {


        #region properties
        public EditContactViewModel EditContactViewModel { get; set; }
        #endregion

        #region constructor

        private readonly IUserService _userService;
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public EditContactModel(IUserService userService, IContactService contactService, IMapper mapper)
        {
            _userService = userService;
            _contactService = contactService;
            _mapper = mapper;
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
               
                var contact = await _contactService.GetContactByIdAsync(new GetContactByIdDTO()
                {
                    UserId = User.GetUserId(),
                    ContactId = contactid
                });
                if (contact == null)
                {
                    TempData["WarningMessage"] = "همچین مخاطبی یافت نشد";
                    return RedirectToPage("ListContacts");
                }

                EditContactViewModel=_mapper.Map<EditContactViewModel>(contact);
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
                    var editConatct = new EditContactDTO()
                    {
                        ContactId = EditContactViewModel.ContactId,
                        FirstName = EditContactViewModel.FirstName,
                        LastName = EditContactViewModel.LastName,
                        PhoneNumber = EditContactViewModel.PhoneNumber,
                        Gender = EditContactViewModel.Gender,
                        ContactImage = EditContactViewModel.ContactImage
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
                        EditContactViewModel.FirstName = null;
                        EditContactViewModel.LastName = null;
                        EditContactViewModel.PhoneNumber = null;
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
