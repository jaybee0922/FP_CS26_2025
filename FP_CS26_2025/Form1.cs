
using FP_CS26_2025.HotelManager_AdminDashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FP_CS26_2025
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Called when Form1 is loaded
        private void Form1_Load(object sender, EventArgs e)
        {
            // You can add any initialization logic here, if needed
        }

        // Login Button Click Event
        private void loginFormBtn_Click(object sender, EventArgs e)
        {
            //FrontDesk_ReceptionistAccoun front = new FrontDesk_ReceptionistAccoun();
            //front.Show();

            Hotel_AdminDashboard hotel = new Hotel_AdminDashboard();
            hotel.Show();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle role selection change
        }

        private void usernameInputField1_Load(object sender, EventArgs e)
        {
        }

        private void passwordInputField1_Load(object sender, EventArgs e)
        {
        }

        private void passwordInputField2_Load(object sender, EventArgs e)
        {
        }

        private void passwordInputField1_Load_1(object sender, EventArgs e)
        {
        }

        private void loginFormContainer1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}