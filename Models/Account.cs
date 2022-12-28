using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
    }
}
