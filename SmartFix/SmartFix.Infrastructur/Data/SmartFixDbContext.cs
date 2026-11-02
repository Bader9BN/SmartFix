using Microsoft.EntityFrameworkCore;
using SmartFix.Domain.Entities;

namespace SmartFix.Infrastructur.Data
{
    public class SmartFixDbContext : DbContext
    {
        public SmartFixDbContext(DbContextOptions<SmartFixDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Device> Devices => Set<Device>();
        public DbSet<MaintenanceTicket> MaintenanceTickets => Set<MaintenanceTicket>();
        public DbSet<TicketLog> TicketLogs => Set<TicketLog>();
        public DbSet<InventoryPart> InventoryParts => Set<InventoryPart>();
        public DbSet<TicketPart> TicketParts => Set<TicketPart>();
        public DbSet<Invoice> Invoices => Set<Invoice>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تكوين الجداول والعلاقات لضمان التوافق مع أوراكل (Oracle Column & Table Mappings)
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Username).HasColumnName("USERNAME").IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).HasColumnName("EMAIL").IsRequired().HasMaxLength(150);
                entity.Property(e => e.PasswordHash).HasColumnName("PASSWORDHASH").IsRequired().HasMaxLength(500);
                entity.Property(e => e.Role).HasColumnName("ROLE").IsRequired().HasMaxLength(50);
                entity.Property(e => e.PhoneNumber).HasColumnName("PHONENUMBER").HasMaxLength(20);
                entity.Property(e => e.CreatedAt).HasColumnName("CREATEDAT");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("DEVICES");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.DeviceName).HasColumnName("DEVICENAME").IsRequired().HasMaxLength(100);
                entity.Property(e => e.DeviceType).HasColumnName("DEVICETYPE").IsRequired().HasMaxLength(50);
                entity.Property(e => e.SerialNumber).HasColumnName("SERIALNUMBER").HasMaxLength(100);
                entity.Property(e => e.CustomerId).HasColumnName("CUSTOMERID");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATEDAT");

                entity.HasOne(d => d.Customer)
                      .WithMany(u => u.Devices)
                      .HasForeignKey(d => d.CustomerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MaintenanceTicket>(entity =>
            {
                entity.ToTable("MAINTENANCETICKETS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.TicketNumber).HasColumnName("TICKETNUMBER").IsRequired().HasMaxLength(50);
                entity.Property(e => e.DeviceId).HasColumnName("DEVICEID");
                entity.Property(e => e.CustomerId).HasColumnName("CUSTOMERID");
                entity.Property(e => e.TechnicianId).HasColumnName("TECHNICIANID");
                entity.Property(e => e.IssueDescription).HasColumnName("ISSUEDESCRIPTION").IsRequired();
                entity.Property(e => e.Status).HasColumnName("STATUS").HasMaxLength(50);
                entity.Property(e => e.Priority).HasColumnName("PRIORITY").HasMaxLength(20);
                entity.Property(e => e.EstimatedCost).HasColumnName("ESTIMATEDCOST");
                entity.Property(e => e.FinalCost).HasColumnName("FINALCOST");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATEDAT");
                entity.Property(e => e.UpdatedAt).HasColumnName("UPDATEDAT");

                entity.HasOne(t => t.Device)
                      .WithMany(d => d.Tickets)
                      .HasForeignKey(t => t.DeviceId);

                entity.HasOne(t => t.Customer)
                      .WithMany(u => u.CustomerTickets)
                      .HasForeignKey(t => t.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(t => t.Technician)
                      .WithMany(u => u.TechnicianTickets)
                      .HasForeignKey(t => t.TechnicianId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<TicketLog>(entity =>
            {
                entity.ToTable("TICKETLOGS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.TicketId).HasColumnName("TICKETID");
                entity.Property(e => e.OldStatus).HasColumnName("OLDSTATUS").HasMaxLength(50);
                entity.Property(e => e.NewStatus).HasColumnName("NEWSTATUS").IsRequired().HasMaxLength(50);
                entity.Property(e => e.Notes).HasColumnName("NOTES").HasMaxLength(500);
                entity.Property(e => e.ChangedBy).HasColumnName("CHANGEDBY");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATEDAT");

                entity.HasOne(l => l.Ticket)
                      .WithMany(t => t.Logs)
                      .HasForeignKey(l => l.TicketId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<InventoryPart>(entity =>
            {
                entity.ToTable("INVENTORYPARTS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.PartName).HasColumnName("PARTNAME").IsRequired().HasMaxLength(100);
                entity.Property(e => e.PartCode).HasColumnName("PARTCODE").IsRequired().HasMaxLength(50);
                entity.Property(e => e.UnitPrice).HasColumnName("UNITPRICE");
                entity.Property(e => e.StockQuantity).HasColumnName("STOCKQUANTITY");
                entity.Property(e => e.CreatedAt).HasColumnName("CREATEDAT");
            });

            modelBuilder.Entity<TicketPart>(entity =>
            {
                entity.ToTable("TICKETPARTS");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.TicketId).HasColumnName("TICKETID");
                entity.Property(e => e.PartId).HasColumnName("PARTID");
                entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
                entity.Property(e => e.UnitPriceAtTime).HasColumnName("UNITPRICEATTIME");

                entity.HasOne(tp => tp.Ticket)
                      .WithMany(t => t.TicketParts)
                      .HasForeignKey(tp => tp.TicketId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(tp => tp.Part)
                      .WithMany(p => p.TicketParts)
                      .HasForeignKey(tp => tp.PartId);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("INVOICES");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.TicketId).HasColumnName("TICKETID");
                entity.Property(e => e.TotalAmount).HasColumnName("TOTALAMOUNT");
                entity.Property(e => e.PaymentStatus).HasColumnName("PAYMENTSTATUS").HasMaxLength(30);
                entity.Property(e => e.PaymentMethod).HasColumnName("PAYMENTMETHOD").HasMaxLength(30);
                entity.Property(e => e.IssuedAt).HasColumnName("ISSUEDAT");

                entity.HasOne(i => i.Ticket)
                      .WithOne(t => t.Invoice)
                      .HasForeignKey<Invoice>(i => i.TicketId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
