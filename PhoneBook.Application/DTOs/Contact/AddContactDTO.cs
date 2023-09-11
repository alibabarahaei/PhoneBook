using Microsoft.AspNetCore.Http;

namespace PhoneBook.Application.DTOs.Contact
{
    public class AddContactDTO
    {
        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public IFormFile ContactImage { get; set; }

        public char? Gender { get; set; } 


    }
}
