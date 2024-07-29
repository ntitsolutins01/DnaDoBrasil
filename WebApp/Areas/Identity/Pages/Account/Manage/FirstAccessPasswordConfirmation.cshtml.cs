using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    [AllowAnonymous]
    public class FirstAccessPasswordConfirmationModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}