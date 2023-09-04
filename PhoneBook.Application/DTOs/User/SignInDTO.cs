using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Domain.Models.User;

namespace PhoneBook.Application.DTOs.User
{
    public class SignInDTO
    {
        public ApplicationUser User { get; set; }
        public bool IsPersistence { get; set; }
    }
}
