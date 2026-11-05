using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFix.Domain.Entities;
using SmartFix.Domain.Enums;
using SmartFix.Infrastructur.Data;
using System.Threading.Tasks;

namespace SmartFix.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly SmartFixDbContext _context;

        public InvoicesController(SmartFixDbContext context)
        {
            _context = context;
        }

        // الحصول على فاتورة تذكرة معينة
        [HttpGet("ticket/{ticketId}")]
        public async Task<ActionResult<Invoice>> GetInvoiceByTicket(int ticketId)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Ticket)
                .FirstOrDefaultAsync(i => i.TicketId == ticketId);

            if (invoice == null) return NotFound(new { message = "الفاتورة غير موجودة لهذا الطلب." });
            return Ok(invoice);
        }

        // إصدار فاتورة جديدة لصيانة منتهية
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice(InvoiceCreateDto dto)
        {
            var ticket = await _context.MaintenanceTickets.FindAsync(dto.TicketId);
            if (ticket == null) return BadRequest(new { message = "التذكرة غير موجودة." });

            var invoiceNumber = $"INV-{System.DateTime.UtcNow.Year}-{new System.Random().Next(1000, 9999)}";

            var invoice = new Invoice
            {
                TicketId = dto.TicketId,
                InvoiceNumber = invoiceNumber,
                LaborCost = dto.LaborCost,
                PartsCost = dto.PartsCost,
                Discount = dto.Discount,
                TotalAmount = (dto.LaborCost + dto.PartsCost) - dto.Discount,
                PaymentStatus = PaymentStatus.Unpaid,
                IssuedAt = System.DateTime.UtcNow
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInvoiceByTicket), new { ticketId = invoice.TicketId }, invoice);
        }

        // تحديث حالة الدفع للفاتورة
        [HttpPatch("{id}/pay")]
        public async Task<IActionResult> PayInvoice(int id, [FromBody] string paymentMethod)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) return NotFound();

            invoice.PaymentStatus = PaymentStatus.Paid;
            invoice.PaymentMethod = paymentMethod;
            invoice.PaidAt = System.DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

    public record InvoiceCreateDto(int TicketId, decimal LaborCost, decimal PartsCost, decimal Discount);
}
