using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhoneBook.Presentation.Razor.Pages
{
    [Authorize]
    [BindProperties]
    public class SiteBasePage : PageModel
    {
    }
}
