using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartFix.Application.Interfaces;
using SmartFix.Application.Services;
using SmartFix.Domain.Entities;
using SmartFix.Infrastructur.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// استخدام قاعدة بيانات في الذاكرة (InMemory DB) للتجربة والاختبار السريع
builder.Services.AddDbContext<SmartFixDbContext>(options =>
    options.UseInMemoryDatabase("SmartFixDb"));

// إعداد Identity للـ API
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<SmartFixDbContext>()
    .AddDefaultTokenProviders();

// تسجيل خدمة التذاكر في نظام Dependency Injection
builder.Services.AddScoped<ITicketService, TicketService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
