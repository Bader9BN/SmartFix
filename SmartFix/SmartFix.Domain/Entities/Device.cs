using System;
using System.Collections.Generic;
using SmartFix.Domain.Common;

namespace SmartFix.Domain.Entities
{
    public class Device : IAuditableEntity
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public string? SerialNumber { get; set; }
        public string? Brand { get; set; }
        public int? AgeInMonths { get; set; }
        public string? WarrantyStatus { get; set; }
        public int CustomerId { get; set; }

        // IAuditableEntity
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        // Navigation properties
        public User? Customer { get; set; }
        public ICollection<MaintenanceTicket> Tickets { get; set; } = new List<MaintenanceTicket>();
    }
}