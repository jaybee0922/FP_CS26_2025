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
            bool available = _bookingService.CheckAvailability(dtpArrival.Value, dtpDeparture.Value, txtPromo.Text);
            if (available)
            {
               _bookingService.BookNow();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();

            // Optional: Ensure the application closes when the login form is closed, 
            // or handle the FormClosed event of loginForm if needed to show this form back.
            // For now, simple navigation as requested.
            loginForm.FormClosed += (s, args) => this.Close(); 
        }

        private void lblLogo_Click(object sender, EventArgs e)
        {

        }
    }
}
