using System.Windows.Forms;
using System.Drawing;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    partial class UserManagementControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.titleLabel = new Label();
            this.searchTextBox = new TextBox();
            this.filterComboBox = new ComboBox();
            this.addUserButton = new Button();
            this.headerPanel = new Panel();
            this.usersFlowPanel = new FlowLayoutPanel();


            this.headerPanel.SuspendLayout();

            this.SuspendLayout();

            // 
            // searchTextBox
            // 
            this.searchTextBox.Font = new Font("Segoe UI", 10F);
            this.searchTextBox.Location = new Point(28, 20);
            this.searchTextBox.Size = new Size(300, 25);
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);

            // 
            // filterComboBox
            // 
            this.filterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.filterComboBox.Font = new Font("Segoe UI", 10F);
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Items.AddRange(new object[] {
            "All Users",
            "Managers",
            "Front Desk"});
            this.filterComboBox.Location = new Point(350, 20);
            this.filterComboBox.Size = new Size(150, 25);
            this.filterComboBox.SelectedIndex = 0;
            this.filterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);

            // 
            // addUserButton
            // 
            this.addUserButton.Text = "+ Add Employee";
            this.addUserButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.addUserButton.BackColor = Color.FromArgb(0, 122, 255);
            this.addUserButton.ForeColor = Color.White;
            this.addUserButton.FlatStyle = FlatStyle.Flat;
            this.addUserButton.FlatAppearance.BorderSize = 0;
            this.addUserButton.Location = new Point(520, 18);
            this.addUserButton.Size = new Size(160, 30);
            this.addUserButton.Cursor = Cursors.Hand;
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            this.addUserButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // 
            // headerPanel (List Header) - HIDDEN
            // 
            this.headerPanel.BackColor = Color.FromArgb(240, 240, 240);
            this.headerPanel.Location = new Point(28, 60);
            this.headerPanel.Size = new Size(1150, 40);
            this.headerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.headerPanel.Visible = false; // Hide as requested
            
            // Header Content is now dynamic (UserListItemControl)

            // 
            // usersFlowPanel
            // 
            this.usersFlowPanel.AutoScroll = true;
            this.usersFlowPanel.FlowDirection = FlowDirection.TopDown;
            this.usersFlowPanel.WrapContents = false;
            this.usersFlowPanel.Location = new Point(28, 60); 
            this.usersFlowPanel.Size = new Size(1150, 610); 
            this.usersFlowPanel.Padding = new Padding(0, 0, 20, 0); // Add padding on right for scrollbar
            this.usersFlowPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.usersFlowPanel.BackColor = Color.White;
            this.usersFlowPanel.AutoScroll = true; // Duplicate for safety in generated code snippets sometimes helps



            // 
            // UserManagementControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.filterComboBox);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.usersFlowPanel);

            this.Controls.Add(this.addUserButton); 
            this.Name = "UserManagementControl";
            this.Size = new Size(1250, 700);
            
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label titleLabel;
        private TextBox searchTextBox;
        private ComboBox filterComboBox;
        private Button addUserButton;
        private Panel headerPanel;
        private FlowLayoutPanel usersFlowPanel;

    }
}
