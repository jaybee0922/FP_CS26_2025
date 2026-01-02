using FP_CS26_2025.HotelManager_AdminDashboard;
using FP_CS26_2025.ModernDesign;
using FP_CS26_2025.Room_Rates___Policies;
using FP_CS26_2025.Rooms;
using System;
using System.Windows.Forms;

namespace FP_CS26_2025
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Hotel_AdminDashboard());
            //Application.Run(new RoomsShowcaseForm());
            //Application.Run(new ModernHomeView());

        }
    }
}
