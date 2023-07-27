using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PhoneBook.Application.DTOs.Contact;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Application.Extensions;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Utilities;
using PhoneBook.Domain.InterfaceRepositories.Base;
using PhoneBook.Domain.Models.Contacts;
using PhoneBook.Domain.Models.User;

namespace PhoneBook.Application.Services
{
    public class ContactService:IContactService
    {



        #region constructor
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IGenericRepository<Contact> _contactRepository;
       

        public ContactService(IGenericRepository<Contact> productRepository, UserManager<ApplicationUser> userManager)
        {
            _contactRepository = productRepository;
            _userManager=userManager;
        }

        #endregion





        #region dispose


        public async ValueTask DisposeAsync()
        {
            //  await _contactManager.DisposeAsync()
        }

        #endregion

        public async Task<ContactResult> AddContactAsync(AddContactDTO addContactDTO)
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(addContactDTO.ContactImage.FileName);

            var res = addContactDTO.ContactImage.AddImageToServer(imageName, SD.ContactImagesOriginServer, 150, 150,
                SD.ContactImagesThumbServer);

            if (res)
            {
                // create product
                var newContact = new Contact()
                {
                    FirstName = addContactDTO.FirstName,
                    LastName = addContactDTO.LastName,
                    PhoneNumber = addContactDTO.PhoneNumber,
                    PathContactImage = imageName,
                    Gender = addContactDTO.Gender,
                    User = addContactDTO.User
                };

                await _contactRepository.AddEntity(newContact);
                await _contactRepository.SaveChanges();
                return ContactResult.Success;
            }

            return ContactResult.Error;
        }

        public async Task<ContactResult> DeleteContactAsync(DeleteContactDTO deleteContactDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<ContactResult> EditContactAsync(EditContactDTO editContactDTO)
        {

            var currentcontact = await _contactRepository.GetEntityById(editContactDTO.ContactId);


            if (editContactDTO.ContactImage != null && editContactDTO.ContactImage.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editContactDTO.ContactImage.FileName);
                editContactDTO.ContactImage.AddImageToServer(imageName, SD.ContactImagesOriginServer, 150, 150, SD.ContactImagesThumbServer, currentcontact.PathContactImage);
                currentcontact.PathContactImage = imageName;
            }

            currentcontact.FirstName = editContactDTO.FirstName;
            currentcontact.LastName = editContactDTO.LastName;
            currentcontact.PhoneNumber = editContactDTO.PhoneNumber;
            currentcontact.Gender=editContactDTO.Gender;
            _contactRepository.EditEntity(currentcontact);
            _contactRepository.SaveChanges();
            return ContactResult.Success;

        }

        public Task<FilterContactsDTO> FilterContactsAsync(FilterContactsDTO contactsDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<EditContactDTO> GetContactByIdAsync(GetContactByIdDTO getContactByIdDTO)
        {
            var contact = await _contactRepository.GetEntityById(getContactByIdDTO.ContactId);
            if (contact == null) return null;
            if (contact.User.Id == getContactByIdDTO.User.Id)
            {
                return new EditContactDTO()
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    PhoneNumber = contact.PhoneNumber,
                    Gender = contact.Gender,
                    PathContactImage = contact.PathContactImage
                };
            }
            else
            {
                return null;
            }
        }
    }
}
