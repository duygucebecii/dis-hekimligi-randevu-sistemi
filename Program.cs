using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// ✅ BURASI ÇOK ÖNEMLİ:
// Default authentication scheme olarak Cookie'yi belirliyoruz
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index";          // Giriş sayfan
        options.AccessDeniedPath = "/Login/AccessDenied"; // Yetki yoksa
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

builder.Services.AddAuthorization();

// Session + HttpContextAccessor
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// ✅ Authentication → Authorization sırası ÖNEMLİ
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

// Default route: Login/Index ile başlasın
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
