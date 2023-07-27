using Microsoft.AspNetCore.Http;
using PhoneBook.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PhoneBook.Application.DTOs.Contact
{
    public class AddContactDTO
    {
        public ApplicationUser User { get; set; }

        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string PhoneNumber { get; set; }


        public IFormFile ContactImage { get; set; }

        public char? Gender { get; set; } 


    }
}
