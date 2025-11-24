using System;
using System.Windows.Forms;
//using FP_CS26_2025.HotelManager_AdminAccount; // ✅ correct namespace for your form
using FP_CS26_2025.FrontDesk_ReceptionistAccount;
namespace FP_CS26_2025
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            // Directly launch the Hotel Manager form for testing
            Application.Run(new FrontDesk_ReceptionistAccoun());

            // To test frontdesk hotel 
            //Application.Run(new FrontDesk_ReceptionistAccoun());
            

            //To test login form 
            Application.Run(new Form1());


        }
    }
}
