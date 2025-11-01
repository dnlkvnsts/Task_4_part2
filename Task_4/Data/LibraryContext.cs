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
            modelBuilder.Entity<Author>()
                 .HasMany(a => a.Books)
                 .WithOne(b => b.Author)
                 .HasForeignKey(b => b.AuthorId)
                 .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Nastya", DateOfBirth = new DateTime(1988, 12, 10) },
                new Author { Id = 2, Name = "Kira", DateOfBirth = new DateTime(1966, 10, 09) },
                new Author { Id = 3, Name = "George Orwell", DateOfBirth = new DateTime(1903, 06, 25) }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Sea", PublishedYear = 1987, AuthorId = 1 },
                new Book { Id = 2, Title = "Dog", PublishedYear = 1966, AuthorId = 2 },
                new Book { Id = 3, Title = "1984", PublishedYear = 1949, AuthorId = 3 },
                new Book { Id = 4, Title = "Animal Farm", PublishedYear = 1945, AuthorId = 3 },
                new Book { Id = 5, Title = "Homage to Catalonia", PublishedYear = 1938, AuthorId = 3 },
                new Book { Id = 6, Title = "Future Book", PublishedYear = 2018, AuthorId = 1 }
        );
        }

    }
}
