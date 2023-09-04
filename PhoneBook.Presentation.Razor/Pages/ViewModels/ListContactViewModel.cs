using PhoneBook.Application.DTOs.Paging;
using PhoneBook.Domain.Models.User;

namespace PhoneBook.Presentation.Razor.Pages.ViewModels
{
    public class ListContactViewModel:BasePaging
    {

        #region properteis

        public ApplicationUser User { get; set; }


        public long ContactId { get; set; }

        public string? FirstName { get; set; }


        public string? LastName { get; set; }


        public string? PhoneNumber { get; set; }


        public List<Domain.Models.Contacts.Contact>? Contacts { get; set; }

        #endregion

    }
}
