using PhoneBook.Application.DTOs.Contact;

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
