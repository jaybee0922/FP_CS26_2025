USE master;
GO
-- 1. Create Database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'GrandNexusDB')
BEGIN
    CREATE DATABASE GrandNexusDB;
END
GO
USE GrandNexusDB;
GO

-- 2. Drop tables in correct order to avoid FK constraint errors
IF OBJECT_ID('dbo.BillItems', 'U') IS NOT NULL DROP TABLE dbo.BillItems; -- [NEW - Added for itemized billing]
IF OBJECT_ID('dbo.Payments', 'U') IS NOT NULL DROP TABLE dbo.Payments;
IF OBJECT_ID('dbo.Reservations', 'U') IS NOT NULL DROP TABLE dbo.Reservations;
IF OBJECT_ID('dbo.PromoCodes', 'U') IS NOT NULL DROP TABLE dbo.PromoCodes;
IF OBJECT_ID('dbo.Guests', 'U') IS NOT NULL DROP TABLE dbo.Guests;
IF OBJECT_ID('dbo.Rooms', 'U') IS NOT NULL DROP TABLE dbo.Rooms;
IF OBJECT_ID('dbo.RoomTypes', 'U') IS NOT NULL DROP TABLE dbo.RoomTypes;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
IF OBJECT_ID('dbo.MonthlyReports', 'U') IS NOT NULL DROP TABLE dbo.MonthlyReports;
GO

-- 3. Create Tables

-- Users Table (Identity Management)
CREATE TABLE Users (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    EmployeeId NVARCHAR(10) NOT NULL UNIQUE,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    FirstName NVARCHAR(50) NOT NULL,
    MiddleName NVARCHAR(50),
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100),
    [Password] NVARCHAR(255) NOT NULL,
    Birthday DATETIME,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('SuperAdmin', 'FrontDesk')),
    IsActive BIT DEFAULT 1,
    DateAdded DATETIME DEFAULT GETDATE(),
    LastUpdated DATETIME DEFAULT GETDATE(),
    LastActive DATETIME
);

