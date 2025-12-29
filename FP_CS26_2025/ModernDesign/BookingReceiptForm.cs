using System;
using System.Windows.Forms;
using FP_CS26_2025.Services;

namespace FP_CS26_2025.ModernDesign
{
    public partial class BookingReceiptForm : Form
    {
        public BookingReceiptForm(BookingRequestData data)
        {
            InitializeComponent();
            PopulateData(data);
            this.btnClose.Click += (s, e) => this.Close();
        }

        private void PopulateData(BookingRequestData data)
        {
            lblGuestInfo.Text = $"Guest: {data.FirstName} {data.LastName}";
            lblRoomInfo.Text = $"Room: {data.NumRooms} x {data.RoomType} - {data.NumAdults} Adults" + 
                                (data.NumChildren > 0 ? $", {data.NumChildren} Children" : "");
            lblDatesInfo.Text = $"Dates: {data.CheckInDate.ToShortDateString()} to {data.CheckOutDate.ToShortDateString()}";
            lblTotalPrice.Text = $"Total Price: P{data.TotalPrice:N2}";
        }
    }
}
