using System.Collections.Generic;
using System.Threading.Tasks;
using SmartFix.Domain.Entities;

namespace SmartFix.Application.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<MaintenanceTicket>> GetAllTicketsAsync();
        Task<MaintenanceTicket?> GetTicketByIdAsync(int id);
        Task<MaintenanceTicket> CreateTicketAsync(int customerId, int deviceId, string issueDescription, string priority);
        Task<bool> UpdateTicketStatusAsync(int ticketId, string newStatus, int changedByUserId, string? notes);
        Task<bool> AssignTechnicianAsync(int ticketId, int technicianId);
    }
}
