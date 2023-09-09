using AutoMapper;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.DTOs.Contact;
using PhoneBook.Domain.Models.User;
using PhoneBook.Presentation.Razor.Areas.Identity.Pages.ViewModels;
using PhoneBook.Presentation.Razor.Pages.ViewModels;

namespace PhoneBook.Presentation.Razor.Mapper
{
    public class AspnetRunProfile:Profile
    {
        public AspnetRunProfile()
        {
            
            CreateMap<AddContactDTO, AddContactViewModel>().ReverseMap();
            CreateMap<EditContactDTO, EditContactViewModel>().ReverseMap();
            CreateMap<FilterContactsDTO, ListContactViewModel>().ReverseMap();
            CreateMap<LoginUserDTO, LoginViewModel>().ReverseMap();
            CreateMap<RegisterUserDTO, RegisterViewModel>().ReverseMap();
            CreateMap<EditProfileDTO, EditProfileViewModel>().ReverseMap();
        }
    }
}
