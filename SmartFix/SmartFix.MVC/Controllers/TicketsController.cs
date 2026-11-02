using Microsoft.AspNetCore.Mvc;
using SmartFix.Application.Interfaces;
using System.Threading.Tasks;

namespace SmartFix.MVC.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // صفحة قائمة جميع التذاكر ولوحة التحكم
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return View(tickets);
        }

        // تفاصيل تذكرة محددة
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null) return NotFound();
            return View(ticket);
        }
    }
}
