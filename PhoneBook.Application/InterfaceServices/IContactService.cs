using Microsoft.AspNetCore.Identity;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Application.DTOs.Contact;
using EditProfileDTO = PhoneBook.Application.DTOs.User.EditProfileDTO;

namespace PhoneBook.Application.InterfaceServices
{
    public interface IContactService: IAsyncDisposable
    {
        Task<ContactResult> AddContactAsync(AddContactDTO addContactDTO);
        Task<ContactResult> DeleteContactAsync(DeleteContactDTO deleteContactDTO);
        Task<ContactResult> EditContactAsync(EditContactDTO editContactDTO);
        Task<FilterContactsDTO> FilterContactsAsync(FilterContactsDTO filtercontactsDTO);
        Task<EditContactDTO> GetContactByIdAsync(GetContactByIdDTO getContactByIdDTO);

    }
}
