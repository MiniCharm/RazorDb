using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDb.Interfaces;
using RazorDb.Modles;

namespace RazorDb.Pages.Test
{
    public class UpdateRoomsModel : PageModel
    {
        //________________________________________________Funktionel
        private IRoomService _roomService;
        [BindProperty]
        public Room room { get; set; }

        public string ErrorMessage { get; set; }
        public UpdateRoomsModel(IRoomService roomService)
        {
            _roomService = roomService;
        }
        public async Task<IActionResult> OnGetAsync(int hotelNr, int roomNr)
        {
            room = await _roomService.GetRoomFromIdAsync(roomNr, hotelNr);
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            try 
            { 
                await _roomService.UpdateRoomAsync(room, room.RoomNr, room.HotelNr);
                return RedirectToPage("Index"); 
            }
            catch 
            {
                ErrorMessage = "Værelset blev ikke fundet - Ugyldig indtastning.";
                return Page();
            }
            
        }
    }
}
