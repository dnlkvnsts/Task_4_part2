using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Task_4.Data;
using Task_4.DTOs;
using Task_4.Filters;
using Task_4.Repositories;
using Task_4.Services;
using Task_4.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidFilter>();

}
);

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();


builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IValidator<AuthorCreateDTO>, AuthorValidator>();
builder.Services.AddScoped<IValidator<BookCreateDTO>, BookValidator>();



builder.Services.AddScoped<IMapperService, MapperService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var supportedCultures = new[] { "en-US", "ru-Ru" };

var localisationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);


app.UseRequestLocalization(localisationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
        dbContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();