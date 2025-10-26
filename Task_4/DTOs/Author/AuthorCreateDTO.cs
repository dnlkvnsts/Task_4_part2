using System.ComponentModel.DataAnnotations;

namespace Task_4.DTOs.Author
{
    public class AuthorCreateDTO
    {
        [Required(ErrorMessage = "Author name is required")]
        [StringLength(20, MinimumLength = 1,
            ErrorMessage = "Name must be from 1 and 20 ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime? DateOfBirth { get; set; }
    }
}
