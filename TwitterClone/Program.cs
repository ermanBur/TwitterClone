using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TwitterCloneApplication.Models;
using Microsoft.AspNetCore.Authentication.Cookies; // Cookie authentication için gerekli namespace
using System.Security.Claims;
using TwitterClone.Service;
using TwitterClone.Repository; // Claims için gerekli namespace

var builder = WebApplication.CreateBuilder(args);

// Connection string'i almak için kullanılan yapılandırma
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TwitterCloneContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddHttpContextAccessor();

// Logging yapılandırması
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.AddDebug();
});

// UserService ve IUserService arasındaki bağımlılığı enjekte eder
builder.Services.AddScoped<IUserService, UserService>();

// Cookie bazlı kimlik doğrulama için gerekli yapılandırma
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/User/Login"; // Kullanıcı giriş yapmadığında yönlendirilecek path
    options.LogoutPath = "/User/Logout"; // Kullanıcı çıkış yaptığında yönlendirilecek path
    // Diğer cookie ayarları...
});

var app = builder.Build();

// HTTP request pipeline yapılandırması
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
