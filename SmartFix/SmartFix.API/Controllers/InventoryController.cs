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
    public class InventoryController : ControllerBase
    {
        private readonly SmartFixDbContext _context;

        public InventoryController(SmartFixDbContext context)
        {
            _context = context;
        }

        // الحصول على جميع قطع الغيار
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryPart>>> GetParts()
        {
            return await _context.InventoryParts.ToListAsync();
        }

        // إضافة قطعة غيار جديدة للمخزون
        [HttpPost]
        public async Task<ActionResult<InventoryPart>> AddPart(InventoryPart part)
        {
            part.CreatedAt = System.DateTime.UtcNow;
            _context.InventoryParts.Add(part);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetParts), new { id = part.Id }, part);
        }

        // تحديث كمية القطع في المخزون
        [HttpPatch("{id}/stock")]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] int quantity)
        {
            var part = await _context.InventoryParts.FindAsync(id);
            if (part == null) return NotFound();

            part.StockQuantity = quantity;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
