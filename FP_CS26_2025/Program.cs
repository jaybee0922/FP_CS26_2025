using System;
using System.Windows.Forms;
using FP_CS26_2025.HotelManager_AdminDashboard;

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
        }
    }
}
