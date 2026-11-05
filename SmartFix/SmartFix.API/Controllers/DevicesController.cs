using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFix.Domain.Entities;
using SmartFix.Infrastructur.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartFix.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly SmartFixDbContext _context;

        public DevicesController(SmartFixDbContext context)
        {
            _context = context;
        }

        // جلب جميع الأجهزة
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
            return await _context.Devices
                .Include(d => d.Customer)
                .ToListAsync();
        }

        // جلب أجهزة عميل معين
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevicesByCustomer(int customerId)
        {
            var devices = await _context.Devices
                .Where(d => d.CustomerId == customerId)
                .ToListAsync();

            return Ok(devices);
        }

        // تسجيل جهاز جديد
        [HttpPost]
        public async Task<ActionResult<Device>> CreateDevice(DeviceCreateDto dto)
        {
            var customer = await _context.Users.FindAsync(dto.CustomerId);
            if (customer == null) return BadRequest(new { message = "العميل غير موجود." });

            var device = new Device
            {
                DeviceName = dto.DeviceName,
                DeviceType = dto.DeviceType,
                SerialNumber = dto.SerialNumber,
                Brand = dto.Brand,
                CustomerId = dto.CustomerId,
                CreatedAt = System.DateTime.UtcNow
            };

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevices), new { id = device.Id }, device);
        }
    }

    public record DeviceCreateDto(string DeviceName, string DeviceType, string? SerialNumber, string? Brand, int CustomerId);
}
