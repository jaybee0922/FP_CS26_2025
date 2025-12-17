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
    public partial class PriceInputDialog : Form
    {
        public string NewPrice => txtInput.Text.Trim();

        public PriceInputDialog(string roomType, string oldPrice)
        {
            InitializeComponent();
            lblMessage.Text = $"Current Price for {roomType}: {oldPrice}\nEnter new price:";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
