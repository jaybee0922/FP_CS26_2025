USE GrandNexusDB;
GO

-- 1. Ensure Reservations Table Exists
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Reservations')
BEGIN
    CREATE TABLE Reservations (
        ReservationID INT IDENTITY(1,1) PRIMARY KEY,
        RoomNumber INT NOT NULL,
        GuestID INT NOT NULL,
        CheckInDate DATETIME NOT NULL,
        CheckOutDate DATETIME NOT NULL,
        Status NVARCHAR(50) DEFAULT 'Checked In',
        TotalAmount DECIMAL(18, 2) NOT NULL,
        CreatedAt DATETIME DEFAULT GETDATE(),
        FOREIGN KEY (RoomNumber) REFERENCES Rooms(RoomNumber),
        FOREIGN KEY (GuestID) REFERENCES Users(UserID) -- Assuming Users table serves as Guests or linked
    );
END
GO

-- 2. Ensure Payments Table Exists
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Payments')
BEGIN
    CREATE TABLE Payments (
        PaymentID INT IDENTITY(1,1) PRIMARY KEY,
        ReservationID INT NOT NULL,
        PaymentDate DATETIME DEFAULT GETDATE(),
        Amount DECIMAL(18, 2) NOT NULL,
        PaymentMethod NVARCHAR(50) NOT NULL, -- 'Credit Card', 'Cash', 'Online'
        TransactionStatus NVARCHAR(50) DEFAULT 'Completed',
        FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID)
    );
END
GO

-- 3. Seed Data if Empty
IF NOT EXISTS (SELECT TOP 1 * FROM Reservations)
BEGIN
    -- Need valid RoomNumbers and UserIDs. Assuming RoomNumber 101-115 exists from previous context, and UserID 1 exists.
    -- Insert Dummy Reservations
    INSERT INTO Reservations (RoomNumber, GuestID, CheckInDate, CheckOutDate, Status, TotalAmount)
    VALUES 
    (101, 1, DATEADD(day, -30, GETDATE()), DATEADD(day, -25, GETDATE()), 'Checked Out', 500.00),
    (102, 1, DATEADD(day, -28, GETDATE()), DATEADD(day, -26, GETDATE()), 'Checked Out', 300.00),
    (103, 1, DATEADD(day, -14, GETDATE()), DATEADD(day, -10, GETDATE()), 'Checked Out', 600.00),
    (104, 1, DATEADD(day, -7, GETDATE()), DATEADD(day, -5, GETDATE()), 'Checked Out', 350.00),
    (105, 1, DATEADD(day, -2, GETDATE()), DATEADD(day, 2, GETDATE()), 'Checked In', 400.00);
END
GO

IF NOT EXISTS (SELECT TOP 1 * FROM Payments)
BEGIN
    -- Insert Dummy Payments linked to the above Reservations (IDs 1 to 5 likely)
    -- We can use nested query or just assume identity starts at 1 if newly created.
    -- Safer to use variables or just simple random inserts if acceptable.
    
    INSERT INTO Payments (ReservationID, PaymentDate, Amount, PaymentMethod)
    SELECT TOP 1 ReservationID, DATEADD(day, -25, GETDATE()), 500.00, 'Credit Card' FROM Reservations WHERE TotalAmount = 500.00;

    INSERT INTO Payments (ReservationID, PaymentDate, Amount, PaymentMethod)
    SELECT TOP 1 ReservationID, DATEADD(day, -26, GETDATE()), 300.00, 'Cash' FROM Reservations WHERE TotalAmount = 300.00;

    INSERT INTO Payments (ReservationID, PaymentDate, Amount, PaymentMethod)
    SELECT TOP 1 ReservationID, DATEADD(day, -10, GETDATE()), 600.00, 'Online' FROM Reservations WHERE TotalAmount = 600.00;

    INSERT INTO Payments (ReservationID, PaymentDate, Amount, PaymentMethod)
    SELECT TOP 1 ReservationID, DATEADD(day, -5, GETDATE()), 350.00, 'Credit Card' FROM Reservations WHERE TotalAmount = 350.00;

    INSERT INTO Payments (ReservationID, PaymentDate, Amount, PaymentMethod)
    SELECT TOP 1 ReservationID, GETDATE(), 100.00, 'Cash' FROM Reservations WHERE TotalAmount = 400.00; -- Partial payment
END
GO
