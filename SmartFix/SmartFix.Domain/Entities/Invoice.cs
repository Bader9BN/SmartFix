using System;

namespace SmartFix.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; } = "Unpaid"; // Unpaid, Paid, Refunded
        public string? PaymentMethod { get; set; }                 // Cash, CreditCard, ApplePay
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public MaintenanceTicket? Ticket { get; set; }
    }
}
