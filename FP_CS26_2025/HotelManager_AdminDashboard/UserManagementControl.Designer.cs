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
            this.paginationPanel = new Panel();
            this.prevButton = new Button();
            this.nextButton = new Button();
            this.pageLabel = new Label();

            this.headerPanel.SuspendLayout();
            this.paginationPanel.SuspendLayout();
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
            "Admins Only",
            "General Users Only"});
            this.filterComboBox.Location = new Point(350, 20);
            this.filterComboBox.Size = new Size(150, 25);
            this.filterComboBox.SelectedIndex = 0;
            this.filterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);

            // 
            // headerPanel (List Header)
            // 
            this.headerPanel.BackColor = Color.FromArgb(240, 240, 240);
            this.headerPanel.Location = new Point(28, 60);
            this.headerPanel.Size = new Size(1150, 40);
            this.headerPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            // Add static labels for headers here if needed (Name, Access, etc)
            Label hName = new Label() { Text = "User Name", Location = new Point(60, 10), Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true };
            
            // Access: Center 420. Width 100. X=370.
            Label hAccess = new Label() { Text = "Access", Location = new Point(370, 10), Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = false, Size = new Size(100, 25), TextAlign = ContentAlignment.MiddleCenter }; 
            
            // Last Active: Center 650. Width 140. X=580.
            Label hLastActive = new Label() { Text = "Last Active", Location = new Point(580, 10), Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = false, Size = new Size(140, 25), TextAlign = ContentAlignment.MiddleCenter };
            
            // Date Added: Center 880. Width 140. X=810.
            Label hDateAdded = new Label() { Text = "Date Added", Location = new Point(810, 10), Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = false, Size = new Size(140, 25), TextAlign = ContentAlignment.MiddleCenter };
            this.headerPanel.Controls.Add(hName);
            this.headerPanel.Controls.Add(hAccess);
            this.headerPanel.Controls.Add(hLastActive);
            this.headerPanel.Controls.Add(hDateAdded);

            // 
            // usersFlowPanel
            // 
            this.usersFlowPanel.AutoScroll = true;
            this.usersFlowPanel.FlowDirection = FlowDirection.TopDown;
            this.usersFlowPanel.WrapContents = false;
            this.usersFlowPanel.Location = new Point(28, 100);
            this.usersFlowPanel.Size = new Size(1150, 520);
            this.usersFlowPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.usersFlowPanel.BackColor = Color.White;

            // 
            // paginationPanel
            // 
            this.paginationPanel.Controls.Add(this.prevButton);
            this.paginationPanel.Controls.Add(this.pageLabel);
            this.paginationPanel.Controls.Add(this.nextButton);
            this.paginationPanel.Location = new Point(28, 630);
            this.paginationPanel.Size = new Size(1150, 50);
            this.paginationPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // prevButton
            this.prevButton.Text = "Previous";
            this.prevButton.Location = new Point(400, 10);
            this.prevButton.Click += (s, e) => ChangePage(-1);

            // nextButton
            this.nextButton.Text = "Next";
            this.nextButton.Location = new Point(600, 10);
            this.nextButton.Click += (s, e) => ChangePage(1);

            // pageLabel
            this.pageLabel.AutoSize = true;
            this.pageLabel.Location = new Point(520, 15);
            this.pageLabel.Text = "Page 1";

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
            this.Controls.Add(this.paginationPanel);
            this.Name = "UserManagementControl";
            this.Size = new Size(1250, 700);
            
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.paginationPanel.ResumeLayout(false);
            this.paginationPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label titleLabel;
        private TextBox searchTextBox;
        private ComboBox filterComboBox;
        private Button addUserButton;
        private Panel headerPanel;
        private FlowLayoutPanel usersFlowPanel;
        private Panel paginationPanel;
        private Button prevButton;
        private Button nextButton;
        private Label pageLabel;
    }
}
