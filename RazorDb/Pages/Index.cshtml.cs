using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDb.Modles;
using RazorDb.Interfaces;

namespace RazorDb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IUSerService _userService;
        public string UserName { get; set; }
        public User CurrentUser { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IUSerService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        //public async Task<IActionResult> OnGetAsync()
        //{
        //    //UserName = HttpContext.Session.GetString("UserName"); // springer herfra til error
        //    //if (UserName == null) // kommer slet ikke ind i if statement
        //    //{
        //    //    return RedirectToPage("Users/UserLogIn");
        //    //}
        //    //else
        //    //{
        //    //    CurrentUser = await _userService.GetUserFromUserNameAsync(UserName);
        //    //}
        //    return Page();
        //}
    }
}
