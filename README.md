# 🛠️ SmartFix – Smart Device Maintenance Management System

> An enterprise-grade, Full-Stack .NET 8 solution designed to streamline smartphone and smart device repair workshop operations. Built with **Clean Architecture**, **ASP.NET Core (MVC & Web API)**, **Oracle Database mapping via EF Core**, and a companion **Flutter** mobile client.

---

## 🏗️ Architecture & Design Principles

SmartFix follows the **N-Tier Clean Architecture** pattern to ensure modularity, maintainability, and testability:
- **`SmartFix.Domain`:** Pure C# POCO entities (`MaintenanceTicket`, `Device`, `User`, `Invoice`, etc.) and Enums (`TicketStatus`, `Priority`, `PaymentStatus`) implementing `IAuditableEntity` for audit logging.
- **`SmartFix.Application`:** Core business logic, services (`TicketService`, `AuthService`), and interfaces (`ITicketService`, `IAuthService`).
- **`SmartFix.Infrastructur`:** EF Core `SmartFixDbContext`, Generic Repository pattern, and Unit of Work implementation.
- **`SmartFix.API`:** RESTful endpoints for mobile clients (Flutter) utilizing DTOs to prevent over-posting vulnerabilities.
- **`SmartFix.MVC`:** Role-based administrative dashboard supporting Cookie Authentication and RTL Arabic layout.
- **`SmartFix.Tests`:** Comprehensive Unit Tests using **xUnit** and EF Core In-Memory database.

---

## 🚀 Key Features

1. **Secure Authentication & Authorization:** Custom secure password hashing using ASP.NET Core `PasswordHasher<User>` and Cookie-based session handling.
2. **Maintenance Ticket Lifecycle:** Automated ticket numbering generation (`TCK-YYYY-XXXX`), priority assignment, and technician allocation.
3. **Audit Trail Logging:** Automated event logging (`TicketLogs`) tracking every status transition along with user stamps and timestamps.
4. **Inventory & Invoice Management:** Stock tracking for spare parts (`InventoryParts`) with strict validation to prevent negative inventory, coupled with automated invoice generation (`Invoices`).
5. **Mobile Readiness:** Dedicated REST API controllers designed for seamless integration with the **Flutter** mobile application.

---

## 🛠️ Tech Stack

- **Backend:** C# 12, ASP.NET Core 8.0 (MVC & Web API)
- **Database:** Oracle Database (EF Core configured with explicit table/column mappings)
- **Frontend / Mobile:** Bootstrap 5 (RTL support), Razor Views, Flutter (Dart)
- **Testing:** xUnit, EF Core InMemory
- **Security:** Password Hashing, Cookie Authentication, DTO protection

---

## 🏃‍♂️ Quick Start

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Bader9BN/SmartFix.git
   ```
2. **Build the solution:**
   ```bash
   dotnet build SmartFix/SmartFix.slnx
   ```
3. **Run Unit Tests:**
   ```bash
   dotnet test SmartFix/SmartFix.slnx
   ```
4. **Run the MVC Dashboard:**
   ```bash
   dotnet run --project SmartFix/SmartFix.MVC/SmartFix.MVC.csproj
   ```
5. **Run the Web API & Swagger:**
   ```bash
   dotnet run --project SmartFix/SmartFix.API/SmartFix.API.csproj
   ```
   *(Navigate to `http://localhost:[port]/swagger` to test endpoints).*

---

## 👨‍💻 Author
**Bader Al Harbi**  
Junior Full Stack .NET Developer  
[GitHub Profile](https://github.com/Bader9BN)
