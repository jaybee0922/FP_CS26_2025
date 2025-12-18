
//using FP_CS26_2025.HotelManager_AdminDashboard;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace FP_CS26_2025
//{
//    public partial class Form1 : Form
//    {
//        public Form1()
//        {
//            InitializeComponent();
//        }

//        // Called when Form1 is loaded
//        private void Form1_Load(object sender, EventArgs e)
//        {
//            // You can add any initialization logic here, if needed
//        }

//        // Login Button Click Event
//        private void loginFormBtn_Click(object sender, EventArgs e)
//        {
//            //FrontDesk_ReceptionistAccoun front = new FrontDesk_ReceptionistAccoun();
//            //front.Show();

//            //Hotel_AdminDashboard hotel = new Hotel_AdminDashboard();
//            //hotel.Show();

//            Room_Manager.Room_Manager roomManager = new Room_Manager.Room_Manager();
//            roomManager.Show();


//            //FP_CS26_2025.Room_Manager.Room_Manager roomManager = new FP_CS26_2025.Room_Manager.Room_Manager();
//            //roomManager.Show();


//        }

//        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            // Handle role selection change
//        }

//        private void usernameInputField1_Load(object sender, EventArgs e)
//        {
//        }

//        private void passwordInputField1_Load(object sender, EventArgs e)
//        {
//        }

//        private void passwordInputField2_Load(object sender, EventArgs e)
//        {
//        }

//        private void passwordInputField1_Load_1(object sender, EventArgs e)
//        {
//        }

//        private void loginFormContainer1_Paint(object sender, PaintEventArgs e)
//        {

//        }

//        private void roleComboBox1_Load(object sender, EventArgs e)
//        {

//        }
//    }
//}





using System;
using System.Windows.Forms;


using FP_CS26_2025;
using FP_CS26_2025.HotelManager_AdminDashboard;
using FP_CS26_2025.Room_Manager;

namespace FP_CS26_2025
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            roleComboBox1.Items.Clear();
            roleComboBox1.Items.Add("Super Admin");
            roleComboBox1.Items.Add("Front Desk");
            //roleComboBox1.Items.Add("Room Manager");
        }

        private void loginFormBtn_Click(object sender, EventArgs e)
        {
            string selectedRole = roleComboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Please select a role.", "No Role Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form nextForm = null;

            switch (selectedRole)
            {
                case "Super Admin":
                    nextForm = new Hotel_AdminDashboard();
                    break;

                case "Front Desk":
                    nextForm = new FrontDesk_ReceptionistAccoun();
                    break;

                //case "Room Manager":
                //    nextForm = new Room_Manager();
                //    break;

                default:
                    MessageBox.Show("Unknown role selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            nextForm.Show();
            this.Hide(); 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
      
        }

        private void roleComboBox1_Load(object sender, EventArgs e)
        {
    
        }


        private void usernameInputField1_Load(object sender, EventArgs e) { }
        private void passwordInputField1_Load(object sender, EventArgs e) { }
        private void passwordInputField2_Load(object sender, EventArgs e) { }
        private void passwordInputField1_Load_1(object sender, EventArgs e) { }
        private void loginFormContainer1_Paint(object sender, PaintEventArgs e) { }
    }
}