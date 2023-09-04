using Microsoft.AspNetCore.Identity;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Domain.Models.User;
using System.Security.Claims;

namespace PhoneBook.Application.InterfaceServices
{
    public interface IUserService : IAsyncDisposable
    {

        Task<IdentityResult> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<IdentityUser> IsUserNameInUseAsync(string userName);
        Task<SignInResult> LoginUserAsync(LoginUserDTO loginUserDTO);
        Task LogOutUserAsync();
        Task<IdentityResult> EditProfileAsync(EditProfileDTO editProfileDTO);
        Task<ApplicationUser> GetUserAsync(ClaimsPrincipal claimsPrincipal);
        Task<ApplicationUser> GetUserAsync(string userId);
        Task<string> GetUserNameAsync(string userId);
        Task<string> GetUserNameAsync(ClaimsPrincipal claimsPrincipal);
        Task<IdentityResult> ChangePasswordAsync(ChangepasswordDTO changepasswordDTO);

    }
}
