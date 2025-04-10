using System.ComponentModel.DataAnnotations;

namespace RazorDb.Modles
{
    public class Hotel : IComparable<Hotel>
    {
        [Required(ErrorMessage = "Hotelnummer nummer er påkrævet")]
        public int HotelNr { get; set; }

        [Required(ErrorMessage = "Navn er påkrævet")]
        [StringLength(30, ErrorMessage = "Navnet kan max være 30 kartaktere")]
        public String Navn { get; set; }

        [Required(ErrorMessage = "Adrasse er påkrævet")]
        [StringLength(50, ErrorMessage = "Adrassn kan ikke være over 70 kartaktere")]
        public String Adresse { get; set; }

        [Required(ErrorMessage = "Billed er påkrævet")]
        public string HotelImage { get; set; }

        public Hotel()
        {
            HotelImage = "default.jpeg";
        }

        public Hotel(int hotelNr, string navn, string adresse)
        {
            HotelNr = hotelNr;
            Navn = navn;
            Adresse = adresse;
            HotelImage = "default.jpeg";
        }

        public override string ToString()
        {
            return $"{nameof(HotelNr)}: {HotelNr}, {nameof(Navn)}: {Navn}, {nameof(Adresse)}: {Adresse}";
        }

        public int CompareTo(Hotel? other)
        {
            return Navn.CompareTo(other.Navn);
        }
    }
}
