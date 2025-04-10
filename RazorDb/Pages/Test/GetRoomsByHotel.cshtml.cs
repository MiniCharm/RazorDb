using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDb.Interfaces;
using RazorDb.Modles;
using System.Data;

namespace RazorDb.Pages.Test
{
    public class GetRoomsByHotelModel : PageModel
    {
        IRoomService _roomService;
        IHotelServiceAsync _hotelService;


        [BindProperty]
        public Hotel hotel { get; set; }

        public List<Room> rooms { get; set; }

        public string ErrorMessage { get; set; }

        public GetRoomsByHotelModel(IRoomService roomService,IHotelServiceAsync hotelService)
        {
            _roomService = roomService;
            _hotelService = hotelService;
        }
        public async Task OnGetAsync( int hotelNr)
        {
            //try 
            //{
                    hotel = await _hotelService.GetHotelFromIdAsync(hotelNr);
            rooms = await _roomService.GetAllRoomFromHotelNoAsync(hotel.HotelNr);
            //}
            //catch (Exception ex)
            //{
            //    rooms = new List<Room>();
            //    ViewData["ExeptionMessage"] = ex.Message;
            //    //return Page();
            //}

        }

    }
}
