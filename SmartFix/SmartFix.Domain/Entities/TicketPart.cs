using System;

namespace SmartFix.Domain.Entities
{
    public class TicketPart
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int PartId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal UnitPriceAtTime { get; set; }
        public DateTime UsedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public MaintenanceTicket? Ticket { get; set; }
        public InventoryPart? Part { get; set; }
    }
}