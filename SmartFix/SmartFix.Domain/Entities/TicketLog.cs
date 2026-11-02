using System;

namespace SmartFix.Domain.Entities
{
    public class TicketLog
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string? OldStatus { get; set; }
        public string NewStatus { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public int ChangedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public MaintenanceTicket? Ticket { get; set; }
        public User? User { get; set; }
    }
}
