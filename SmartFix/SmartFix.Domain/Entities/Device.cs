using System;
using System.Collections.Generic;

namespace SmartFix.Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string DeviceType { get; set; } = string.Empty;
        public string? SerialNumber { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public User? Customer { get; set; }
        public ICollection<MaintenanceTicket> Tickets { get; set; } = new List<MaintenanceTicket>();
    }
}
