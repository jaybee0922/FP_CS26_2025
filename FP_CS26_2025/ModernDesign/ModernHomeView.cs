using System;
using System.Drawing;
using System.Windows.Forms;
using FP_CS26_2025.Services;
using FP_CS26_2025.Rooms;
using FP_CS26_2025.Services.Models;

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

        public ModernHomeView(IBookingService bookingService, IRoomService roomService)
        {
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
            InitializeComponent();
            
            this.modernNavbar.ActivePage = "Home";
        }

        public ModernHomeView() : this(new BookingService(), new RoomService())
        {
        }

        private void ModernHomeView_Load(object sender, EventArgs e)
        {
            btnCheck.Click += SafeBtnCheck_Click;
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
            using (var modal = new BookingModalForm(_roomService, _bookingService, checkIn, checkOut))
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
