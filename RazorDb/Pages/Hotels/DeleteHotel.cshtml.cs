using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDb.Interfaces;
using RazorDb.Modles;

namespace RazorDb.Pages.Hotels
{
    public class DeleteHotelModel : PageModel
    {
        private IHotelServiceAsync _hotelServiceAsync;

        [BindProperty]
        public Hotel hotel { get; set; }
        public DeleteHotelModel(IHotelServiceAsync hotelServiceAsync)
        {
            _hotelServiceAsync = hotelServiceAsync;
        }

        public async Task OnGetAsync(int hotelNr)
        {
            hotel = await _hotelServiceAsync.GetHotelFromIdAsync(hotelNr); // Vi venter nu på den asynkrone opgave
        }

        public async Task<IActionResult> OnPostAsync(int hotelNr) 
        {
            await _hotelServiceAsync.DeleteHotelAsync(hotelNr);
            return RedirectToPage("GetAllHotels");
        }
    }
}
