# Hotel Management System (FP_CS26_2025)

A C# WinForms application for hotel management, featuring a modular architecture and clean design.

## 🏢 Modules

### 🛎️ Front Desk / Receptionist (MVC Implementation)
The Front Desk module has been refactored using the **Model-View-Controller (MVC)** pattern to ensure scalability and adherence to SOLID principles.

#### **Key Features**
- **Reservation Management**: Process and view guest bookings.
- **Check-In/Check-Out**: Automated room status updates and guest processing.
- **Room Assignment**: Real-time tracking of room availability and types.
- **Billing**: Automatic bill generation based on room type and stay duration.

#### **Technical Architecture**
- **Models**: `IRoom`, `Room`, `StandardRoom`, `SuiteRoom`, `Guest`, `Reservation`, `Bill`.
- **Controllers**: `FrontDeskController` handles all business logic.
- **Services**: `IHotelDataService` provides abstraction for data persistence (currently using `InMemoryHotelService`).
- **Namespace**: `FP_CS26_2025.FrontDesk_MVC`

---

### 👑 Super Admin Dashboard
*Note: This module is currently under development.*
- Manages room pricing, policies, and overall hotel statistics.
- User management and role assignment.

---

## 🛠️ Project Structure

FP_CS26_2025/
├── FrontDesk_ReceptionistAccount/  # Front Desk Module
│   ├── Controllers/               # FrontDeskController.cs
│   ├── Models/                    # IRoom.cs, Room.cs, Models.cs
│   ├── Services/                  # HotelDataService.cs
│   ├── Hotel_FrontDeskDashboard.cs # Main Dashboard View
│   └── FrontDesk_Views.cs         # Reusable UI Panels (CheckIn, Billing, etc.)
├── HotelManager_AdminDashboard/    # Admin UI Components
├── LoginFormDesign/               # Custom UI Controls for Login
├── Program.cs                     # Application Entry Point
└── Form1.cs                       # Role Selection & Login Form


## 🚀 Getting Started

1.  Open the solution in Visual Studio.
2.  Ensure the target framework is set to **.NET Framework 4.8**.
3.  Build the project (`dotnet build`).
4.  Run the application.
5.  Select **"Front Desk"** in the login role to view the refactored MVC dashboard.

## ⚖️ OOP & SOLID Principles Applied
- **Encapsulation**: Private fields and public properties in Models.
- **Inheritance**: Specialized room types inheriting from a base `Room` class.
- **Polymorphism**: Overridden `CalculateTotalPrice` and `GetDetails` methods.
- **Abstraction**: Use of interfaces (`IRoom`, `IHotelDataService`).
- **Dependency Inversion**: Controllers depend on interfaces, not concrete implementations.
