USE master;
GO

-- 1. Create Database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'GrandNexusDB')
BEGIN
    CREATE DATABASE GrandNexusDB;
    PRINT 'Database GrandNexusDB created successfully.';
END
GO

USE GrandNexusDB;
GO

-- 2. Drop tables if they exist (Reverse order of dependencies)
IF OBJECT_ID('dbo.Payments', 'U') IS NOT NULL DROP TABLE dbo.Payments;
IF OBJECT_ID('dbo.Reservations', 'U') IS NOT NULL DROP TABLE dbo.Reservations;
IF OBJECT_ID('dbo.Guests', 'U') IS NOT NULL DROP TABLE dbo.Guests;
IF OBJECT_ID('dbo.Rooms', 'U') IS NOT NULL DROP TABLE dbo.Rooms;
IF OBJECT_ID('dbo.RoomTypes', 'U') IS NOT NULL DROP TABLE dbo.RoomTypes;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
GO

-- 3. Create Tables

-- Users Table
CREATE TABLE Users (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL, -- Storing as plain text per request, should be hashed in production
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('SuperAdmin', 'FrontDesk')),
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- RoomTypes Table (Normalization: Removing transitive dependency)
CREATE TABLE RoomTypes (
    RoomTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(50) NOT NULL UNIQUE, -- 'Standard', 'Deluxe', 'Suite'
    BasePrice DECIMAL(10, 2) NOT NULL,
    Capacity INT NOT NULL
);

-- Rooms Table
CREATE TABLE Rooms (
    RoomNumber NVARCHAR(10) NOT NULL PRIMARY KEY, -- Using string ID "101", "A-1"
    RoomTypeID INT NOT NULL FOREIGN KEY REFERENCES RoomTypes(RoomTypeID),
    Floor INT NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Available' CHECK (Status IN ('Available', 'Occupied', 'Maintenance'))
);

-- Guests Table
CREATE TABLE Guests (
    GuestID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Reservations Table
CREATE TABLE Reservations (
    ReservationID NVARCHAR(50) NOT NULL PRIMARY KEY, -- "RES-XXXXX"
    GuestID INT NOT NULL FOREIGN KEY REFERENCES Guests(GuestID),
    RoomNumber NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Rooms(RoomNumber),
    CheckInDate DATETIME NOT NULL,
    CheckOutDate DATETIME NOT NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Pending', 'CheckedIn', 'CheckedOut', 'Cancelled')),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Payments Table
CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    ReservationID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Reservations(ReservationID),
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATETIME DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50) -- 'Cash', 'Card', etc.
);
GO

-- 4. Seed Data

-- Validating Users
INSERT INTO Users (Username, Password, Role)
VALUES 
('grandnexusadmin', 'grandnexusadmin123', 'SuperAdmin'),
('grandnexusfrontdesk', 'grandnexusfrontdesk123', 'FrontDesk');

-- Validating Room Types
INSERT INTO RoomTypes (TypeName, BasePrice, Capacity)
VALUES 
('Standard', 1200.00, 2),
('Deluxe', 1800.00, 3),
('Suite', 2500.00, 4);

-- Validating Rooms
-- Standard Rooms (1st Floor)
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES ('101', 1, 1, 'Available');
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES ('102', 1, 1, 'Available');
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES ('103', 1, 1, 'Occupied');
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES ('104', 1, 1, 'Available');
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES ('105', 1, 1, 'Maintenance');

-- Deluxe Rooms (2nd Floor)
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES ('201', 2, 2, 'Available');
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES ('202', 2, 2, 'Available');
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES ('203', 2, 2, 'Occupied');

-- Suites (3rd Floor)
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES ('301', 3, 3, 'Available');
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status) VALUES ('302', 3, 3, 'Available');

-- Sample Guests
INSERT INTO Guests (FirstName, LastName, Email, PhoneNumber)
VALUES 
('John', 'Doe', 'john.doe@example.com', '555-0101'),
('Jane', 'Smith', 'jane.smith@example.com', '555-0102');

-- Sample Reservations
-- Ensure Room 103 and 203 are reflected as Occupied here if they are checked in
INSERT INTO Reservations (ReservationID, GuestID, RoomNumber, CheckInDate, CheckOutDate, Status, TotalAmount)
VALUES 
('RES-00001', 1, '103', GETDATE(), DATEADD(day, 3, GETDATE()), 'CheckedIn', 3600.00), -- 3 days * 1200
('RES-00002', 2, '203', GETDATE(), DATEADD(day, 2, GETDATE()), 'CheckedIn', 3600.00); -- 2 days * 1800

PRINT 'Tables created and seed data inserted successfully.';
GO
