
using System.ComponentModel.DataAnnotations;

namespace Task_4.Models
{
    public class Author
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
