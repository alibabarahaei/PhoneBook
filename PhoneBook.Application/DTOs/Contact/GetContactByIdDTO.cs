﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Domain.Models.User;

namespace PhoneBook.Application.DTOs.Contact
{
    public class GetContactByIdDTO
    {
        public ApplicationUser User { get; set; }
        public long ContactId { get; set; }
    }
}