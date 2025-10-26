using Microsoft.EntityFrameworkCore;
using Task_4.Models;

namespace Task_4.Data
{
    public class LibraryContext : DbContext 
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)             
                .WithMany(a => a.Books)          
                .HasForeignKey(b => b.AuthorId)     
                .OnDelete(DeleteBehavior.Cascade);   
        }

    }
}
