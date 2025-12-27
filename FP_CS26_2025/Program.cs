using System;
using System.Windows.Forms;
using FP_CS26_2025.Rooms;
using FP_CS26_2025.ModernDesign;
using FP_CS26_2025.Room_Rates___Policies;

namespace FP_CS26_2025
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // To run the new Room Gallery Showcase:
            Application.Run(new RoomsShowcaseForm());
            
            // Original startup:
            // Application.Run(new ModernHomeView());
        }
    }
}