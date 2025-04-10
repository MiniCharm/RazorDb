using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDb.Interfaces;
using RazorDb.Modles;
using RazorDb.Services;
using System.Dynamic;
using System.Net;

namespace RazorDb.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        private IHotelServiceAsync _hotelService;

        [BindProperty(SupportsGet = true)]
        public string FilterName { get; set; }
        public List<Modles.Hotel> Hotels { get; private set; }



        public GetAllHotelsModel(IHotelServiceAsync hotelServiceAsync)
        {
            _hotelService = hotelServiceAsync;
        }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(FilterName))
            {
                Hotels = await _hotelService.GetHotelsByNameAsync(FilterName);
            }
            else
            {
                Hotels = await _hotelService.GetAllHotelAsync();
            }
        }

    }
}
