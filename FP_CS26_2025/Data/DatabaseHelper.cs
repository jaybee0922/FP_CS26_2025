using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FP_CS26_2025.Data
{
    public static class DatabaseHelper
    {
        private static string _cachedConnectionString = null;

        public static string ConnectionString
        {
            get
            {
                if (_cachedConnectionString == null)
                {
                    _cachedConnectionString = GetWorkingConnectionString();
                }
                return _cachedConnectionString;
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        private static string GetWorkingConnectionString()
        {
            // 1. Get the configured string from App.config as the primary candidate
            string configConnString = ConfigurationManager.ConnectionStrings["GrandNexusDB"].ConnectionString;
            
            // Extract the base parts (User ID, Password, Initial Catalog) to rebuild strings
            // Note: A more robust parsing might be needed, but simple string replacement works for this specific format
            // Assumes format: Data Source=...;Initial Catalog=...;...
            
            var sourcesToCheck = new System.Collections.Generic.List<string>
            {
                configConnString, // Try what's in config first
                configConnString.Replace("Data Source=.\\SQLEXPRESS01;", "Data Source=.\\SQLEXPRESS;").Replace("Data Source=.\\SQLEXPRESS01", "Data Source=.\\SQLEXPRESS"),
                configConnString.Replace("Data Source=.\\SQLEXPRESS01;", "Data Source=.;").Replace("Data Source=.\\SQLEXPRESS01", "Data Source=."),
                configConnString.Replace("Data Source=.\\SQLEXPRESS01;", "Data Source=(localdb)\\MSSQLLocalDB;").Replace("Data Source=.\\SQLEXPRESS01", "Data Source=(localdb)\\MSSQLLocalDB")
            };

            // Also handle if the config string was NOT SQLEXPRESS01 originally
            if (!sourcesToCheck.Contains(configConnString.Replace(GetDataSource(configConnString), "Data Source=.\\SQLEXPRESS01")))
                 sourcesToCheck.Add(configConnString.Replace(GetDataSource(configConnString), "Data Source=.\\SQLEXPRESS01"));


            foreach (var connString in sourcesToCheck)
            {
                if (TryConnect(connString))
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("Successfully connected to: {0}", connString));
                    return connString;
                }
            }

            // If all fail, return the original one so the error bubbles up normally
            return configConnString;
        }

        private static string GetDataSource(string connString)
        {
            // Simple helper to extract Data Source=...; part
            int start = connString.IndexOf("Data Source=");
            if (start == -1) return "";
            int end = connString.IndexOf(";", start);
            if (end == -1) return connString.Substring(start);
            return connString.Substring(start, end - start);
        }

        private static bool TryConnect(string connString)
        {
            try
            {
                using (var conn = new SqlConnection(connString))
                {
                    conn.Open();
                    return true;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Failed to connect to: {0}. Error: {1}", connString, ex.Message));
                return false;
            }
        }

        public static bool ValidateUser(string username, string password, string role)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT Count(1) FROM Users WHERE Username = @Username AND Password = @Password AND Role = @Role";
                    using (var cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password); 
                        cmd.Parameters.AddWithValue("@Role", role);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count == 1;
                    }
                }
                catch (Exception ex)
                {
                    // Log exception appropriately
                    System.Diagnostics.Debug.WriteLine("Database Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static void EnsureSchema()
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    // Check if Description column exists in RoomTypes
                    var checkCmd = new SqlCommand("SELECT COL_LENGTH('RoomTypes', 'Description')", conn);
                    if (checkCmd.ExecuteScalar() == DBNull.Value)
                    {
                        var alterCmd = new SqlCommand("ALTER TABLE RoomTypes ADD Description NVARCHAR(MAX)", conn);
                        alterCmd.ExecuteNonQuery();
                    }

                    // Check if ImageFilename column exists in RoomTypes
                    var checkImgCmd = new SqlCommand("SELECT COL_LENGTH('RoomTypes', 'ImageFilename')", conn);
                    if (checkImgCmd.ExecuteScalar() == DBNull.Value)
                    {
                        // Add column
                        var alterImgCmd = new SqlCommand("ALTER TABLE RoomTypes ADD ImageFilename NVARCHAR(255)", conn);
                        alterImgCmd.ExecuteNonQuery();

                        // Backfill: Initially set ImageFilename = TypeName for existing rows
                        var updateImgCmd = new SqlCommand("UPDATE RoomTypes SET ImageFilename = TypeName WHERE ImageFilename IS NULL", conn);
                        updateImgCmd.ExecuteNonQuery();
                    }

                    // Check if MonthlyReports table exists
                    var checkTableCmd = new SqlCommand("SELECT OBJECT_ID('dbo.MonthlyReports', 'U')", conn);
                    if (checkTableCmd.ExecuteScalar() == DBNull.Value)
                    {
                        const string createTableQuery = @"
                            CREATE TABLE MonthlyReports (
                                ReportID INT IDENTITY(1,1) PRIMARY KEY,
                                [Month] INT NOT NULL,
                                [Year] INT NOT NULL,
                                TotalRevenue DECIMAL(18, 2) NOT NULL,
                                TransactionCount INT NOT NULL,
                                GeneratedDate DATETIME DEFAULT GETDATE(),
                                CONSTRAINT UQ_Report_MonthYear UNIQUE ([Month], [Year])
                            )";
                        var createCmd = new SqlCommand(createTableQuery, conn);
                        createCmd.ExecuteNonQuery();
                    }

                    // Check if PromoCodes table exists
                    var checkPromoTableCmd = new SqlCommand("SELECT OBJECT_ID('dbo.PromoCodes', 'U')", conn);
                    if (checkPromoTableCmd.ExecuteScalar() == DBNull.Value)
                    {
                        const string createPromoTableQuery = @"
                            CREATE TABLE PromoCodes (
                                PromoID INT IDENTITY(1,1) PRIMARY KEY,
                                Code NVARCHAR(50) NOT NULL UNIQUE,
                                DiscountType NVARCHAR(20) NOT NULL, -- 'Percentage' or 'Fixed'
                                DiscountValue DECIMAL(18, 2) NOT NULL,
                                ExpiryDate DATETIME NOT NULL,
                                IsActive BIT DEFAULT 1,
                                CreatedAt DATETIME DEFAULT GETDATE()
                            )";
                        var createCmd = new SqlCommand(createPromoTableQuery, conn);
                        createCmd.ExecuteNonQuery();

                        // Add a seed promo code for testing
                        const string seedPromoQuery = @"
                            INSERT INTO PromoCodes (Code, DiscountType, DiscountValue, ExpiryDate)
                            VALUES ('WELCOME20', 'Percentage', 20.00, '2030-12-31')";
                        var seedCmd = new SqlCommand(seedPromoQuery, conn);
                        seedCmd.ExecuteNonQuery();
                    }

                    // Add columns to Reservations for promo tracking if they don't exist
                    var checkPromoCodeCmd = new SqlCommand("SELECT COL_LENGTH('Reservations', 'PromoCode')", conn);
                    if (checkPromoCodeCmd.ExecuteScalar() == DBNull.Value)
                    {
                        var alterPromoCodeCmd = new SqlCommand("ALTER TABLE Reservations ADD PromoCode NVARCHAR(50), DiscountAmount DECIMAL(18, 2) DEFAULT 0", conn);
                        alterPromoCodeCmd.ExecuteNonQuery();
                    }

                    // Add Room metadata columns (BedConfig, ViewType)
                    var checkBedConfigCmd = new SqlCommand("SELECT COL_LENGTH('Rooms', 'BedConfig')", conn);
                    if (checkBedConfigCmd.ExecuteScalar() == DBNull.Value)
                    {
                        var alterBedConfigCmd = new SqlCommand("ALTER TABLE Rooms ADD BedConfig NVARCHAR(50) DEFAULT 'Standard'", conn);
                        alterBedConfigCmd.ExecuteNonQuery();
                    }

                    var checkViewTypeCmd = new SqlCommand("SELECT COL_LENGTH('Rooms', 'ViewType')", conn);
                    if (checkViewTypeCmd.ExecuteScalar() == DBNull.Value)
                    {
                        var alterViewTypeCmd = new SqlCommand("ALTER TABLE Rooms ADD ViewType NVARCHAR(50) DEFAULT 'City View'", conn);
                        alterViewTypeCmd.ExecuteNonQuery();
                    }

                    // Add Guest detail columns (IdType, IdNumber, Nationality, GuestType)
                    var checkIdTypeCmd = new SqlCommand("SELECT COL_LENGTH('Guests', 'IdType')", conn);
                    if (checkIdTypeCmd.ExecuteScalar() == DBNull.Value)
                    {
                        var alterIdTypeCmd = new SqlCommand("ALTER TABLE Guests ADD IdType NVARCHAR(50), IdNumber NVARCHAR(100), Nationality NVARCHAR(100), GuestType NVARCHAR(50) DEFAULT 'Regular'", conn);
                        alterIdTypeCmd.ExecuteNonQuery();
                    }

                    // Create BillItems table for itemized billing
                    var checkBillItemsTableCmd = new SqlCommand("SELECT OBJECT_ID('dbo.BillItems', 'U')", conn);
                    if (checkBillItemsTableCmd.ExecuteScalar() == DBNull.Value)
                    {
                        const string createBillItemsTableQuery = @"
                            CREATE TABLE BillItems (
                                ItemID INT IDENTITY(1,1) PRIMARY KEY,
                                ReservationID NVARCHAR(50) NOT NULL,
                                Description NVARCHAR(255) NOT NULL,
                                Quantity INT DEFAULT 1,
                                UnitPrice DECIMAL(18, 2) NOT NULL,
                                TotalPrice DECIMAL(18, 2) NOT NULL,
                                CreatedAt DATETIME DEFAULT GETDATE(),
                                FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID)
                            )";
                        var createCmd = new SqlCommand(createBillItemsTableQuery, conn);
                        createCmd.ExecuteNonQuery();
                    }

                    // Update Room Status Constraint to allow new enum values
                    UpdateRoomStatusConstraint(conn);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Schema Update Error: " + ex.Message);
                }
            }
        }

        private static void UpdateRoomStatusConstraint(SqlConnection conn)
        {
            try
            {
                // Find existing constraint name
                string sqlFind = @"SELECT name FROM sys.check_constraints 
                                   WHERE parent_object_id = OBJECT_ID('Rooms') 
                                   AND parent_column_id = (SELECT column_id FROM sys.columns WHERE name = 'Status' AND object_id = OBJECT_ID('Rooms'))";

                using (var cmd = new SqlCommand(sqlFind, conn))
                {
                    var result = cmd.ExecuteScalar();
                    string constraintName = result as string;

                    // If existing constraint provides different logic or we just want to enforce 'CK_Rooms_Status_Final'
                    if (!string.IsNullOrEmpty(constraintName) && constraintName != "CK_Rooms_Status_Final")
                    {
                        // Drop old constraint
                        var dropCmd = new SqlCommand($"ALTER TABLE Rooms DROP CONSTRAINT [{constraintName}]", conn);
                        dropCmd.ExecuteNonQuery();
                        
                        // Add new corrected constraint
                        // Allowed values: Available, Occupied, Reserved, UnderMaintenance, OutOfService, Cleaning, ReadyForCheckIn
                        // Note: Using 'UnderMaintenance' (no space) matching the code fixes.
                        string addSql = @"ALTER TABLE Rooms ADD CONSTRAINT [CK_Rooms_Status_Final] CHECK (Status IN ('Available', 'Occupied', 'Reserved', 'UnderMaintenance', 'OutOfService', 'Cleaning', 'ReadyForCheckIn'))";
                        var addCmd = new SqlCommand(addSql, conn);
                        addCmd.ExecuteNonQuery();
                    }
                    else if (string.IsNullOrEmpty(constraintName))
                    {
                        // Add if missing
                        string addSql = @"ALTER TABLE Rooms ADD CONSTRAINT [CK_Rooms_Status_Final] CHECK (Status IN ('Available', 'Occupied', 'Reserved', 'UnderMaintenance', 'OutOfService', 'Cleaning', 'ReadyForCheckIn'))";
                        var addCmd = new SqlCommand(addSql, conn);
                        addCmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Constraint Update Warning: " + ex.Message);
            }
        }
    }
}
