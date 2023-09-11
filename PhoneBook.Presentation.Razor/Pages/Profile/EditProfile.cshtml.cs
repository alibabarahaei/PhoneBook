using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Presentation.Razor.Extensions;
using PhoneBook.Presentation.Razor.Pages.ViewModels;

namespace PhoneBook.Presentation.Razor.Pages.Profile
{

    public class EditProfileModel : SiteBasePage
    {

        #region properties
        public EditProfileViewModel editProfileViewModel { get; set; }

        #endregion

        #region constructor

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public EditProfileModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
            editProfileViewModel = new EditProfileViewModel();
        }


        #endregion



        public async Task OnGet()
        {

            var user = await _userService.GetUserWithUserIdAsync(User.GetUserId());
            editProfileViewModel.FirstName = user.FirstName;
            editProfileViewModel.LastName = user.LastName;
            editProfileViewModel.PhoneNumber = user.PhoneNumber;
            editProfileViewModel.PathProfileImageBefore = user.PathProfileImage;
            editProfileViewModel.Gender = user.Gender ?? 'U';

        }



        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var newUser = _mapper.Map<EditProfileDTO>(editProfileViewModel);
                    newUser.UserId = User.GetUserId();
                    var result = await _userService.EditProfileAsync(newUser);

                    if (result.Succeeded)
                    {
                        TempData["SuccessMessage"] = "اطلاعات شما با موفقیت تغییر کرد";
                        return RedirectToPage("../ListContacts");
                    }
                    else
                    {
                        TempData["WarningMessage"] = "خطا";
                        editProfileViewModel.FirstName = null;
                        editProfileViewModel.LastName = null;
                        editProfileViewModel.PhoneNumber = null;
                        return RedirectToPage("~/ListContacts");
                    }

                }

                TempData["WarningMessage"] = "در سایت ورود کنید";
                return Page();
            }

            return Page();


        }
    }
}
