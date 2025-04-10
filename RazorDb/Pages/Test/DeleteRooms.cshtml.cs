using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDb.Interfaces;
using RazorDb.Modles;

namespace RazorDb.Pages.Test
{
    //________________________________________________FUNKTIONEL
    public class DeleteRoomsModel : PageModel
    {
        private IRoomService _roomservice;

        [BindProperty]
        public Room room { get; set; }

        public string ErrorMessage { get; set; }

        public DeleteRoomsModel(IRoomService roomservice)
        {
            _roomservice = roomservice;
        }
        public async Task OnGetAsync(int roomNr,int hotelNr)
        {
            room = await _roomservice.GetRoomFromIdAsync(roomNr, hotelNr);
        }

        public async Task<IActionResult> OnPostAsync(int hotelNr)
        {
            try 
            {             
                await _roomservice.DeleteRoomAsync(room.RoomNr,room.HotelNr);
                return RedirectToPage("Index");
            }
            catch 
            {
                ErrorMessage = "Værelse ikke slettet. Mulige fejl: Ikke eksisterende værelses nummer";
                return Page();
            }

        }
    }
}

