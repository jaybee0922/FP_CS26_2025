using System;

namespace FP_CS26_2025.Services
{
    /// <summary>
    /// Service for handling authentication operations.
    /// Follows the Interface Segregation Principle and Dependency Inversion Principle.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Validates a user's credentials and role.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <param name="role">The role as selected in the UI.</param>
        /// <returns>True if the credentials are valid for the given role, false otherwise.</returns>
        bool Login(string username, string password, string role);
    }
}
