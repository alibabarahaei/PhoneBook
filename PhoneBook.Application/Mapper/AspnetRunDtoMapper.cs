using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PhoneBook.Application.DTOs.Account;
using PhoneBook.Application.DTOs.Contact;
using PhoneBook.Domain.Models.Contacts;
using PhoneBook.Domain.Models.User;

namespace PhoneBook.Application.Mapper
{
    // The best implementation of AutoMapper for class libraries -> https://www.abhith.net/blog/using-automapper-in-a-net-core-class-library/
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<AspnetRunDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class AspnetRunDtoMapper : Profile
    {
        public AspnetRunDtoMapper()
        {
            CreateMap<Contact, AddContactDTO>().ReverseMap();
            CreateMap<ApplicationUser, RegisterUserDTO>().ReverseMap();
            CreateMap<Contact, EditContactDTO>().ReverseMap();
            CreateMap<ApplicationUser, EditProfileDTO>().ReverseMap();
        }
    }
}
