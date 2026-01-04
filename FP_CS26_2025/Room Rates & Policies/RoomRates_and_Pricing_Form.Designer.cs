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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvRoomRates = new System.Windows.Forms.DataGridView();
            this.btnChangePrice = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gbPolicies = new System.Windows.Forms.GroupBox();
            this.lblPolicyText = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomRates)).BeginInit();
            this.gbPolicies.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.lblTitle);
            this.panelMain.Controls.Add(this.lblSearch);
            this.panelMain.Controls.Add(this.txtSearch);
            this.panelMain.Controls.Add(this.dgvRoomRates);
            this.panelMain.Controls.Add(this.btnChangePrice);
            this.panelMain.Controls.Add(this.btnRefresh);
            this.panelMain.Controls.Add(this.gbPolicies);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(1100, 600);
            this.panelMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Room Rates & Policies";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSearch.Location = new System.Drawing.Point(22, 70);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(92, 19);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search Room:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.Location = new System.Drawing.Point(120, 68);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(250, 25);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // dgvRoomRates
            // 
            this.dgvRoomRates.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoomRates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRoomRates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRoomRates.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRoomRates.Location = new System.Drawing.Point(20, 110);
            this.dgvRoomRates.MultiSelect = false;
            this.dgvRoomRates.Name = "dgvRoomRates";
            this.dgvRoomRates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoomRates.Size = new System.Drawing.Size(650, 300);
            this.dgvRoomRates.TabIndex = 3;
            // 
            // btnChangePrice
            // 
            this.btnChangePrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnChangePrice.Location = new System.Drawing.Point(20, 420);
            this.btnChangePrice.Name = "btnChangePrice";
            this.btnChangePrice.Size = new System.Drawing.Size(150, 40);
            this.btnChangePrice.TabIndex = 4;
            this.btnChangePrice.Text = "Change Pricing";
            this.btnChangePrice.Click += new System.EventHandler(this.btnChangePrice_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefresh.Location = new System.Drawing.Point(180, 420);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // gbPolicies
            // 
            this.gbPolicies.Controls.Add(this.lblPolicyText);
            this.gbPolicies.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbPolicies.Location = new System.Drawing.Point(700, 110);
            this.gbPolicies.Name = "gbPolicies";
            this.gbPolicies.Size = new System.Drawing.Size(350, 350);
            this.gbPolicies.TabIndex = 6;
            this.gbPolicies.TabStop = false;
            this.gbPolicies.Text = "Policies";
            // 
            // lblPolicyText
            // 
            this.lblPolicyText.AutoSize = true;
            this.lblPolicyText.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPolicyText.Location = new System.Drawing.Point(15, 30);
            this.lblPolicyText.Name = "lblPolicyText";
            this.lblPolicyText.Size = new System.Drawing.Size(253, 95);
            this.lblPolicyText.TabIndex = 0;
            this.lblPolicyText.Text = "• Check-in: 2:00 PM\n• Check-out: 12:00 PM\n• No smoking inside rooms\n• Pets allowe" +
    "d only in designated rooms\n• Free cancellation up to 24 hours";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Room Type";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Pricing";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Availability";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Capacity Info";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // RoomRates_and_Pricing_Form
            // 
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.panelMain);
            this.Name = "RoomRates_and_Pricing_Form";
            this.Text = "git ";
            this.Load += new System.EventHandler(this.RoomRates_and_Pricing_Form_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomRates)).EndInit();
            this.gbPolicies.ResumeLayout(false);
            this.gbPolicies.PerformLayout();
            this.ResumeLayout(false);

        }

        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}