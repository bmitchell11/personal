using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using FinalProject.Services;
using FinalProject.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
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

// Make HttpContext accessible in Razor pages/layout
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Important: Session middleware must come before MapRazorPages
app.UseSession();

app.UseAuthorization();

// Redirect root "/" depending on login status
app.MapGet("/", async context =>
{
    var user = context.Session.GetString("User");
    if (!string.IsNullOrEmpty(user))
        context.Response.Redirect("/Dashboard");
    else
        context.Response.Redirect("/Login");
    await Task.CompletedTask;
});

// Map Razor Pages
app.MapRazorPages();

app.Run();
