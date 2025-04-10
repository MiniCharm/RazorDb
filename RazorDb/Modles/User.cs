using System.ComponentModel.DataAnnotations;

namespace RazorDb.Modles
{
    public class User
    {
        [Required]
        [StringLength(30, ErrorMessage = "Brugernavn må max være 30 karaktere")]
        public string UserName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Adgangskode må maks være 30 karaktere")]
        public string Password { get; set; }

        public User()
        {
            
        }

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }


    }
}
