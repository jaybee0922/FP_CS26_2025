using System;
using System.Drawing;
using System.Windows.Forms;
using FP_CS26_2025.Services;

namespace FP_CS26_2025.ModernDesign
{
    // Inheritance: Inherits from Form
    public partial class ModernHomeView : Form
    {
        private readonly IBookingService _bookingService;

        // Constructor Injection for Dependency Inversion
        public ModernHomeView(IBookingService bookingService)
        {
            _bookingService = bookingService;
            InitializeComponent();
            
            // Set active page in navbar
            this.modernNavbar.ActivePage = "Home";
        }

        // Default constructor for Designer and simple instantiation
        public ModernHomeView() : this(new BookingService())
        {
        }

        private void ModernHomeView_Load(object sender, EventArgs e)
        {
            // No image loading needed anymore. GradientPanel handles the background.
            // Setup events
            btnCheck.Click += btnCheck_Click;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            using (var modal = new BookingModalForm())
            {
                if (modal.ShowDialog() == DialogResult.OK)
                {
                    var request = modal.BookingResult;
                    request.CheckInDate = dtpArrival.Value;
                    request.CheckOutDate = dtpDeparture.Value;

                    // Calculate total
                    int nights = (request.CheckOutDate - request.CheckInDate).Days;
                    if (nights <= 0) nights = 1;

                    request.TotalPrice = _bookingService.CalculateTotal(request.RoomType, request.NumRooms, nights);

                    try
                    {
                        // Save to database
                        _bookingService.ProcessBookingRequest(request);

                        // Show receipt
                        using (var receipt = new BookingReceiptForm(request))
                        {
                            receipt.ShowDialog();
                        }

                        // Final notification as requested
                        MessageBox.Show("Receipt has been sent to your gmail!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to process booking: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void lblLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
