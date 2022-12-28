using System.ComponentModel.DataAnnotations;

namespace FPTBook.Models
{
    public class CategoryReq
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Req { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
