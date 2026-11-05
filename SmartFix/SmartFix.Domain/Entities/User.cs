using System;
using System.Collections.Generic;
using SmartFix.Domain.Common;

namespace SmartFix.Domain.Entities
{
    public class User : IAuditableEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer"; // Admin, Technician, Customer
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;

        // IAuditableEntity
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        // Navigation properties
        public ICollection<Device> Devices { get; set; } = new List<Device>();
        public ICollection<MaintenanceTicket> CustomerTickets { get; set; } = new List<MaintenanceTicket>();
        public ICollection<MaintenanceTicket> TechnicianTickets { get; set; } = new List<MaintenanceTicket>();
    }
}
