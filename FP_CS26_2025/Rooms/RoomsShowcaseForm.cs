using System;
using System.Drawing;
using System.Windows.Forms;

namespace FP_CS26_2025.Rooms
{
    public partial class RoomsShowcaseForm : Form
    {
        public RoomsShowcaseForm()
        {
            InitializeComponent();
            this.modernNavbar1.ActivePage = "Rooms";
            
            // Set Default Dates
            dtpArrival.Value = DateTime.Today;
            dtpDeparture.Value = DateTime.Today.AddDays(1);
            
            // Subscribe to Booking Event
            roomGalleryView1.RoomBookRequested += RoomGalleryView1_RoomBookRequested;

            try
            {
                // Load background image
                string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "homepage_bg.jpg");
                if (System.IO.File.Exists(imagePath))
                {
                    this.mainBackgroundPanel.BackgroundImage = Image.FromFile(imagePath);
                    this.mainBackgroundPanel.BackgroundImageLayout = ImageLayout.Zoom;
                    // Ensure controls are transparent where needed or panel is visible through
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load background image: {ex.Message}");
            }
        }

        private void RoomGalleryView1_RoomBookRequested(object sender, string roomName)
        {
            // Validate Dates
            var checkIn = dtpArrival.Value.Date;
            var checkOut = dtpDeparture.Value.Date;

            if (checkIn < DateTime.Today)
            {
                MessageBox.Show("Check-in date cannot be in the past.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkOut <= checkIn)
            {
                MessageBox.Show("Departure must be at least one day after arrival.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Launch Modal
            using (var modal = new FP_CS26_2025.ModernDesign.BookingModalForm(
                new FP_CS26_2025.Rooms.RoomService(), 
                new FP_CS26_2025.Services.BookingService(), 
                checkIn, 
                checkOut,
                null,      // promoCode
                roomName)) // preSelectedRoom - Pass the room name to lock it
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    ProcessBookingSafely(modal.BookingResult);
                }
            }
        }

        private void ProcessBookingSafely(FP_CS26_2025.Services.Models.BookingRequestData request)
        {
            try
            {
                var bookingService = new FP_CS26_2025.Services.BookingService();
                bookingService.ProcessBookingRequest(request);

                using (var receipt = new FP_CS26_2025.ModernDesign.BookingReceiptForm(request))
                {
                    receipt.ShowDialog();
                }
                MessageBox.Show("Receipt has been sent to your gmail!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing booking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void roomGalleryView1_Load(object sender, EventArgs e)
        {
            // Sync Hotel Name
            try 
            {
                var config = FP_CS26_2025.ConfigHelper.LoadConfig();
                lblLogo.Text = config.HotelName.ToUpper(); 
                footerControl.UpdateInfo(
                    config.HotelAddress,
                    $"{config.HotelEmail} | {config.HotelPhone}",
                    config.CopyrightText
                );
                footerControl.BringToFront(); 
            }
            catch { /* Ignore if fails */ }
        }
    }
}
