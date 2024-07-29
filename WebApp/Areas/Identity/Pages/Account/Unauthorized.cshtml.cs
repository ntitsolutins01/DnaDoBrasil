using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class UnauthorizedModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}