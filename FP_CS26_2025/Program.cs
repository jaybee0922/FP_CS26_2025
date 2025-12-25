//using HotelManagementSystem;
using FP_CS26_2025.Room_Rates___Policies;
using FP_CS26_2025.ModernDesign;
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
            //Application.Run(new RoomRates_and_Pricing_Form());
            //Application.Run(new Form1());
            //Application.Run(new HotelDashboardForm());
            Application.Run(new ModernHomeView());
        }
    }
}