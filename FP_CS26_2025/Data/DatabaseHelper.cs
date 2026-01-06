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
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Schema Update Error: " + ex.Message);
                }
            }
        }
    }
}
