using System;
using SmartFix.Domain.Common;
using SmartFix.Domain.Enums;

namespace SmartFix.Domain.Entities
{
    public class Invoice : IAuditableEntity
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public decimal LaborCost { get; set; }
        public decimal PartsCost { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? PaidAt { get; set; }

        // IAuditableEntity
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        // Navigation property
        public MaintenanceTicket? Ticket { get; set; }
    }
}