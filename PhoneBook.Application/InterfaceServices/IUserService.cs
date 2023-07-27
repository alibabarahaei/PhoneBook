using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.DTOs.User;

using PhoneBook.Domain.Models.User;

namespace PhoneBook.Application.InterfaceServices
{
    public interface IUserService : IAsyncDisposable
    {

        Task<IdentityResult> RegisterUserAsync(RegisterUserDTO registerUserDTO);
        Task<IdentityUser> IsUserNameInUseAsync(string userName);
        Task<SignInResult> LoginUserAsync(LoginUserDTO loginUserDTO);
        Task LogOutUserAsync();
        Task<IdentityResult> EditProfileAsync(EditProfileDTO editProfileDTO);
        Task<ApplicationUser> GetUserAsync(GetUserDTO getuserDTO);
        Task<IdentityResult> ChangePasswordAsync(ChangepasswordDTO changepasswordDTO);
       
    }
}
