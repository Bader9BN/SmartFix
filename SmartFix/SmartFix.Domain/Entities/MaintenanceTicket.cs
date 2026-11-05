using System;
using System.Collections.Generic;
using SmartFix.Domain.Common;
using SmartFix.Domain.Enums;

namespace SmartFix.Domain.Entities
{
    public class MaintenanceTicket : IAuditableEntity
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; } = string.Empty;
        public int DeviceId { get; set; }
        public int CustomerId { get; set; }
        public int? TechnicianId { get; set; }
        public string IssueDescription { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.Pending;
        public Priority Priority { get; set; } = Priority.Medium;
        public string? Diagnosis { get; set; }
        public string? Solution { get; set; }
        public string? CustomerNotes { get; set; }
        public decimal? EstimatedCost { get; set; }
        public decimal? FinalCost { get; set; }
        public DateTime? EstimatedCompletion { get; set; }
        public DateTime? ActualCompletion { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // IAuditableEntity
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        // Navigation properties
        public Device? Device { get; set; }
        public User? Customer { get; set; }
        public User? Technician { get; set; }
        public ICollection<TicketLog> Logs { get; set; } = new List<TicketLog>();
        public ICollection<TicketPart> TicketParts { get; set; } = new List<TicketPart>();
        public Invoice? Invoice { get; set; }
    }
}