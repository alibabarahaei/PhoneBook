using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Application.DTOs.Contact;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Presentation.Razor.Extensions;
using PhoneBook.Presentation.Razor.Pages.ViewModels;

namespace PhoneBook.Presentation.Razor.Pages
{


    public class AddContactModel : SiteBasePage
    {


        #region properties
        public AddContactViewModel AddContactViewModel { get; set; }
        private readonly IMapper _mapper;
        #endregion

        #region constructor

        private readonly IUserService _userService;
        private readonly IContactService _contactService;

        public AddContactModel(IMapper mapper, IUserService userService, IContactService contactService)
        {
            _mapper = mapper;
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
                    var newConatct = _mapper.Map<AddContactDTO>(AddContactViewModel);
                    newConatct.UserId = User.GetUserId();
                    var result = await _contactService.AddContactAsync(newConatct);

                    if (result == ContactResult.Success)
                    {
                        TempData["SuccessMessage"] = "اطلاعات شما با موفقیت تغییر کرد";
                        return RedirectToPage("ListContacts");
                    }
                    else
                    {
                        TempData["WarningMessage"] = "خطا";
                        AddContactViewModel.FirstName = null;
                        AddContactViewModel.LastName = null;
                        AddContactViewModel.PhoneNumber = null;
                        return RedirectToPage("ListContacts");
                    }

                }

                TempData["WarningMessage"] = "در سایت ورود کنید";
                return RedirectToPage();
            }
            return Page();


        }


    }
}
