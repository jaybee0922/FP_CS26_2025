using System.Windows.Forms;

namespace FP_CS26_2025.Services
{
    /// <summary>
    /// Interface for logout operations.
    /// Implements Abstraction (OOP) and Dependency Inversion (SOLID).
    /// </summary>
    public interface ILogoutService
    {
        /// <summary>
        /// Handles the logout logic including confirmation and navigation.
        /// </summary>
        /// <param name="currentForm">The form to be closed upon logout.</param>
        void HandleLogout(Form currentForm);
    }
}
