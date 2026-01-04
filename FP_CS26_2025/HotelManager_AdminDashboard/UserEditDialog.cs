using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    public class UserEditDialog : Form
    {
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string Username { get; private set; } // Read-only usually, or editable? Requirement said "requirements: FirstName... UserName..." implies editable but mostly people don't change username. I will allow it based on req.
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

        private SystemUser _currentUser;

        public UserEditDialog(SystemUser user)
        {
            _currentUser = user;
            InitializeComponent();
            LoadUserData();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(400, 500);
            this.Text = "Edit Employee";
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
            var lblPassword = new Label { Text = "Password:", Location = new Point(30, startY), AutoSize = true };
            txtPassword = new TextBox { Location = new Point(30 + labelWidth, startY - 3), Width = inputWidth, UseSystemPasswordChar = true };
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);

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

        private void LoadUserData()
        {
            if (_currentUser == null) return;

            txtFirstName.Text = _currentUser.FirstName;
            txtMiddleName.Text = _currentUser.MiddleName;
            txtLastName.Text = _currentUser.LastName;
            txtUsername.Text = _currentUser.Username;

            // Set Birthday
            if (_currentUser.Birthday != DateTime.MinValue)
                dtpBirthday.Value = _currentUser.Birthday;

            // Set Role
            if (_currentUser is ManagerUser)
                cmbRole.SelectedItem = "Manager";
            else
                cmbRole.SelectedItem = "Front Desk";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("All fields (except Middle Name and Password) must be filled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Password Validation only if changed
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                string pwd = txtPassword.Text;
                bool hasUpper = pwd.Any(char.IsUpper);
                bool hasDigit = pwd.Any(char.IsDigit);

                if (!hasUpper || !hasDigit)
                {
                    MessageBox.Show("Password must contain an uppercase letter and a number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Password = txtPassword.Text;
            }
            else
            {
                Password = null; // Indicate no change
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
                Role = cmbRole.SelectedItem.ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
