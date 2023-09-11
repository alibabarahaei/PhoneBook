using Microsoft.AspNetCore.Mvc;

namespace PhoneBook.Presentation.Razor.Pages
{


    public class IndexModel : SiteBasePage
    {


        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<RedirectToPageResult> OnGet()
        {
            return RedirectToPage("ListContacts");
        }
       
    }
}