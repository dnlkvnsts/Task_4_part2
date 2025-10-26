namespace Task_4.DTOs.Book
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PublishedYear { get; set; }
        public AuthorInBookDTO Author { get; set; }
    }
}
