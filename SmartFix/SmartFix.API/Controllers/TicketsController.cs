using Microsoft.AspNetCore.Mvc;
using SmartFix.Application.Interfaces;
using SmartFix.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartFix.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // الحصول على جميع التذاكر
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaintenanceTicket>>> GetTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        // الحصول على تذكرة محددة بالـ ID
        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceTicket>> GetTicket(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound(new { message = "التذكرة غير موجودة." });
            return Ok(ticket);
        }

        // إنشاء تذكرة جديدة من تطبيق الجوال
        [HttpPost]
        public async Task<ActionResult<MaintenanceTicket>> CreateTicket(TicketCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var ticket = await _ticketService.CreateTicketAsync(
                dto.CustomerId,
                dto.DeviceId,
                dto.IssueDescription,
                dto.Priority
            );

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
        }

        // تحديث حالة التذكرة
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, TicketStatusUpdateDto dto)
        {
            var result = await _ticketService.UpdateTicketStatusAsync(id, dto.NewStatus, dto.UserId, dto.Notes);
            if (!result) return BadRequest(new { message = "فشل تحديث الحالة." });
            return NoContent();
        }
    }

    // DTOs (Data Transfer Objects)
    // نستخدمها لنقل البيانات بين الـ Client والـ Server بشكل منظم وأكثر أماناً
    public record TicketCreateDto(int CustomerId, int DeviceId, string IssueDescription, string Priority);
    public record TicketStatusUpdateDto(string NewStatus, int UserId, string? Notes);
}
