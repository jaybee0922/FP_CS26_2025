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
            Application.Run(new Form1());  // Start with the login form
        }
    }
}