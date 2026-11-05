using System;
using System.Collections.Generic;
using SmartFix.Domain.Common;
using SmartFix.Domain.Enums;

namespace SmartFix.Domain.Entities
{
    public class InventoryPart : IAuditableEntity
    {
        public int Id { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string PartCode { get; set; } = string.Empty;
        public PartCategory Category { get; set; } = PartCategory.Other;
        public string? CompatibleModels { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public int ReorderLevel { get; set; } = 5;
        public string? Supplier { get; set; }

        // IAuditableEntity
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        // Navigation properties
        public ICollection<TicketPart> TicketParts { get; set; } = new List<TicketPart>();
    }
}