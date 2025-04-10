using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDb.Interfaces;
using RazorDb.Modles;
using RazorDb.Services;

namespace RazorDb.Pages.Users
{
    public class UserLogInModel : PageModel
    {
        //IUSerService _userService;
        //[BindProperty]
        //public User user { get; set; }
        //[BindProperty]
        //public string UserName { get; set; }
        //[BindProperty]
        //public string Password { get; set; }
        //public UserLogInModel(IUSerService userService)
        //{
        //    _userService = userService;
        //}

        //public void OnGet()
        //{
        //}

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    try
        //    {
        //        User? loginUser = await _userService.VerifyUserAsync(user.UserName, user.Password);
        //        if (loginUser != null)
        //        {
        //            HttpContext.Session.SetString("UserName", loginUser.UserName);
        //            return RedirectToPage("../Index");
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        ViewData["ExceptionMessage"] = ex.Message;
        //        return Page();
        //    }
        //    return Page();

        //}
    }
}
