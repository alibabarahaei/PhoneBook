using Microsoft.AspNetCore.Identity;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Domain.Models.User;
using System.Security.Claims;

namespace PhoneBook.Application.InterfaceServices
{
    public interface IUserService : IDisposable
    {

        Task<IdentityResult> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<IdentityUser> IsUserNameInUseAsync(string userName);
        Task<SignInResult> LoginUserAsync(LoginUserDTO loginUserDTO);
        Task LogOutUserAsync();
        Task<IdentityResult> EditProfileAsync(EditProfileDTO editProfileDTO);
        Task<ApplicationUser> GetUserWithClaimsPrincipalAsync(ClaimsPrincipal claimsPrincipal);
        Task<ApplicationUser> GetUserWithUserIdAsync(string userId);
        Task<string> GetUserNameWithUserIdAsync(string userId);
        Task<string> GetUserNameWithClaimsPrincipalAsync(ClaimsPrincipal claimsPrincipal);
        Task<IdentityResult> ChangePasswordAsync(ChangepasswordDTO changepasswordDTO);
        List<UserNotEmailConfirmedDTO> GetUsersNotEmailConfirmed();
        Task<string> GetEmailConfirmationTokenAsync(string email);
        Task DeleteUrlEmailConfirmationWithEmailAsync(List<string> emails);
        Task<ApplicationUser> GetUserWithEmailAsync(string email);
        Task UpdateUserAsync(ApplicationUser user);
        Task SignOutAsync();
    }
}
