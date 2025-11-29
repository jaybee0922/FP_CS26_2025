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
          

            FrontDesk_ReceptionistAccoun front = new FrontDesk_ReceptionistAccoun();
            front.Show();
        }

        private void usernameInputField1_Load(object sender, EventArgs e)
        {

        }
    }
}