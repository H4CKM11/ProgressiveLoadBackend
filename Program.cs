using Microsoft.EntityFrameworkCore;
using ProgressiveLoadBackend.Data;
using ProgressiveLoadBackend.Repositories.Users;
using ProgressiveLoadBackend.Services.Cookies;
using ProgressiveLoadBackend.Services.HashingService;
using ProgressiveLoadBackend.Services.Users;

var builder = WebApplication.CreateBuilder(args);

// Adding Services
builder.Services.AddSingleton<IHashingService, HashingService>();
builder.Services.AddSingleton<ICookieService, CookieService>();
builder.Services.AddScoped<IUsersService, UsersService>();

// Adding Repositories
builder.Services.AddScoped<IUsersRepository, UsersRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<dbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

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
