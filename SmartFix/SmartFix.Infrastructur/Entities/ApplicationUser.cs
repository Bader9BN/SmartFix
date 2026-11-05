using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using SmartFix.Domain.Common;
using SmartFix.Domain.Entities;

namespace SmartFix.Infrastructur.Entities
{
    public class ApplicationUser : IdentityUser<int>, IAuditableEntity
    {
        public string Role { get; set; } = "Customer"; // Admin, Technician, Customer
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
