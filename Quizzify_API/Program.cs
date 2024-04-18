using Microsoft.EntityFrameworkCore;
using Quizzify_BLL;
using Quizzify_DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<QuizzifyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddControllersWithViews();
builder.Services.AddDataProtection();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // Update with your Redis server configuration
});

// Configure session services
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".My.Session.Cookie";
    options.IdleTimeout = TimeSpan.FromSeconds(1000); // Adjust the timeout as needed
});
// Configure the HTTP request pipeline.

// Register IMemoryCache with the dependency injection container

//builder.Services.AddMemoryCache();
//// Register UserService with the dependency injection container

//builder.Services.AddScoped<UserService>();

var app = builder.Build();


app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseSession();
app.MapControllers();

app.Run();
