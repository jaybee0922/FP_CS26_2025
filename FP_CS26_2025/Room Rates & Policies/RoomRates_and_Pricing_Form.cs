using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FP_CS26_2025.Room_Rates___Policies
{
    public partial class RoomRates_and_Pricing_Form : Form 
    {
        public RoomRates_and_Pricing_Form()
        {
            InitializeComponent();
        }

        private void RoomRates_and_Pricing_Form_Load(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void LoadRoomData()
        {
            dgvRoomRates.Rows.Clear();

            dgvRoomRates.Rows.Add("Single Room", "₱1,200 per night", "Available", "Max 1 Guest");
            dgvRoomRates.Rows.Add("Double Room", "₱1,800 per night", "Booked", "Max 2 Guests");
            dgvRoomRates.Rows.Add("Family Suite", "₱3,500 per night", "Available", "Max 4 Guests");
            dgvRoomRates.Rows.Add("Deluxe Suite", "₱5,200 per night", "Under Maintenance", "Max 2 Guests");
        }

        private void btnChangePrice_Click(object sender, EventArgs e)
        {
            if (dgvRoomRates.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a room to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }

            var selectedRow = dgvRoomRates.SelectedRows[0];
            string roomType = selectedRow.Cells[0].Value.ToString();
            string priceOld = selectedRow.Cells[1].Value.ToString();

            PriceInputDialog dialog = new PriceInputDialog(roomType, priceOld);

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(dialog.NewPrice))
                {
                    selectedRow.Cells[1].Value = dialog.NewPrice;

                    MessageBox.Show("Pricing updated successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRoomData();
        }

        private void btnChangePrice_Click_1(object sender, EventArgs e)
        {
            PriceInputDialog changePrice = new PriceInputDialog("Single Room", "₱1,200 per night");
            changePrice.ShowDialog();
            
        }
    }
}
