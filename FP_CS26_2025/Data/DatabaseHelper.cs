using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FP_CS26_2025.Data
{
    public static class DatabaseHelper
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["GrandNexusDB"].ConnectionString;
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
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
    }
}
