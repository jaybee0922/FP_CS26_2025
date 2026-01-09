using System;
using System.Drawing;
using System.Windows.Forms;
using FP_CS26_2025.Services;
using FP_CS26_2025.Rooms;
using FP_CS26_2025.Services.Models;

using FP_CS26_2025.HotelManager_AdminDashboard.Configuration;

namespace FP_CS26_2025.ModernDesign
{
    /// <summary>
    /// Main view for the modern application.
    /// Orchestrates the booking flow with production-grade robustness and security.
    /// </summary>
    public partial class ModernHomeView : Form
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        private readonly IConfigService _configService;

        public ModernHomeView(IBookingService bookingService, IRoomService roomService, IConfigService configService)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            InitializeComponent();
            
            this.modernNavbar.ActivePage = "Home";
        }

        public ModernHomeView() : this(new BookingService(), new RoomService(), new XmlConfigService())
        {
        }

        private void ModernHomeView_Load(object sender, EventArgs e)
        {
            btnCheck.Click += SafeBtnCheck_Click;
            LoadHotelConfiguration();

            try
            {
                // Load background image
                string imagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "homepage_bg.jpg");
                if (System.IO.File.Exists(imagePath))
                {
                    this.mainBackgroundPanel.BackgroundImage = Image.FromFile(imagePath);
                    this.mainBackgroundPanel.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load background image: {ex.Message}");
            }
        }

        private void LoadHotelConfiguration()
        {
            try
            {
                var config = _configService.LoadConfig();
                
                // Update Hotel Name
                lblLogo.Text = config.HotelName.ToUpper();
                lblWelcome.Text = $"WELCOME TO {config.HotelName.ToUpper()}";

                // Update Footer
                footerControl.UpdateInfo(
                    config.HotelAddress,
                    $"{config.HotelEmail} | {config.HotelPhone}",
                    config.CopyrightText
                );
            }
            catch (Exception ex)
            {
                // Fallback or silent fail
                System.Diagnostics.Debug.WriteLine($"Failed to load config: {ex.Message}");
            }
        }

        private void SafeBtnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                ExecuteBookingFlow();
            }
            catch (Exception ex)
            {
                // Robustness: No crashes allowed
                MessageBox.Show("Security Failure: The system encountered an error starting the booking process.\n" + ex.Message,
                    "Process Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecuteBookingFlow()
        {
            // Precision Date Handling
            DateTime checkIn = dtpArrival.Value.Date;
            DateTime checkOut = dtpDeparture.Value.Date;

            if (checkIn < DateTime.Today)
            {
                MessageBox.Show("Precision Error: Check-in date cannot be in the past.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkIn >= checkOut)
            {
                MessageBox.Show("Precision Error: Departure must be at least one day after arrival.", "Date Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dependency Inversion: Injecting services and context securely
            using (var modal = new BookingModalForm(_roomService, _bookingService, checkIn, checkOut, txtPromo.Text.Trim()))
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    ProcessBookingSafely(modal.BookingResult);
                }
            }
        }

        private void ProcessBookingSafely(BookingRequestData request)
        {
            try
            {
                if (request == null) return;

                // Persistence: Process through the service layer (Security & SRP)
                _bookingService.ProcessBookingRequest(request);

                // UI: Display the professional receipt (Abstraction)
                using (var receipt = new BookingReceiptForm(request))
                {
                    receipt.ShowDialog();
                }

                MessageBox.Show("Receipt has been sent to your gmail!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Booking Failure: System could not finalize your reservation securely.\n" + ex.Message, 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblLogo_Click(object sender, EventArgs e)
        {
            // Placeholder for brand navigation
        }
    }
}
