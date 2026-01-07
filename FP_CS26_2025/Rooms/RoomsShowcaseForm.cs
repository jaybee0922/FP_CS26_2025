using System;
using System.Windows.Forms;

namespace FP_CS26_2025.Rooms
{
    public partial class RoomsShowcaseForm : Form
    {
        public RoomsShowcaseForm()
        {
            InitializeComponent();
            this.modernNavbar1.ActivePage = "Rooms";
        }

        private void roomGalleryView1_Load(object sender, EventArgs e)
        {
            // Sync Hotel Name
            try 
            {
                var config = FP_CS26_2025.ConfigHelper.LoadConfig();
                lblLogo.Text = config.HotelName.ToUpper(); // Syncing Hotel Name
                footerControl.UpdateInfo(
                    config.HotelAddress,
                    $"{config.HotelEmail} | {config.HotelPhone}",
                    config.CopyrightText
                );
                footerControl.BringToFront(); // Ensure it's on top
            }
            catch { /* Ignore if fails */ }
        }
    }
}
