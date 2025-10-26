namespace Task_4.DTOs.Author
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<BookInAuthorDTO> Books { get; set; } = new();
    }
}
