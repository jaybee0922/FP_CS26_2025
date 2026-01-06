using System;
using System.Windows.Forms;

namespace FP_CS26_2025.Room_Rates___Policies
{
    public partial class PriceInputDialog : Form
    {
        public string NewPrice { get; private set; }

        public PriceInputDialog(string roomType, string oldPrice)
        {
            InitializeComponent();
            lblMessage.Text = $"Update price for {roomType}\nCurrent: {oldPrice}";
            
            // Extract numeric value from oldPrice if possible for the textbox default
            // Assuming format "₱1,200 per night" or similar
            // For now, just focus might be better empty or let them type.
            // Let's leave it empty to force conscious entry.
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string input = txtInput.Text.Trim();
            
            // Remove currency symbol and "per night" if user typed them, just in case
            string cleanInput = input.Replace("₱", "").Replace("per night", "").Replace(",", "").Trim();

            if (decimal.TryParse(cleanInput, out decimal price))
            {
                if (price < 0)
                {
                    MessageBox.Show("Price cannot be negative.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Format back to standard display format "₱N,NNN.00 per night" or similar
                // Or just keep the raw number and let the caller format it
                // The caller code: selectedRow.Cells[1].Value = dialog.NewPrice;
                // Existing format: "₱1,200 per night"
                
                NewPrice = $"₱{price:N0} per night"; 
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric price.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
