using Microsoft.AspNetCore.Http;

namespace PhoneBook.Application.DTOs.Contact
{
    public class EditContactDTO
    {

        public long ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string PathContactImage { get; set; }

        public IFormFile ContactImage { get; set; }

        public char? Gender { get; set; }
    }
  
}
