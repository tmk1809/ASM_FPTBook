using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Author { get; set; }
        public string Picture { get; set; }
    }
}
