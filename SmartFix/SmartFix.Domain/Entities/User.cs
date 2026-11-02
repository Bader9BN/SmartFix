using System;
using System.Collections.Generic;

namespace SmartFix.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer"; // Admin, Technician, Customer
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Device> Devices { get; set; } = new List<Device>();
        public ICollection<MaintenanceTicket> CustomerTickets { get; set; } = new List<MaintenanceTicket>();
        public ICollection<MaintenanceTicket> TechnicianTickets { get; set; } = new List<MaintenanceTicket>();
    }
}
