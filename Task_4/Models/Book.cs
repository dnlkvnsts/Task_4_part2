using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task_4.Models
{
    public class Book
    {
        public int Id { get; set; }


        public string Title { get; set; }

       
        public int PublishedYear { get; set; }

      
        public int AuthorId { get; set; }


        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }

      
    }
}
