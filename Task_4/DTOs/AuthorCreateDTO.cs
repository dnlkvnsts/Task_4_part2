using System.ComponentModel.DataAnnotations;

namespace Task_4.DTOs
{
    public class AuthorCreateDTO
    {
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
