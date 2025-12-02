//using HotelManagementSystem;
using FP_CS26_2025.Room_Rates___Policies;
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
            Application.Run(new RoomRates_and_Pricing_Form());
            Application.Run(new Form1());  // Start with the login form
            //Application.Run(new HotelDashboardForm());
        }
    }
}