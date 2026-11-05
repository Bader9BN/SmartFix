using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFix.Application.Interfaces;
using SmartFix.Domain.Entities;
using SmartFix.Domain.Enums;
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

        public async Task<MaintenanceTicket> CreateTicketAsync(int customerId, int deviceId, string issueDescription, string priorityStr)
        {
            string ticketNumber = $"TCK-{DateTime.UtcNow.Year}-{new Random().Next(1000, 9999)}";

            Enum.TryParse<Priority>(priorityStr, true, out var priorityEnum);

            var ticket = new MaintenanceTicket
            {
                TicketNumber = ticketNumber,
                CustomerId = customerId,
                DeviceId = deviceId,
                IssueDescription = issueDescription,
                Priority = priorityEnum,
                Status = TicketStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.MaintenanceTickets.Add(ticket);

            var log = new TicketLog
            {
                Ticket = ticket,
                NewStatus = TicketStatus.Pending,
                Notes = "طلب صيانة جديد تم إنشاؤه من قبل العميل.",
                ChangedBy = customerId,
                CreatedAt = DateTime.UtcNow
            };
            _context.TicketLogs.Add(log);

            await _context.SaveChangesAsync();
            return ticket;
        }

        public async Task<bool> UpdateTicketStatusAsync(int ticketId, string newStatusStr, int changedByUserId, string? notes)
        {
            var ticket = await _context.MaintenanceTickets.FindAsync(ticketId);
            if (ticket == null) return false;

            TicketStatus? oldStatus = ticket.Status;

            if (Enum.TryParse<TicketStatus>(newStatusStr, true, out var newStatusEnum))
            {
                ticket.Status = newStatusEnum;
            }

            ticket.UpdatedAt = DateTime.UtcNow;

            var log = new TicketLog
            {
                TicketId = ticketId,
                OldStatus = oldStatus,
                NewStatus = ticket.Status,
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
                ChangedBy = 1,
                CreatedAt = DateTime.UtcNow
            };

            _context.TicketLogs.Add(log);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
