using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// MySQL Database Connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 32)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    ));

// Dependency Injection: register all services
builder.Services.AddScoped<EmailSender>();
builder.Services.AddScoped<PackageService>();
builder.Services.AddScoped<ResidentService>();
builder.Services.AddScoped<AuthService>();

// Enable session for login management
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // serve wwwroot files

app.UseRouting();
app.UseAuthorization();
app.UseSession(); // enable session middleware

// Map Razor Pages
app.MapRazorPages();

app.Run();
