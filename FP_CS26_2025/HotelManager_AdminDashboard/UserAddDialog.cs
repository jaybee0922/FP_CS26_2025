using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    public class UserAddDialog : Form
    {
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Username { get; private set; }
        public DateTime Birthday { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        private TextBox txtFirstName;
        private TextBox txtMiddleName;
        private TextBox txtLastName;
        private TextBox txtUsername;
        private DateTimePicker dtpBirthday;
        private TextBox txtPassword;
        private ComboBox cmbRole;
        private Button btnSave;
        private Button btnCancel;

        public UserAddDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(400, 500);
            this.Text = "Add New Employee";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            int labelWidth = 120;
            int inputWidth = 200;
            int startY = 30;
            int gap = 40;

            // First Name
            var lblFirstName = new Label { Text = "First Name:", Location = new Point(30, startY), AutoSize = true };
            txtFirstName = new TextBox { Location = new Point(30 + labelWidth, startY - 3), Width = inputWidth };
            this.Controls.Add(lblFirstName);
            this.Controls.Add(txtFirstName);

            // Middle Name
            startY += gap;
            var lblMiddleName = new Label { Text = "Middle Name:", Location = new Point(30, startY), AutoSize = true };
            txtMiddleName = new TextBox { Location = new Point(30 + labelWidth, startY - 3), Width = inputWidth };
            this.Controls.Add(lblMiddleName);
            this.Controls.Add(txtMiddleName);

            // Last Name
            startY += gap;
            var lblLastName = new Label { Text = "Last Name:", Location = new Point(30, startY), AutoSize = true };
            txtLastName = new TextBox { Location = new Point(30 + labelWidth, startY - 3), Width = inputWidth };
            this.Controls.Add(lblLastName);
            this.Controls.Add(txtLastName);

            // Username
            startY += gap;
            var lblUsername = new Label { Text = "Username:", Location = new Point(30, startY), AutoSize = true };
            txtUsername = new TextBox { Location = new Point(30 + labelWidth, startY - 3), Width = inputWidth };
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);

            // Password
            startY += gap;
            // Password
            startY += gap;
            var lblPassword = new Label { Text = "Password:", Location = new Point(30, startY), AutoSize = true };
            txtPassword = new TextBox { Location = new Point(30 + labelWidth, startY - 3), Width = inputWidth, UseSystemPasswordChar = true };
            
            // Eye Icon Button
            var btnTogglePass = new Button 
            { 
                Text = "👁", 
                Location = new Point(30 + labelWidth + inputWidth + 5, startY - 5), 
                Size = new Size(30, 25),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnTogglePass.FlatAppearance.BorderSize = 0;
            btnTogglePass.Click += (s, e) => {
                txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
                btnTogglePass.Text = txtPassword.UseSystemPasswordChar ? "👁" : "🚫";
            };

            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnTogglePass);

            // Birthday
            startY += gap;
            var lblBirthday = new Label { Text = "Birthday:", Location = new Point(30, startY), AutoSize = true };
            dtpBirthday = new DateTimePicker { Location = new Point(30 + labelWidth, startY - 3), Width = inputWidth, Format = DateTimePickerFormat.Short };
            this.Controls.Add(lblBirthday);
            this.Controls.Add(dtpBirthday);

            // Role
            startY += gap;
            var lblRole = new Label { Text = "Role:", Location = new Point(30, startY), AutoSize = true };
            cmbRole = new ComboBox { Location = new Point(30 + labelWidth, startY - 3), Width = inputWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRole.Items.Add("Front Desk");
            cmbRole.Items.Add("Manager");
            cmbRole.SelectedIndex = 0; // Default to Front Desk
            this.Controls.Add(lblRole);
            this.Controls.Add(cmbRole);

            // Buttons
            startY += gap * 2;
            btnSave = new Button { Text = "Save", Location = new Point(100, startY), DialogResult = DialogResult.None, Width = 80 };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button { Text = "Cancel", Location = new Point(200, startY), DialogResult = DialogResult.Cancel, Width = 80 };

            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnSave;
            this.CancelButton = btnCancel;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("All fields (except Middle Name) must be filled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Password Validation: 1 Uppercase, 1 Number (as implied by previous/context, enforcing standard security)
            string pwd = txtPassword.Text;
            bool hasUpper = pwd.Any(char.IsUpper);
            bool hasDigit = pwd.Any(char.IsDigit);

            if (!hasUpper || !hasDigit)
            {
                MessageBox.Show("Password must contain an uppercase letter and a number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmation
            var confirmResult = MessageBox.Show("Do you want to save these changes?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                FirstName = txtFirstName.Text.Trim();
                MiddleName = txtMiddleName.Text.Trim();
                LastName = txtLastName.Text.Trim();
                Username = txtUsername.Text.Trim();
                Birthday = dtpBirthday.Value;
                Password = txtPassword.Text;
                Role = cmbRole.SelectedItem.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
