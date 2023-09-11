using Microsoft.AspNetCore.Http;

namespace PhoneBook.Application.DTOs.Account
{
    public class EditProfileDTO
    {


        public string UserId { get; set; }  

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public IFormFile? ProfileImage { get; set; }

        public char? Gender { get; set; }
    }
}