-- PromoCodes Table (For managing discount vouchers)
CREATE TABLE PromoCodes (
    PromoID INT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    DiscountType NVARCHAR(20) NOT NULL CHECK (DiscountType IN ('Percentage', 'Fixed')),
    DiscountValue DECIMAL(10, 2) NOT NULL,
    ExpiryDate DATETIME NOT NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- RoomTypes Table (Category pricing and capacity)
CREATE TABLE RoomTypes (
    RoomTypeID INT IDENTITY(1,1) PRIMARY KEY,
    TypeName NVARCHAR(50) NOT NULL UNIQUE,
    BasePrice DECIMAL(10, 2) NOT NULL,
    Capacity INT NOT NULL,
    Description NVARCHAR(MAX), 
    ImageFilename NVARCHAR(255) 
);

-- Rooms Table (Physical Room Inventory)
-- [UPDATED - Added BedConfig and ViewType columns]
CREATE TABLE Rooms (
    RoomNumber NVARCHAR(10) NOT NULL PRIMARY KEY,
    RoomTypeID INT NOT NULL FOREIGN KEY REFERENCES RoomTypes(RoomTypeID),
    Floor INT NOT NULL,
    Status NVARCHAR(30) NOT NULL DEFAULT 'Available' 
        CHECK (Status IN ('Available', 'Occupied', 'Reserved', 'Under Maintenance', 'Cleaning', 'Out of Service', 'ReadyForCheckIn')),
    BedConfig NVARCHAR(50) DEFAULT 'Standard', -- [NEW - Bed configuration: King, Queen, Twin, etc.]
    ViewType NVARCHAR(50) DEFAULT 'City View'  -- [NEW - View type: City View, Garden View, Sea View, etc.]
);

-- Guests Table
-- [UPDATED - Added identity and classification fields]
CREATE TABLE Guests (
    GuestID INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100),
    PhoneNumber NVARCHAR(20),
    IdType NVARCHAR(50),              -- [NEW - ID Type: Passport, Driver's License, etc.]
    IdNumber NVARCHAR(100),            -- [NEW - ID Number]
    Nationality NVARCHAR(100),         -- [NEW - Guest nationality]
    GuestType NVARCHAR(50) DEFAULT 'Regular', -- [NEW - Classification: Regular, VIP, Corporate]
    CreatedDate DATETIME DEFAULT GETDATE()
);

-- Reservations Table (Updated with Promo Tracking)
CREATE TABLE Reservations (
    ReservationID NVARCHAR(50) NOT NULL PRIMARY KEY,
    GuestID INT NOT NULL FOREIGN KEY REFERENCES Guests(GuestID),
    RoomNumber NVARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Rooms(RoomNumber),
    CheckInDate DATETIME NOT NULL,
    CheckOutDate DATETIME NOT NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Pending', 'Approved', 'CheckedIn', 'CheckedOut', 'Cancelled')),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    RoomType NVARCHAR(50), 
    NumAdults INT,
    NumChildren INT,
    NumRooms INT,
    PromoCode NVARCHAR(50),
    DiscountAmount DECIMAL(10, 2) DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Payments Table
CREATE TABLE Payments (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    ReservationID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Reservations(ReservationID),
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentDate DATETIME DEFAULT GETDATE(),
    PaymentMethod NVARCHAR(50)
);

-- [NEW] BillItems Table (For itemized billing - Mini-bar, Late Checkout, etc.)
CREATE TABLE BillItems (
    ItemID INT IDENTITY(1,1) PRIMARY KEY,
    ReservationID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES Reservations(ReservationID),
    Description NVARCHAR(255) NOT NULL,
    Quantity INT DEFAULT 1,
    UnitPrice DECIMAL(18, 2) NOT NULL,
    TotalPrice DECIMAL(18, 2) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- MonthlyReports Table (Archived Reports)
CREATE TABLE MonthlyReports (
    ReportID INT IDENTITY(1,1) PRIMARY KEY,
    [Month] INT NOT NULL,
    [Year] INT NOT NULL,
    TotalRevenue DECIMAL(18, 2) NOT NULL,
    TransactionCount INT NOT NULL,
    GeneratedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT UQ_Report_MonthYear UNIQUE ([Month], [Year]) 
);
GO

-- 4. Seed Initial Data
INSERT INTO Users (EmployeeId, Username, FirstName, LastName, [Password], Role)
VALUES 
('001', 'grandnexusadmin', 'Admin', 'User', 'grandnexusadmin123', 'SuperAdmin'),
('002', 'grandnexusfrontdesk', 'FrontDesk', 'User', 'grandnexusfrontdesk123', 'FrontDesk');

-- Seed Initial Promo Code
INSERT INTO PromoCodes (Code, DiscountType, DiscountValue, ExpiryDate, IsActive)
VALUES ('WELCOME20', 'Percentage', 20.00, '2026-12-31', 1);

-- Full 15 Room Types
INSERT INTO RoomTypes (TypeName, BasePrice, Capacity, Description, ImageFilename)
VALUES 
('Celebrity Suite', 1200.00, 4, 'Experience the ultimate luxury in our most prestigious suite.', 'Celebrity Suite'),
('Club Room', 450.00, 2, 'Modern elegance meets comfort with exclusive lounge access.', 'Club Room'),
('Deluxe King', 250.00, 2, 'A spacious retreat with a plush King-sized bed.', 'Deluxe King'),
('Deluxe Twin', 250.00, 2, 'Comfortably designed with twin beds for shared comfort.', 'Deluxe Twin'),
('Executive Suite Double', 650.00, 4, 'Two double beds and a separate living area.', 'Executive Suite Double'),
('Executive Suite King', 700.00, 2, 'Premier suite for executives with a King bed and workspace.', 'Executive Suite King'),
('Garden Suite', 400.00, 2, 'Tranquil views of our lush tropical gardens.', 'Garden Suite'),
('Grand Deluxe Family', 550.00, 5, 'Multiple bedding options and child-friendly features.', 'Grand Deluxe Family'),
('Grand Deluxe King', 350.00, 2, 'Elevated stay with extra space and stunning city views.', 'Grand Deluxe King'),
('Grand Deluxe Twin', 350.00, 2, 'Spacious and modern for those who value style.', 'Grand Deluxe Twin'),
('Junior Suite', 500.00, 2, 'A seamless blend of living and sleeping areas.', 'Junior Suite'),
('Man Bay Suite', 850.00, 2, 'Breathtaking views of the iconic Manila Bay sunset.', 'Manila Bay Suite'),
('Premium Suite Double', 480.00, 4, 'Excellent choice for groups with two double beds.', 'Premium Suite Double'),
('Premium Suite King', 520.00, 2, 'Unmatched comfort with a dedicated workspace.', 'Premium Suite King'),
('Presidential Suite', 2500.00, 6, 'The pinnacle of opulence with 24/7 butler service.', 'Presidential Suite');

-- Seed 15 Individual Rooms assigned to Floors
-- [UPDATED - Added BedConfig and ViewType values for demonstration]
INSERT INTO Rooms (RoomNumber, RoomTypeID, Floor, Status, BedConfig, ViewType) VALUES 
('101', 1, 1, 'Available', 'King Bed', 'City View'), 
('102', 2, 1, 'Available', 'Queen Bed', 'Garden View'), 
('103', 3, 1, 'Available', 'King Bed', 'City View'), 
('201', 4, 2, 'Available', 'Twin Beds', 'City View'), 
('202', 5, 2, 'Available', 'Two Double Beds', 'Sea View'), 
('203', 6, 2, 'Available', 'King Bed', 'Sea View'), 
('301', 7, 3, 'Available', 'Queen Bed', 'Garden View'), 
('302', 8, 3, 'Available', 'Multiple Beds', 'Garden View'), 
('401', 9, 4, 'Available', 'King Bed', 'City View'), 
('402', 10, 4, 'Available', 'Twin Beds', 'City View'),
('501', 11, 5, 'Available', 'King Bed', 'Sea View'), 
('502', 12, 5, 'Available', 'King Bed', 'Sea View'),
('601', 13, 6, 'Available', 'Two Double Beds', 'City View'), 
('602', 14, 6, 'Available', 'King Bed', 'City View'),
('701', 15, 7, 'Available', 'King Bed', 'Panoramic View'); 

PRINT 'GrandNexusDB successfully created and seeded with all updates:';
PRINT '  - Room metadata (BedConfig, ViewType)';
PRINT '  - Guest details (IdType, IdNumber, Nationality, GuestType)';
PRINT '  - BillItems table for itemized billing';
GO
