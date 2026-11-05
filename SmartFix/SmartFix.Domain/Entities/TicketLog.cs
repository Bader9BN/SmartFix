using System;
using SmartFix.Domain.Common;
using SmartFix.Domain.Enums;

namespace SmartFix.Domain.Entities
{
    public class TicketLog : IAuditableEntity
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public TicketStatus? OldStatus { get; set; }
        public TicketStatus NewStatus { get; set; }
        public string? Notes { get; set; }
        public int ChangedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        // IAuditableEntity
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        // Navigation properties
        public MaintenanceTicket? Ticket { get; set; }
        public User? User { get; set; }
    }
}