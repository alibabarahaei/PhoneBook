using Microsoft.AspNetCore.Identity;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.Extensions;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Utilities;
using PhoneBook.Domain.Models.User;
using System.Security.Claims;



namespace PhoneBook.Application.Services
{
    public class UserService : IUserService
    {
        #region constructor
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #endregion


        #region dispose

        public void Dispose()
        {
            _userManager.Dispose();
        }


        #endregion


        public async Task<IdentityResult> RegisterUserAsync(RegisterUserDTO registerUserDTO)
        {
            var user = new ApplicationUser()
            {
                UserName = registerUserDTO.UserName,
                Email = registerUserDTO.Email,
            };
            var IdentityResult = await _userManager.CreateAsync(user, registerUserDTO.Password);
            //if (IdentityResult.Succeeded)
            //{

            //var emailConfirmationNumber = rnd.Next(10000, 100000);
            //_messageSender.SendEmail(registerUserDTO.Email, "تاییدیه ایمیل", $"code : {emailConfirmationNumber}");

            //}
            return IdentityResult;

        }

        public async Task<IdentityUser> IsUserNameInUseAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }


        public async Task<SignInResult> LoginUserAsync(LoginUserDTO loginUserDTO)
        {

            var result = await _signInManager.PasswordSignInAsync(loginUserDTO.UserName, loginUserDTO.Password, loginUserDTO.RememberMe, true);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginUserDTO.UserName);
                await _signInManager.SignInAsync(user, loginUserDTO.RememberMe);
            }
            return result;
        }


        public async Task LogOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<IdentityResult> EditProfileAsync(EditProfileDTO editProfileDTO)
        {
            var currentUser = await GetUserWithUserIdAsync(editProfileDTO.UserId);
            currentUser.FirstName = editProfileDTO.FirstName;
            currentUser.LastName = editProfileDTO.LastName;
            currentUser.PhoneNumber = editProfileDTO.PhoneNumber;

            if (editProfileDTO.Gender != null)
            {
                currentUser.Gender = editProfileDTO.Gender;
            }
            else
            {
                currentUser.Gender = GenderTypes.Unknown;
            }
            if (editProfileDTO.ProfileImage != null && editProfileDTO.ProfileImage.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editProfileDTO.ProfileImage.FileName);
                editProfileDTO.ProfileImage.AddImageToServer(imageName, SD.UserProfileOriginServer, 64, 64, SD.UserProfileThumbServer, currentUser.PathProfileImage);
                currentUser.PathProfileImage = imageName;
            }
            return await _userManager.UpdateAsync(currentUser);
        }

        public async Task<ApplicationUser> GetUserWithUserIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<string> GetUserNameWithUserIdAsync(string userId)
        {
            var user = await GetUserWithUserIdAsync(userId);
            return user.UserName;
        }

        public async Task<string> GetUserNameWithClaimsPrincipalAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await GetUserWithClaimsPrincipalAsync(claimsPrincipal);
            return user.UserName;
        }

        public async Task<ApplicationUser> GetUserWithClaimsPrincipalAsync(ClaimsPrincipal claimsPrincipal)
        {
            return await _userManager.GetUserAsync(claimsPrincipal);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangepasswordDTO changepasswordDTO)
        {
            var user = await GetUserWithUserIdAsync(changepasswordDTO.UserId);
            return await _userManager.ChangePasswordAsync(user, changepasswordDTO.CurrentPassword, changepasswordDTO.NewPassword);
        }

        public List<UserNotEmailConfirmedDTO> GetUsersNotEmailConfirmed()
        {

            var userNotEmailConfirmed = _userManager.Users.Where(u => ((u.EmailConfirmed == false) && (u.UrlEmailConfirmation != null))).Select(u => new UserNotEmailConfirmedDTO()
            {
                Email = u.Email,
                UrlEmailConfirmed = u.UrlEmailConfirmation
            }).ToList();

            return userNotEmailConfirmed;
        }

        public async Task<string> GetEmailConfirmationTokenAsync(string email)
        {
            var user = await GetUserWithEmailAsync(email);
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return emailConfirmationToken;
        }

        public async Task DeleteUrlEmailConfirmationWithEmailAsync(List<string> emails)
        {
            foreach (var email in emails)
            {
                var user = await GetUserWithEmailAsync(email);
                user.UrlEmailConfirmation = null;
                await UpdateUserAsync(user);
            }
        }


        public async Task<ApplicationUser> GetUserWithEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            return await _userManager.ConfirmEmailAsync(user, code);

        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
