using System;
using System.Windows.Forms;
using FP_CS26_2025.ModernDesign;
using FP_CS26_2025.Rooms;

namespace FP_CS26_2025.Services
{
    /// <summary>
    /// Concrete implementation of navigation service.
    /// Handles the creation and management of form transitions.
    /// </summary>
    public class NavigationService : INavigationService
    {
        public void NavigateToHome(Form currentForm)
        {
            if (currentForm is ModernHomeView) return;

            ModernHomeView homeView = new ModernHomeView();
            SwitchForm(currentForm, homeView);
        }

        public void NavigateToRooms(Form currentForm)
        {
            if (currentForm is RoomsShowcaseForm) return;

            RoomsShowcaseForm roomsView = new RoomsShowcaseForm();
            SwitchForm(currentForm, roomsView);
        }

        public void NavigateToLogin(Form currentForm)
        {
            if (currentForm is LoginForm) return;
            LoginForm loginForm = new LoginForm();
            SwitchForm(currentForm, loginForm);
        }

        public void NavigateToBookNow()
        {
            // Implementation for book now, e.g., showing a dialog or scroll to section
            MessageBox.Show("Book Now clicked!");
        }

        private void SwitchForm(Form current, Form next)
        {
            next.Show();
            current.Hide();
            // Ensure application exits if last form is closed
            next.FormClosed += (s, e) => current.Close();
        }
    }
}
