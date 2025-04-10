using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDb.Interfaces;
using RazorDb.Modles;
using RazorDb.Services;
using System.Net;

namespace RazorDb.Pages.Hotels
{
    public class UpdateHotelModel : PageModel
    {
        IHotelServiceAsync _hotelServiceAsync;

        [BindProperty]
        public Hotel hotel { get; set; }

        //[BindProperty]
        //public string Address { get; set; }

        //[BindProperty]
        //public string Name { get; set; }

        //[BindProperty]
        //public int HotelNr { get; set; }

        public UpdateHotelModel(IHotelServiceAsync hotelServiceAsync)
        {
            _hotelServiceAsync = hotelServiceAsync;
        }

        public async Task<IActionResult> OnGetAsync(int hotelNr)
        {
            hotel = await _hotelServiceAsync.GetHotelFromIdAsync(hotelNr);
            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int HotelNr)
        {
            try
            { 
                await _hotelServiceAsync.UpdateHotelAsync(hotel,hotel.HotelNr);
            }
            catch (Exception ex)
            {
                ViewData["ExeptionMessage"] = ex.Message;
            }

            return RedirectToPage("GetAllHotels");
        }
    }
}
