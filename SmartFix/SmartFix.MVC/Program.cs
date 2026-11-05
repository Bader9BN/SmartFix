using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SmartFix.Application.Interfaces;
using SmartFix.Application.Services;
using SmartFix.Infrastructur.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// استخدام قاعدة بيانات في الذاكرة (InMemory DB) للتجربة والاختبار السريع
builder.Services.AddDbContext<SmartFixDbContext>(options =>
    options.UseInMemoryDatabase("SmartFixDb"));

// تفعيل المصادقة عبر الكوكيز (Cookie Authentication)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
    });

// تسجيل خدمات التطبيق
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// تهيئة بيانات تجريبية (Seed Data) عند تشغيل التطبيق لأول مرة
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SmartFixDbContext>();
    if (!context.Users.Any())
    {
        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
        // إنشاء مستخدم تجريبي بصلاحية مدير (Admin)
        await authService.RegisterAsync("مدير النظام", "admin@smartfix.com", "Password123", "Admin", "0501111111");
        // إنشاء عميل تجريبي
        await authService.RegisterAsync("احمد علي", "ahmed@example.com", "Password123", "Customer", "0501234567");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // تفعيل المصادقة (مهم جداً أن تكون قبل Authorization)
app.UseAuthorization();  // تفعيل الصلاحيات

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
