using System;
using System.Drawing;
using System.Windows.Forms;

namespace FP_CS26_2025.Room_Rates___Policies
{
    partial class RoomRates_and_Pricing_Form
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelMain;
        private Label lblTitle;
        private DataGridView dgvRoomRates;
        private Button btnChangePrice;
        private Button btnRefresh;
        private TextBox txtSearch;
        private Label lblSearch;
        private GroupBox gbPolicies;
        private Label lblPolicyText;

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
            this.panelMain = new Panel();
            this.lblTitle = new Label();
            this.dgvRoomRates = new DataGridView();
            this.btnChangePrice = new Button();
            this.btnRefresh = new Button();
            this.txtSearch = new TextBox();
            this.lblSearch = new Label();
            this.gbPolicies = new GroupBox();
            this.lblPolicyText = new Label();

            // Main Panel
            this.panelMain.BackColor = Color.White;
            this.panelMain.Dock = DockStyle.Fill;
            this.panelMain.Padding = new Padding(20);

            // Title Label
            this.lblTitle.Text = "Room Rates & Policies";
            this.lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(20, 20);

            // Search Label
            this.lblSearch.Text = "Search Room:";
            this.lblSearch.Font = new Font("Segoe UI", 10F);
            this.lblSearch.Location = new Point(22, 70);
            this.lblSearch.AutoSize = true;

            // Search TextBox
            this.txtSearch.Font = new Font("Segoe UI", 10F);
            this.txtSearch.Location = new Point(120, 68);
            this.txtSearch.Width = 250;

            // DataGridView
            this.dgvRoomRates.Location = new Point(20, 110);
            this.dgvRoomRates.Size = new Size(650, 300);
            this.dgvRoomRates.AllowUserToAddRows = false;
            this.dgvRoomRates.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoomRates.MultiSelect = false;

            this.dgvRoomRates.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.dgvRoomRates.DefaultCellStyle.Font = new Font("Segoe UI", 10F);

            this.dgvRoomRates.Columns.Add("RoomType", "Room Type");
            this.dgvRoomRates.Columns.Add("Price", "Pricing");
            this.dgvRoomRates.Columns.Add("Availability", "Availability");
            this.dgvRoomRates.Columns.Add("Capacity", "Capacity Info");

            // Buttons
            this.btnChangePrice.Text = "Change Pricing";
            this.btnChangePrice.Font = new Font("Segoe UI", 10F);
            this.btnChangePrice.Size = new Size(150, 40);
            this.btnChangePrice.Location = new Point(20, 420);
            this.btnChangePrice.FlatStyle = FlatStyle.Flat;

            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Font = new Font("Segoe UI", 10F);
            this.btnRefresh.Size = new Size(120, 40);
            this.btnRefresh.Location = new Point(180, 420);
            this.btnRefresh.FlatStyle = FlatStyle.Flat;

            // Policies
            this.gbPolicies.Text = "Policies";
            this.gbPolicies.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.gbPolicies.Location = new Point(700, 110);
            this.gbPolicies.Size = new Size(350, 350);

            this.lblPolicyText.Text =
                "• Check-in: 2:00 PM\n" +
                "• Check-out: 12:00 PM\n" +
                "• No smoking inside rooms\n" +
                "• Pets allowed only in designated rooms\n" +
                "• Free cancellation up to 24 hours";

            this.lblPolicyText.Font = new Font("Segoe UI", 10F);
            this.lblPolicyText.AutoSize = true;
            this.lblPolicyText.Location = new Point(15, 30);

            // Add Controls
            this.gbPolicies.Controls.Add(this.lblPolicyText);
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblSearch);
            this.panelMain.Controls.Add(this.txtSearch);
            this.panelMain.Controls.Add(this.dgvRoomRates);
            this.panelMain.Controls.Add(this.btnChangePrice);
            this.panelMain.Controls.Add(this.btnRefresh);
            this.panelMain.Controls.Add(this.gbPolicies);
            this.Controls.Add(this.panelMain);

            // Form Settings
            this.Text = "Room Rates & Policies";
            this.Load += new EventHandler(RoomRates_and_Pricing_Form_Load);
            this.ClientSize = new Size(1100, 600);
        }
    }
}