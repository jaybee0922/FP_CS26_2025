using System;
using System.Drawing;
using System.Windows.Forms;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    public partial class UserListItemControl : UserControl
    {
        private SystemUser _user;
        public event EventHandler EditClicked;
        public event EventHandler DeleteClicked;

        private CheckBox checkBox;
        private Label nameLabel;
        private Label dateLabel; // Shows Employee ID
        private Label dateAddedLabel; // Shows Date Added
        private Label roleLabel;
        private Button editButton;
        private Button deleteButton;

        public UserListItemControl(SystemUser user)
        {
            _user = user;
            InitializeComponent();
            SetupData();
        }

        // Constructor for Header use
        public UserListItemControl()
        {
            InitializeComponent();
        }

        public void ConfigureAsHeader()
        {
            // Hide interactive elements
            checkBox.Visible = false;
            editButton.Visible = false;
            deleteButton.Visible = false;

            // Configure Labels
            nameLabel.Text = "UserName";
            nameLabel.ForeColor = Color.DimGray;
            nameLabel.Cursor = Cursors.Default;
            nameLabel.Click -= NameLabel_Click; // Remove click event

            dateAddedLabel.Text = "Date Added";
            dateAddedLabel.ForeColor = Color.DimGray;

            roleLabel.Text = "Access";
            roleLabel.ForeColor = Color.DimGray;
            roleLabel.BackColor = Color.Transparent; // Remove badge background
            roleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold); // Match other headers
            roleLabel.Padding = new Padding(0); // Remove badge padding

            this.BackColor = Color.FromArgb(240, 240, 240); // Light gray header background
        }

        private void InitializeComponent()
        {
            this.checkBox = new CheckBox();
            this.nameLabel = new Label();
            this.dateLabel = new Label();
            this.dateAddedLabel = new Label();
            this.roleLabel = new Label();
            this.editButton = new Button();
            this.deleteButton = new Button();

            this.SuspendLayout();

            // CheckBox
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new Point(20, 20);

            // Name (Full Name/Username) - Left
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new Point(100, 15); // Moved to 100 to align with header
            this.nameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Employee ID - Hidden
            this.dateLabel.AutoSize = true;
            this.dateLabel.Visible = false; // Hide from list

            // Date Added - Moved to Center (was Employee ID position)
            this.dateAddedLabel.AutoSize = true;
            this.dateAddedLabel.ForeColor = Color.Black;
            this.dateAddedLabel.Font = new Font("Segoe UI", 9F);
            this.dateAddedLabel.Location = new Point(500, 18); // Aligned at 500

            // Role Label (Badge) - Right
            this.roleLabel.AutoSize = true;
            this.roleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            this.roleLabel.ForeColor = Color.Gray;
            this.roleLabel.Location = new Point(850, 18); // Moved to 850 to align with header

            // 
            // Edit Button (Pencil)
            // 
            this.editButton.Text = "✎";
            this.editButton.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Regular);
            this.editButton.Size = new Size(40, 30);
            this.editButton.Location = new Point(1000, 15);
            this.editButton.FlatStyle = FlatStyle.Flat;
            this.editButton.FlatAppearance.BorderSize = 0;
            this.editButton.ForeColor = Color.DimGray;
            this.editButton.Cursor = Cursors.Hand;
            this.editButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.editButton.Click += (s, e) => EditClicked?.Invoke(this, EventArgs.Empty);
            this.editButton.Visible = false;

            // 
            // Delete Button (Trash)
            // 
            this.deleteButton.Text = "🗑";
            this.deleteButton.Font = new Font("Segoe UI Symbol", 12F, FontStyle.Regular);
            this.deleteButton.Size = new Size(40, 30);
            this.deleteButton.Location = new Point(1050, 15);
            this.deleteButton.FlatStyle = FlatStyle.Flat;
            this.deleteButton.FlatAppearance.BorderSize = 0;
            this.deleteButton.ForeColor = Color.Red;
            this.deleteButton.Cursor = Cursors.Hand;
            this.deleteButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.deleteButton.Click += (s, e) => DeleteClicked?.Invoke(this, EventArgs.Empty);
            this.deleteButton.Visible = false;

            // Checkbox Events
            this.checkBox.CheckedChanged += CheckBox_CheckedChanged;

            this.Controls.Add(checkBox);
            this.Controls.Add(nameLabel);
            this.Controls.Add(dateLabel);
            this.Controls.Add(dateAddedLabel);
            this.Controls.Add(roleLabel);
            this.Controls.Add(editButton);
            this.Controls.Add(deleteButton);

            this.Size = new Size(1120, 60);
            this.BackColor = Color.White;
            this.Padding = new Padding(0, 0, 0, 1);
            this.Margin = new Padding(0); // Ensure no external margin affects alignment

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox.Checked;
            editButton.Visible = isChecked;
            deleteButton.Visible = isChecked;

            this.BackColor = isChecked ? Color.FromArgb(245, 245, 255) : Color.White;
        }

        private void SetupData()
        {
            // Display Username
            nameLabel.Text = _user.Username;
            nameLabel.ForeColor = Color.Black; // Changed to Black as requested
            nameLabel.Cursor = Cursors.Hand;

            nameLabel.Click -= NameLabel_Click;
            nameLabel.Click += NameLabel_Click;

            // Display Employee ID instead of Date Added
            dateLabel.Text = _user.EmployeeId; // e.g. "001"
            dateLabel.ForeColor = Color.Black;

            // Display Date Added
            dateAddedLabel.Text = "Date Added: " + _user.DateAdded.ToString("MMM dd, yyyy");

            // Setup Role Badge
            string role = _user.GetRoleDisplay();

            if (role == "Manager")
            {
                roleLabel.Text = "Manager";
                roleLabel.BackColor = Color.FromArgb(0, 122, 255); // Blue
            }
            else
            {
                roleLabel.Text = "Front Desk";
                roleLabel.BackColor = Color.FromArgb(255, 149, 0); // Orange
            }

            roleLabel.ForeColor = Color.White;
            roleLabel.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            roleLabel.Padding = new Padding(5, 2, 5, 2);
        }

        private void NameLabel_Click(object sender, EventArgs e)
        {
            string details = $"Employee ID: {_user.EmployeeId}\n\n" +
                             $"Username: {_user.Username}\n" +
                             $"Full Name: {_user.FirstName} {_user.MiddleName} {_user.LastName}\n" +
                             $"Birthday: {_user.Birthday:MMM dd, yyyy}\n" +
                             $"Role: {_user.GetRoleDisplay()}";

            MessageBox.Show(details, "Employee Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw bottom border
            e.Graphics.DrawLine(Pens.LightGray, 0, this.Height - 1, this.Width, this.Height - 1);
        }
    }
}
