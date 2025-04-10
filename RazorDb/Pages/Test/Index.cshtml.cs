using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorDb.Interfaces;
using RazorDb.Modles;

namespace RazorDb.Pages.Test
{
    public class IndexModel : PageModel
    {
        //________________________________________________FUNKTIONEL
        #region Instance Fields
        private IRoomService _roomService;
        #endregion

        #region Properties
        public List<Room> Rooms { get; private set; }

        [BindProperty(SupportsGet = true)]
        public double FiltherPrice { get; set; }
        #endregion

        #region Constructor
        public IndexModel(IRoomService roomService)
        {
            _roomService = roomService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync()
        {

            Rooms = await _roomService.GetAllRoomAsync();
            if (FiltherPrice > 0 && FiltherPrice != null)
            {
                Rooms = Rooms.FindAll(r => r.Pris <= FiltherPrice);
            }
            return Page();

        }
        #endregion

    }
}
