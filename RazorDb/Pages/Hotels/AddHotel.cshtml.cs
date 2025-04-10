using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using RazorDb.Interfaces;
using RazorDb.Modles;
using System.Data;

namespace RazorDb.Pages.Hotels
{
    public class AddHotelModel : PageModel
    {
        IHotelServiceAsync _hotelServiceAsync;
        IWebHostEnvironment _webhostEnviroment;

        [BindProperty]
        public Hotel hotel { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }
        public AddHotelModel(IHotelServiceAsync hotelServiceAsync, IWebHostEnvironment webhost)
        {
            _hotelServiceAsync = hotelServiceAsync;
            _webhostEnviroment = webhost;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                if (Photo != null)
                {
                    if (hotel.HotelImage != null && hotel.HotelImage != "default.jpeg")
                    {
                        string filepath = Path.Combine(_webhostEnviroment.WebRootPath, "/images/memberImages", hotel.HotelImage);
                        System.IO.File.Delete(filepath);
                    }
                    hotel.HotelImage = processUploadedFile();
                }
                    _hotelServiceAsync.CreateHotelAsync(hotel);
                return RedirectToPage("GetAllHotels");
            }
            catch (Exception ex)
            {
                ViewData["ExeptionMessage"] = ex.Message;
                return null;
            }
            

        }
        private string processUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webhostEnviroment.WebRootPath, "images/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var filestream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(filestream);
                }
            }
            return uniqueFileName;
        }
    }
}

