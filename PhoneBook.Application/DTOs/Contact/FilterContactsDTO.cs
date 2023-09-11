using PhoneBook.Application.DTOs.Paging;

namespace PhoneBook.Application.DTOs.Contact
{
    public class FilterContactsDTO:BasePaging
    {

        public string UserId { get; set; }

        public long ContactId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? PhoneNumber { get; set; }


        public List<Domain.Models.Contacts.Contact>? Contacts { get; set; }

        

        #region methods

        public FilterContactsDTO SetProducts(List<Domain.Models.Contacts.Contact> contacts)
        {
            this.Contacts = contacts;
            return this;
        }

        public FilterContactsDTO SetPaging(BasePaging paging)
        {
            this.PageId = paging.PageId;
            this.AllEntitiesCount = paging.AllEntitiesCount;
            this.StartPage = paging.StartPage;
            this.EndPage = paging.EndPage;
            this.HowManyShowPageAfterAndBefore = paging.HowManyShowPageAfterAndBefore;
            this.TakeEntity = paging.TakeEntity;
            this.SkipEntity = paging.SkipEntity;
            this.PageCount = paging.PageCount;
            return this;
        }

        #endregion

    }
    public enum ContactResult
    {
        Success,
        Error
    }
}
