using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quizzify_API;
using Quizzify_BLL;
using Quizzify_BLL;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
var _configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<QuizzifyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//JWT Authentication

var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudience = _configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });


// Add authorization policies


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

builder.Services.AddMemoryCache();
// Register UserService with the dependency injection container

// Register DAL services
builder.Services.AddScoped<UserDAL>();
builder.Services.AddScoped<QuestionDAL>();
builder.Services.AddScoped<QuizDAL>();
builder.Services.AddScoped<AttemptQuizDAL>();


// Register BLL services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<CreateQuizService>();
builder.Services.AddScoped<AttemptQuizService>();

// Add other services and configurations as needed

builder.Services.AddControllers();

var app = builder.Build();


app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();
app.MapControllers();

app.Run();