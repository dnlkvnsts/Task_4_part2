using System.ComponentModel.DataAnnotations;

namespace Task_4.DTOs.Book
{
    public class BookCreateDTO
    {
        [Required(ErrorMessage = "Book title is required")]
        [StringLength(20, MinimumLength = 1,ErrorMessage = "Name must be from 1 and 20 ")]

       
        public string Title { get; set; }

        [Required(ErrorMessage = "Published year is required")]
        [Range(1000, 9999)]
        public int PublishedYear { get; set; }

        [Required(ErrorMessage = "Author ID is required")]
        [Range(1, int.MaxValue, ErrorMessage = "ID must be positive number ")]
        public int AuthorId { get; set; }
    }
}
