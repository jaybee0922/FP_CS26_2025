namespace FP_CS26_2025.Room_Rates___Policies
{
    partial class RoomRatesControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvRoomRates;
        private System.Windows.Forms.Button btnChangePrice;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnRoomCategories;
        private System.Windows.Forms.Button btnRoomInventory;
        private System.Windows.Forms.Button btnAddRoom;
        private System.Windows.Forms.DataGridView dgvRoomInventory;
        private System.Windows.Forms.GroupBox gbPolicies;
        private System.Windows.Forms.Label lblPolicyText;
        private System.Windows.Forms.DataGridViewImageColumn colImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dgvRoomRates = new System.Windows.Forms.DataGridView();
            this.colImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnChangePrice = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gbPolicies = new System.Windows.Forms.GroupBox();
            this.lblPolicyText = new System.Windows.Forms.Label();
            this.btnRoomCategories = new System.Windows.Forms.Button();
            this.btnRoomInventory = new System.Windows.Forms.Button();
            this.btnAddRoom = new System.Windows.Forms.Button();
            this.dgvRoomInventory = new System.Windows.Forms.DataGridView();
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
            this.panelMain.Controls.Add(this.btnRoomInventory);
            this.panelMain.Controls.Add(this.btnAddRoom);
            this.panelMain.Controls.Add(this.dgvRoomInventory);
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
            this.lblTitle.Text = "Room Rates and Policies";
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
            this.dgvRoomRates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoomRates.BackgroundColor = System.Drawing.Color.White;
            this.dgvRoomRates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRoomRates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRoomRates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colImage,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRoomRates.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRoomRates.Location = new System.Drawing.Point(20, 110);
            this.dgvRoomRates.MultiSelect = false;
            this.dgvRoomRates.Name = "dgvRoomRates";
            this.dgvRoomRates.RowHeadersVisible = false;
            this.dgvRoomRates.RowTemplate.Height = 80;
            this.dgvRoomRates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoomRates.Size = new System.Drawing.Size(800, 450);
            this.dgvRoomRates.TabIndex = 3;
            // 
            // colImage
            // 
            this.colImage.HeaderText = "Room Image";
            this.colImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.colImage.Name = "colImage";
            this.colImage.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Room Type";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Pricing";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Availability";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Capacity Info";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // btnChangePrice
            // 
            this.btnChangePrice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePrice.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnChangePrice.Location = new System.Drawing.Point(20, 580);
            this.btnChangePrice.Name = "btnChangePrice";
            this.btnChangePrice.Size = new System.Drawing.Size(150, 40);
            this.btnChangePrice.TabIndex = 4;
            this.btnChangePrice.Text = "Configure Room";
            this.btnChangePrice.UseVisualStyleBackColor = true;
            this.btnChangePrice.Click += new System.EventHandler(this.btnChangePrice_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnRefresh.Location = new System.Drawing.Point(180, 580);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 40);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // gbPolicies
            // 
            this.gbPolicies.Controls.Add(this.lblPolicyText);
            this.gbPolicies.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbPolicies.Location = new System.Drawing.Point(850, 110);
            this.gbPolicies.Name = "gbPolicies";
            this.gbPolicies.Size = new System.Drawing.Size(300, 450);
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

            // 
            // btnRoomInventory
            // 
            this.btnRoomInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnRoomInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomInventory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRoomInventory.ForeColor = System.Drawing.Color.White;
            this.btnRoomInventory.Location = new System.Drawing.Point(680, 20);
            this.btnRoomInventory.Name = "btnRoomInventory";
            this.btnRoomInventory.Size = new System.Drawing.Size(140, 35);
            this.btnRoomInventory.TabIndex = 8;
            this.btnRoomInventory.Text = "Room Inventory";
            this.btnRoomInventory.UseVisualStyleBackColor = false;
            this.btnRoomInventory.Click += new System.EventHandler(this.btnRoomInventory_Click);
            // 
            // btnAddRoom
            // 
            this.btnAddRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRoom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAddRoom.Location = new System.Drawing.Point(310, 580);
            this.btnAddRoom.Name = "btnAddRoom";
            this.btnAddRoom.Size = new System.Drawing.Size(120, 40);
            this.btnAddRoom.TabIndex = 9;
            this.btnAddRoom.Text = "Add Room";
            this.btnAddRoom.UseVisualStyleBackColor = true;
            this.btnAddRoom.Visible = false;
            this.btnAddRoom.Click += new System.EventHandler(this.btnAddRoom_Click);
            // 
            // dgvRoomInventory
            // 
            this.dgvRoomInventory.AllowUserToAddRows = false;
            this.dgvRoomInventory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoomInventory.BackgroundColor = System.Drawing.Color.White;
            this.dgvRoomInventory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRoomInventory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoomInventory.Location = new System.Drawing.Point(20, 110);
            this.dgvRoomInventory.MultiSelect = false;
            this.dgvRoomInventory.Name = "dgvRoomInventory";
            this.dgvRoomInventory.ReadOnly = true;
            this.dgvRoomInventory.RowHeadersVisible = false;
            this.dgvRoomInventory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoomInventory.Size = new System.Drawing.Size(800, 450);
            this.dgvRoomInventory.TabIndex = 10;
            this.dgvRoomInventory.Visible = false;
            this.dgvRoomInventory.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRoomInventory_CellDoubleClick);
            // 
            // RoomRatesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelMain);
            this.Name = "RoomRatesControl";
            this.Size = new System.Drawing.Size(1100, 600);
            this.Load += new System.EventHandler(this.RoomRatesControl_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoomRates)).EndInit();
            this.gbPolicies.ResumeLayout(false);
            this.gbPolicies.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
