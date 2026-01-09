namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    partial class PromoManagementControl
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
            this.dgvPromos = new System.Windows.Forms.DataGridView();
            this.pnlAddPromo = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.numValue = new System.Windows.Forms.NumericUpDown();
            this.lblValue = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromos)).BeginInit();
            this.pnlAddPromo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPromos
            // 
            this.dgvPromos.AllowUserToAddRows = false;
            this.dgvPromos.AllowUserToDeleteRows = false;
            this.dgvPromos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPromos.BackgroundColor = System.Drawing.Color.White;
            this.dgvPromos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPromos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPromos.Location = new System.Drawing.Point(20, 60);
            this.dgvPromos.Name = "dgvPromos";
            this.dgvPromos.ReadOnly = true;
            this.dgvPromos.RowHeadersVisible = false;
            this.dgvPromos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPromos.Size = new System.Drawing.Size(550, 400);
            this.dgvPromos.TabIndex = 0;
            // 
            // pnlAddPromo
            // 
            this.pnlAddPromo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlAddPromo.Controls.Add(this.btnSave);
            this.pnlAddPromo.Controls.Add(this.dtpExpiry);
            this.pnlAddPromo.Controls.Add(this.lblExpiry);
            this.pnlAddPromo.Controls.Add(this.numValue);
            this.pnlAddPromo.Controls.Add(this.lblValue);
            this.pnlAddPromo.Controls.Add(this.cmbType);
            this.pnlAddPromo.Controls.Add(this.lblType);
            this.pnlAddPromo.Controls.Add(this.txtCode);
            this.pnlAddPromo.Controls.Add(this.lblCode);
            this.pnlAddPromo.Location = new System.Drawing.Point(590, 60);
            this.pnlAddPromo.Name = "pnlAddPromo";
            this.pnlAddPromo.Size = new System.Drawing.Size(250, 350);
            this.pnlAddPromo.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(210, 40);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "GENERATE CODE";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpiry.Location = new System.Drawing.Point(20, 230);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(210, 25);
            this.dtpExpiry.TabIndex = 7;
            // 
            // lblExpiry
            // 
            this.lblExpiry.AutoSize = true;
            this.lblExpiry.Location = new System.Drawing.Point(20, 210);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Size = new System.Drawing.Size(74, 17);
            this.lblExpiry.TabIndex = 6;
            this.lblExpiry.Text = "Expiry Date";
            // 
            // numValue
            // 
            this.numValue.DecimalPlaces = 2;
            this.numValue.Location = new System.Drawing.Point(20, 170);
            this.numValue.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numValue.Name = "numValue";
            this.numValue.Size = new System.Drawing.Size(210, 25);
            this.numValue.TabIndex = 5;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(20, 150);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(95, 17);
            this.lblValue.TabIndex = 4;
            this.lblValue.Text = "Discount Value";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Percentage",
            "Fixed"});
            this.cmbType.Location = new System.Drawing.Point(20, 110);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(210, 25);
            this.cmbType.TabIndex = 3;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(20, 90);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(90, 17);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Discount Type";
            // 
            // txtCode
            // 
            this.txtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCode.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCode.Location = new System.Drawing.Point(20, 50);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(210, 25);
            this.txtCode.TabIndex = 1;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(20, 30);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(83, 17);
            this.lblCode.TabIndex = 0;
            this.lblCode.Text = "Promo Code";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(325, 30);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "PROMO CODE MANAGEMENT";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(20, 470);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "DELETE SELECTED";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // PromoManagementControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlAddPromo);
            this.Controls.Add(this.dgvPromos);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PromoManagementControl";
            this.Size = new System.Drawing.Size(870, 520);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPromos)).EndInit();
            this.pnlAddPromo.ResumeLayout(false);
            this.pnlAddPromo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dgvPromos;
        private System.Windows.Forms.Panel pnlAddPromo;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.NumericUpDown numValue;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnDelete;
    }
}
