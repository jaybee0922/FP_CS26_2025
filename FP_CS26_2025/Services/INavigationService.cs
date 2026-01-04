using System;
using System.Windows.Forms;

namespace FP_CS26_2025.Services
{
    /// <summary>
    /// Abstraction for navigation between views.
    /// Follows Dependency Inversion and Interface Segregation principles.
    /// </summary>
    public interface INavigationService
    {
        void NavigateToHome(Form currentForm);
        void NavigateToRooms(Form currentForm);
        void NavigateToLogin(Form currentForm);
        void NavigateToBookNow();
    }
}
