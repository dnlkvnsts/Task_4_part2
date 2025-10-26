namespace Task_4.DTOs.Author
{
    public class AuthorWithBookCountDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int BookCount { get; set; }
    }
}
