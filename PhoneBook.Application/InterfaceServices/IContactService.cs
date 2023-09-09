using Microsoft.AspNetCore.Identity;
using PhoneBook.Application.DTOs.Contact;
using PhoneBook.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditProfileDTO = PhoneBook.Application.DTOs.Account.EditProfileDTO;

namespace PhoneBook.Application.InterfaceServices
{
    public interface IContactService: IDisposable
    {
        Task<ContactResult> AddContactAsync(AddContactDTO addContactDTO);
        Task<ContactResult> DeleteContactAsync(DeleteContactDTO deleteContactDTO);
        Task<ContactResult> EditContactAsync(EditContactDTO editContactDTO);
        Task<FilterContactsDTO> FilterContactsAsync(FilterContactsDTO filtercontactsDTO);
        Task<EditContactDTO> GetContactByIdAsync(GetContactByIdDTO getContactByIdDTO);

    }
}
