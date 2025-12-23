using System;
using System.Drawing;
using System.Windows.Forms;

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
        }

        // Default constructor for Designer and simple instantiation
        public ModernHomeView() : this(new BookingService())
        {
        }

        private void ModernHomeView_Load(object sender, EventArgs e)
        {
            // No image loading needed anymore. GradientPanel handles the background.
            // Setup events
            btnCheck.Click += BtnCheck_Click;
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            bool available = _bookingService.CheckAvailability(dtpArrival.Value, dtpDeparture.Value, txtPromo.Text);
            if (available)
            {
               _bookingService.BookNow();
            }
        }
    }
}
