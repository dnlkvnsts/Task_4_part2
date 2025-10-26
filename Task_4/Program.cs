using Microsoft.EntityFrameworkCore;
using Task_4.Data;
using Task_4.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.\

builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlite("Data Source=library.db"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



using(var scope  = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();

    context.Database.EnsureCreated();

    SeedDatabase(context);
   
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SeedDatabase(LibraryContext context)
{
    if (context.Authors.Any())
    {
        return; 
    }

    var authors = new List<Author>
{
    new Author
    {
        Name = "Leo Tolstoy",
        DateOfBirth = new DateTime(1828, 9, 9)
    },
    new Author
    {
        Name = "Fyodor Dostoevsky",
        DateOfBirth = new DateTime(1821, 11, 11)
    },
    new Author
    {
        Name = "Ivan Turgenev",
        DateOfBirth = new DateTime(1818, 10, 28)
    }
};

    context.Authors.AddRange(authors);
    context.SaveChanges();

    var books = new List<Book>
{
    new Book
    {
        Title = "War and Peace",
        PublishedYear = 1869,
        AuthorId = 1
    },
    new Book
    {
        Title = "Anna Karenina",
        PublishedYear = 1877,
        AuthorId = 1
    },
    new Book
    {
        Title = "Crime and Punishment",
        PublishedYear = 1866,
        AuthorId = 2
    },
    new Book
    {
        Title = "Demons",
        PublishedYear = 1872,
        AuthorId = 2
    },
    new Book
    {
        Title = "Fathers and Sons",
        PublishedYear = 1862,
        AuthorId = 3
    }
};

    context.Books.AddRange(books);
    context.SaveChanges();

}