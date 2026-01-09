using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using FP_CS26_2025.Services.Models;

namespace FP_CS26_2025.ModernDesign
{
    /// <summary>
    /// Displays a summary receipt of the requested reservation.
    /// Following SRP by only handling the display of booking data.
    /// </summary>
    public partial class BookingReceiptForm : Form
    {
        public BookingReceiptForm(BookingRequestData data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            
            InitializeComponent();
            PopulateData(data);
            
            this.btnClose.Click += (s, e) => this.Close();
        }

        private void PopulateData(BookingRequestData data)
        {
            // Abstraction: Displaying the room image for visual confirmation
            if (!string.IsNullOrEmpty(data.RoomImagePath) && File.Exists(data.RoomImagePath))
            {
                try
                {
                    picRoomReceipt.Image?.Dispose();
                    picRoomReceipt.Image = Image.FromFile(data.RoomImagePath);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Image Load Error: {ex.Message}");
                    picRoomReceipt.Image = null; // Robust fallback
                }
            }

            // Abstraction: Displaying clean mapped data from the DTO
            lblGuestInfo.Text = $"Guest: {data.FirstName} {data.LastName}";
            lblRoomInfo.Text = $"Room: {data.NumRooms} x {data.RoomType} - {data.NumAdults} Adults" + 
                                (data.NumChildren > 0 ? $", {data.NumChildren} Children" : "");
            lblDatesInfo.Text = $"Dates: {data.CheckInDate.ToShortDateString()} to {data.CheckOutDate.ToShortDateString()}";
            lblTotalPrice.Text = $"Total Price: P{data.TotalPrice:N2}";

            if (!string.IsNullOrEmpty(data.PromoCode))
            {
                lblReceiptDiscount.Text = $"(Discount P{data.DiscountAmount:N2} applied via {data.PromoCode})";
                lblReceiptDiscount.Visible = true;
            }
            else
            {
                lblReceiptDiscount.Visible = false;
            }

            // Load and display current hotel policies
            try
            {
                var currentConfig = ConfigHelper.LoadConfig();
                lblPoliciesText.Text = currentConfig?.PolicyText ?? "Standard hotel policies apply.";
            }
            catch (Exception ex)
            {
                // Robustness: Handle configuration loading errors gracefully
                System.Diagnostics.Debug.WriteLine($"Policy Load Error: {ex.Message}");
                lblPoliciesText.Text = "Standard hotel policies apply.";
            }
        }
    }
}
