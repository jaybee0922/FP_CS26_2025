
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


using FP_CS26_2025;
using FP_CS26_2025.HotelManager_AdminDashboard;

using FP_CS26_2025.Services;
using FP_CS26_2025.HotelManager_AdminDashboard.Configuration;

namespace FP_CS26_2025
{
    public partial class LoginForm : Form
    {
        private readonly IAuthService _authService;
        private readonly IConfigService _configService;

        public LoginForm() : this(new DatabaseAuthService(), new FP_CS26_2025.HotelManager_AdminDashboard.Configuration.XmlConfigService())
        {
        }

        public LoginForm(IAuthService authService, IConfigService configService)
        {
            InitializeComponent();
            _authService = authService;
            _configService = configService;
            this.DoubleBuffered = true;
            this.modernNavbar.ActivePage = "Login";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            roleComboBox1.Items.Clear();
            roleComboBox1.Items.Add("Super Admin");
            roleComboBox1.Items.Add("Front Desk");
            
            // Sync Hotel Name
            if (_configService != null)
            {
                var config = _configService.LoadConfig();
                lblLogo.Text = config.HotelName.ToUpper();
                lblCopyright.Text = config.CopyrightText;
            }
        }

        private void loginFormBtn_Click(object sender, EventArgs e)
        {
            string selectedRole = roleComboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Please select a role.", "No Role Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = usernameInputField2.Text;
            string password = passwordInputField2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!_authService.Login(username, password, selectedRole))
            {
                MessageBox.Show("Invalid credentials or role selection.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form nextForm = null;

            switch (selectedRole)
            {
                case "Super Admin":
                    nextForm = new Hotel_AdminDashboard();
                    break;

                case "Front Desk":
                    nextForm = new Hotel_FrontDeskDashboard();
                    break;

                default:
                    MessageBox.Show("Unknown role selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }

            nextForm.FormClosed += (s, args) => 
            {
                ClearCredentials();
                this.Show();
            };
            nextForm.Show();
            this.Hide(); 
        }

        private void ClearCredentials()
        {
            usernameInputField2.Text = "";
            passwordInputField2.Text = "";
            roleComboBox1.SelectedIndex = -1;
        }

        private void roleComboBox1_Load(object sender, EventArgs e) { }
        private void usernameInputField1_Load(object sender, EventArgs e) { }
        private void passwordInputField1_Load(object sender, EventArgs e) { }
        private void passwordInputField2_Load(object sender, EventArgs e) { }
        private void passwordInputField1_Load_1(object sender, EventArgs e) { }
        private void loginFormContainer1_Paint(object sender, PaintEventArgs e) { }
    }
}
