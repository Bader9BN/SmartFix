using System;
using System.Collections.Generic;

namespace SmartFix.Domain.Entities
{
    public class MaintenanceTicket
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public int DeviceId { get; set; }
        public int CustomerId { get; set; }
        public int? TechnicianId { get; set; }
        public string IssueDescription { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending"; // Pending, InProgress, WaitingForParts, Repaired, Delivered, Cancelled
        public string Priority { get; set; } = "Medium"; // Low, Medium, High, Urgent
        public decimal? EstimatedCost { get; set; }
        public decimal? FinalCost { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Device? Device { get; set; }
        public User? Customer { get; set; }
        public User? Technician { get; set; }
        public ICollection<TicketLog> Logs { get; set; } = new List<TicketLog>();
        public ICollection<TicketPart> TicketParts { get; set; } = new List<TicketPart>();
        public Invoice? Invoice { get; set; }
    }
}
