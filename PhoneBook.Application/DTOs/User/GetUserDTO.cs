using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook.Application.DTOs.User
{
    public class GetUserDTO
    {
        public ClaimsPrincipal User { get; set; }
    }
}
