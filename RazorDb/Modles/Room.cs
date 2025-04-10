using System.ComponentModel.DataAnnotations;

namespace RazorDb.Modles
{
    public class Room : IComparable<Room>
    {
        [Required(ErrorMessage = "Værelses nummer er påkrævet")]
        public int RoomNr { get; set; }

        [Required(ErrorMessage = "Værelses type er påkrævet")]
        public char Types { get; set; }

        [Required(ErrorMessage = "Værelses pris er påkrævet")]
        [Range(0,10000, ErrorMessage = "Prisen skal være mellem 0 og 10.000 kr.")]
        public double Pris { get; set; }

        [Required(ErrorMessage = "Hotelnummer er påkrævet")]
        public int HotelNr { get; set; }

        public Room()
        {

        }
        public Room(int værelsesNummer)
        {

        }
        public Room(int nr, char types, double pris)
        {
            RoomNr = nr;
            Types = types;
            Pris = pris;
        }

        public Room(int nr, char types, double pris, int hotelNr) : this(nr, types, pris)
        {
            HotelNr = hotelNr;
        }

        public override string ToString()
        {
            return $"Room = {RoomNr}, Types = {Types}, Pris = {Pris}";
        }

        public int CompareTo(Room? other)
        {
            return RoomNr.CompareTo(other.RoomNr);
        }
    }
}
