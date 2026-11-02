using System;
using System.Collections.Generic;

namespace SmartFix.Domain.Entities
{
    public class InventoryPart
    {
        public int Id { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string PartCode { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public ICollection<TicketPart> TicketParts { get; set; } = new List<TicketPart>();
    }
}
