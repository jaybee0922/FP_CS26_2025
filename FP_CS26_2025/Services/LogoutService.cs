using System.Windows.Forms;

namespace FP_CS26_2025.Services
{
    /// <summary>
    /// Concrete implementation of the logout service.
    /// Implements Encapsulation and Single Responsibility (SOLID).
    /// </summary>
    public class LogoutService : ILogoutService
    {
        /// <summary>
        /// Prompts the user for confirmation and closes the form if they choose 'Yes'.
        /// </summary>
        /// <param name="currentForm">The active dashboard form.</param>
        public void HandleLogout(Form currentForm)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?", 
                "Logout Confirmation", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                currentForm.Close();
            }
        }
    }
}
