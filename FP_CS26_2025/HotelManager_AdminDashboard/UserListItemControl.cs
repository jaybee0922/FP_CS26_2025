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
        private Label emailLabel;
        private Label roleLabel;
        private Label lastActiveLabel;
        private Label dateAddedLabel;
        private Button editButton;
        private Button deleteButton;

        public UserListItemControl(SystemUser user)
        {
            _user = user;
            InitializeComponent();
            SetupData();
            ApplyStyles();
        }

        private void InitializeComponent()
        {
            this.checkBox = new CheckBox();
            this.nameLabel = new Label();
            this.emailLabel = new Label();
            this.roleLabel = new Label();
            this.lastActiveLabel = new Label();
            this.dateAddedLabel = new Label();
            this.editButton = new Button();
            this.deleteButton = new Button();

            this.SuspendLayout();

            // CheckBox
            this.checkBox.AutoSize = true;
            this.checkBox.Location = new Point(20, 20);

            // Name
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new Point(60, 15);
            this.nameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Email
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new Point(60, 35);
            this.emailLabel.ForeColor = Color.Gray;

            // 
            // Role (Access)
            // 
            this.roleLabel.AutoSize = false;
            this.roleLabel.Size = new Size(80, 25);
            this.roleLabel.Location = new Point(380, 18); // Center 420
            this.roleLabel.TextAlign = ContentAlignment.MiddleCenter;
            this.roleLabel.ForeColor = Color.White;

            // 
            // Last Active
            // 
            this.lastActiveLabel.AutoSize = false;
            this.lastActiveLabel.Size = new Size(140, 25);
            this.lastActiveLabel.Location = new Point(580, 22); // Center 650
            this.lastActiveLabel.TextAlign = ContentAlignment.MiddleCenter;

            // 
            // Date Added
            // 
            this.dateAddedLabel.AutoSize = false;
            this.dateAddedLabel.Size = new Size(140, 25);
            this.dateAddedLabel.Location = new Point(810, 22); // Center 880
            this.dateAddedLabel.TextAlign = ContentAlignment.MiddleCenter;

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
            this.Controls.Add(emailLabel);
            this.Controls.Add(roleLabel);
            this.Controls.Add(lastActiveLabel);
            this.Controls.Add(dateAddedLabel);
            this.Controls.Add(editButton);
            this.Controls.Add(deleteButton);

            this.Size = new Size(1150, 60);
            this.BackColor = Color.White;
            this.Padding = new Padding(0, 0, 0, 1);
            
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBox.Checked;
            editButton.Visible = isChecked;
            deleteButton.Visible = isChecked;
            
            // Optional: Change background color on selection
            this.BackColor = isChecked ? Color.FromArgb(245, 245, 255) : Color.White;
        }

        private void SetupData()
        {
            nameLabel.Text = _user.Username;
            emailLabel.Text = _user.Email;
            roleLabel.Text = _user.GetRoleDisplay();
            lastActiveLabel.Text = _user.LastActive.ToString("MMM d, yyyy");
            dateAddedLabel.Text = _user.DateAdded.ToString("MMM d, yyyy");
        }

        private void ApplyStyles()
        {
            // Role Pill Style
            if (_user is AdminUser)
            {
                roleLabel.BackColor = Color.FromArgb(0, 122, 255); // Blue
            }
            else
            {
                roleLabel.BackColor = Color.Gray;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw bottom border
            e.Graphics.DrawLine(Pens.LightGray, 0, this.Height - 1, this.Width, this.Height - 1);
        }
    }
}
