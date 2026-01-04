using System;
using FP_CS26_2025.Data;

namespace FP_CS26_2025.Services
{
    /// <summary>
    /// Implementation of IAuthService that uses the database for validation.
    /// Follows the Single Responsibility Principle.
    /// </summary>
    public class DatabaseAuthService : IAuthService
    {
        public bool Login(string username, string password, string role)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                return false;
            }

            // Map UI role to Database role (e.g., "Super Admin" -> "SuperAdmin")
            string dbRole = role.Replace(" ", "");

            return DatabaseHelper.ValidateUser(username, password, dbRole);
        }
    }
}
