using System;
using System.Windows.Forms;
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
            // Abstraction: Displaying clean mapped data from the DTO
            lblGuestInfo.Text = $"Guest: {data.FirstName} {data.LastName}";
            lblRoomInfo.Text = $"Room: {data.NumRooms} x {data.RoomType} - {data.NumAdults} Adults" + 
                                (data.NumChildren > 0 ? $", {data.NumChildren} Children" : "");
            lblDatesInfo.Text = $"Dates: {data.CheckInDate.ToShortDateString()} to {data.CheckOutDate.ToShortDateString()}";
            lblTotalPrice.Text = $"Total Price: P{data.TotalPrice:N2}";
        }
    }
}
