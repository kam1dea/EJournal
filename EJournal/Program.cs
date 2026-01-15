using EJournal.Data;
using EJournal.Interfaces;
using EJournal.Repositories;
using EJournal.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// База данных
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Razor Pages и API
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("http://localhost:5119"); // или http если не https
});


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Репозитории
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Сервисы
builder.Services.AddScoped<IJournalService, JournalService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapRazorPages();
app.MapControllers();

app.Run();