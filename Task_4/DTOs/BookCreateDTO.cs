using System.ComponentModel.DataAnnotations;

namespace Task_4.DTOs
{
    public class BookCreateDTO
    {
        public string Title { get; set; }

        public int PublishedYear { get; set; }

        public int AuthorId { get; set; }
    }
}
