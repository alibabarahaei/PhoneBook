using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Application.DTOs.Contact;
using PhoneBook.Application.DTOs.Paging;
using PhoneBook.Application.DTOs.User;
using PhoneBook.Application.Extensions;
using PhoneBook.Application.InterfaceServices;
using PhoneBook.Application.Utilities;
using PhoneBook.Domain.InterfaceRepositories.Base;
using PhoneBook.Domain.Models.Contacts;
using PhoneBook.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneBook.Application.Mapper;

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





        public async Task<ContactResult> AddContactAsync(AddContactDTO addContactDTO)
        {
            var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(addContactDTO.ContactImage.FileName);

            var res = addContactDTO.ContactImage.AddImageToServer(imageName, SD.ContactImagesOriginServer, 150, 150,
                SD.ContactImagesThumbServer);

            if (res)
            {
                // create product
                var newContact = ObjectMapper.Mapper.Map<Contact>(addContactDTO);
                newContact.PathContactImage = imageName;
                var user = await _userManager.FindByIdAsync(addContactDTO.UserId);
                newContact.User = user;
                await _contactRepository.AddEntity(newContact);
                await _contactRepository.SaveChanges();
                return ContactResult.Success;
            }

            return ContactResult.Error;
        }

        public async Task<ContactResult> DeleteContactAsync(DeleteContactDTO deleteContactDTO)
        {
            var contact = await _contactRepository.GetEntityById(deleteContactDTO.ContactId);
            if (contact != null && contact.User.Id == deleteContactDTO.UserId)
            {
                 _contactRepository.DeletePermanent(contact);
                await _contactRepository.SaveChanges();
                 return ContactResult.Success;
            }

            return ContactResult.Error;
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
            await _contactRepository.SaveChanges();
            return ContactResult.Success;

        }

        public async Task<FilterContactsDTO> FilterContactsAsync(FilterContactsDTO filterontactsDTO)
        {
            var query = _contactRepository.GetQuery().AsQueryable();

            var user = await _userManager.FindByIdAsync(filterontactsDTO.UserId);
            query = query.Where(c => c.User.UserName == user.UserName);


            #region filter

            if (!string.IsNullOrEmpty(filterontactsDTO.FirstName))
                query = query.Where(s => EF.Functions.Like(s.FirstName, $"%{filterontactsDTO.FirstName}%"));

            if (!string.IsNullOrEmpty(filterontactsDTO.LastName))
                query = query.Where(s => EF.Functions.Like(s.LastName, $"%{filterontactsDTO.LastName}%"));
            if (!string.IsNullOrEmpty(filterontactsDTO.PhoneNumber))
                query = query.Where(s => EF.Functions.Like(s.PhoneNumber, $"%{filterontactsDTO.PhoneNumber}%"));
            #endregion

            #region paging

            var pager = Pager.Build(filterontactsDTO.PageId, await query.CountAsync(), filterontactsDTO.TakeEntity, filterontactsDTO.HowManyShowPageAfterAndBefore);
            var allEntities = await query.Paging(pager).ToListAsync();

            #endregion

            return filterontactsDTO.SetProducts(allEntities).SetPaging(pager);
        }
    

        public async Task<EditContactDTO> GetContactByIdAsync(GetContactByIdDTO getContactByIdDTO)
        {
            var contact =await _contactRepository.GetQuery().Include("User").FirstOrDefaultAsync(c => (c.User.Id == getContactByIdDTO.UserId) && (c.Id == getContactByIdDTO.ContactId));
               var editContactDTO = ObjectMapper.Mapper.Map<EditContactDTO>(contact);
               if (editContactDTO!=null)
               {
                   editContactDTO.ContactId = getContactByIdDTO.ContactId;
               }

               return editContactDTO;
        }



        #region dispose


        public async ValueTask DisposeAsync()
        {
            //  await _contactManager.DisposeAsync()
        }

        #endregion
    }
}
