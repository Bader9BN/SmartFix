using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFix.Application.Interfaces;
using SmartFix.Domain.Entities;
using SmartFix.Infrastructur.Data;

namespace SmartFix.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly SmartFixDbContext _context;

        public TicketService(SmartFixDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MaintenanceTicket>> GetAllTicketsAsync()
        {
            return await _context.MaintenanceTickets
                .Include(t => t.Customer)
                .Include(t => t.Device)
                .Include(t => t.Technician)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<MaintenanceTicket?> GetTicketByIdAsync(int id)
        {
            return await _context.MaintenanceTickets
                .Include(t => t.Customer)
                .Include(t => t.Device)
                .Include(t => t.Technician)
                .Include(t => t.Logs)
                .Include(t => t.TicketParts).ThenInclude(tp => tp.Part)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<MaintenanceTicket> CreateTicketAsync(int customerId, int deviceId, string issueDescription, string priority)
        {
            // توليد رقم تذكرة فريد تلقائياً
            string ticketNumber = $"TCK-{DateTime.UtcNow.Year}-{new Random().Next(1000, 9999)}";

            var ticket = new MaintenanceTicket
            {
                TicketNumber = ticketNumber,
                CustomerId = customerId,
                DeviceId = deviceId,
                IssueDescription = issueDescription,
                Priority = priority,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.MaintenanceTickets.Add(ticket);

            // تسجيل الحركة الأولى في السجلات (Audit Log)
            var log = new TicketLog
            {
                Ticket = ticket,
                NewStatus = "Pending",
                Notes = "طلب صيانة جديد تم إنشاؤه من قبل العميل.",
                ChangedBy = customerId,
                CreatedAt = DateTime.UtcNow
            };
            _context.TicketLogs.Add(log);

            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<bool> UpdateTicketStatusAsync(int ticketId, string newStatus, int changedByUserId, string? notes)
        {
            var ticket = await _context.MaintenanceTickets.FindAsync(ticketId);
            if (ticket == null) return false;

            string? oldStatus = ticket.Status;
            ticket.Status = newStatus;
            ticket.UpdatedAt = DateTime.UtcNow;

            // إضافة سجل تاريخي للتغيير
            var log = new TicketLog
            {
                TicketId = ticketId,
                OldStatus = oldStatus,
                NewStatus = newStatus,
                Notes = notes,
                ChangedBy = changedByUserId,
                CreatedAt = DateTime.UtcNow
            };

            _context.TicketLogs.Add(log);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AssignTechnicianAsync(int ticketId, int technicianId)
        {
            var ticket = await _context.MaintenanceTickets.FindAsync(ticketId);
            if (ticket == null) return false;

            ticket.TechnicianId = technicianId;
            ticket.UpdatedAt = DateTime.UtcNow;

            var log = new TicketLog
            {
                TicketId = ticketId,
                NewStatus = ticket.Status,
                Notes = "تم تعيين فني جديد للمهمة.",
                ChangedBy = 1, // نفترض أن المسؤول (Admin) هو من قام بالتعيين
                CreatedAt = DateTime.UtcNow
            };

            _context.TicketLogs.Add(log);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
