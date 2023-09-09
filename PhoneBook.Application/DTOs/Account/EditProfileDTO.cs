using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
