using System;
using System.Data.SqlClient;
using FP_CS26_2025.Data;

class Program
{
    static void Main()
    {
        Console.WriteLine("Ensuring RoomRates table exists...");
        try
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RoomRates' AND xtype='U')
                    BEGIN
                        CREATE TABLE RoomRates (
                            RoomType NVARCHAR(100) PRIMARY KEY,
                            PricePerNight DECIMAL(18, 2),
                            Status NVARCHAR(50),
                            CapacityInfo NVARCHAR(255)
                        );
                        
                        INSERT INTO RoomRates (RoomType, PricePerNight, Status, CapacityInfo) VALUES 
                        ('Single Room', 100.00, 'Available', '1 Adult'),
                        ('Double Room', 150.00, 'Available', '2 Adults'),
                        ('Suite', 300.00, 'Available', '2 Adults, 2 Children');
                    END
                ";
                
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Table check completed.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
