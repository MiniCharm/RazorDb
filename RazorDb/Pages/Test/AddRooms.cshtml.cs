using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using RazorDb.Interfaces;
using RazorDb.Modles;
using System.Linq.Expressions;

namespace RazorDb.Pages.Test
{
    //________________________________________________FUNKTIONEL
    public class AddRoomsModel : PageModel
    {
        private IRoomService _roomService;

        [BindProperty]
        public Room room { get; set; }
        [BindProperty]
        public char ChosenType { get; set; }
        public List<SelectListItem> SelectListRoomType { get; set; }
        public AddRoomsModel(IRoomService roomService)
        {
            _roomService = roomService;
            createSelectListRoomType();
        }
        public void OnGet()
        {

        }

        public void createSelectListRoomType()
        {
            SelectListRoomType = new List<SelectListItem>();
            SelectListRoomType.Add(new SelectListItem("Vælg type", "-1"));
            SelectListRoomType.Add(new SelectListItem("S", "S"));
            SelectListRoomType.Add(new SelectListItem("D", "D"));
            SelectListRoomType.Add(new SelectListItem("F", "F"));
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) 
            {
                return Page();
            }
            try
            { 
                await _roomService.CreateRoomAsync(room.HotelNr, new Room(room.RoomNr, ChosenType /*room.Types*/, room.Pris, room.HotelNr));
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {

                ViewData["ExeptionMessage"] = ex.Message;
            }
            return Page();

        }
    }
}
