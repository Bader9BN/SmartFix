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

// تسجيل خدمة التذاكر في نظام Dependency Injection
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

// تهيئة بيانات تجريبية (Seed Data) عند تشغيل التطبيق لأول مرة
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SmartFixDbContext>();
    if (!context.Users.Any())
    {
        var customer = new SmartFix.Domain.Entities.User
        {
            Username = "احمد علي",
            Email = "ahmed@example.com",
            PasswordHash = "hashed_pass",
            Role = "Customer",
            PhoneNumber = "0501234567"
        };
        context.Users.Add(customer);
        context.SaveChanges();

        var device = new SmartFix.Domain.Entities.Device
        {
            DeviceName = "iPhone 15 Pro",
            DeviceType = "Phone",
            SerialNumber = "SN-998877",
            CustomerId = customer.Id
        };
        context.Devices.Add(device);
        context.SaveChanges();

        var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();
        ticketService.CreateTicketAsync(customer.Id, device.Id, "الشاشة لا تستجيب للمس بعد السقوط", "High").Wait();
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tickets}/{action=Index}/{id?}");

app.Run();
