namespace FP_CS26_2025.Room_Rates___Policies
{
    partial class ManageRoomForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.TextBox txtRoomNumber;
        private System.Windows.Forms.Label lblFloor;
        private System.Windows.Forms.NumericUpDown numFloor;
        private System.Windows.Forms.Label lblRoomType;
        private System.Windows.Forms.ComboBox cmbRoomType;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblBedConfig;
        private System.Windows.Forms.ComboBox cmbBedConfig;
        private System.Windows.Forms.Label lblViewType;
        private System.Windows.Forms.ComboBox cmbViewType;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

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
            this.lblRoomNumber = new System.Windows.Forms.Label();
            this.txtRoomNumber = new System.Windows.Forms.TextBox();
            this.lblFloor = new System.Windows.Forms.Label();
            this.numFloor = new System.Windows.Forms.NumericUpDown();
            this.lblRoomType = new System.Windows.Forms.Label();
            this.cmbRoomType = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblBedConfig = new System.Windows.Forms.Label();
            this.cmbBedConfig = new System.Windows.Forms.ComboBox();
            this.lblViewType = new System.Windows.Forms.Label();
            this.cmbViewType = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numFloor)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRoomNumber.Location = new System.Drawing.Point(30, 30);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(102, 19);
            this.lblRoomNumber.TabIndex = 0;
            this.lblRoomNumber.Text = "Room Number:";
            // 
            // txtRoomNumber
            // 
            this.txtRoomNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtRoomNumber.Location = new System.Drawing.Point(150, 27);
            this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.Size = new System.Drawing.Size(200, 25);
            this.txtRoomNumber.TabIndex = 1;
            // 
            // lblFloor
            // 
            this.lblFloor.AutoSize = true;
            this.lblFloor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFloor.Location = new System.Drawing.Point(30, 75);
            this.lblFloor.Name = "lblFloor";
            this.lblFloor.Size = new System.Drawing.Size(43, 19);
            this.lblFloor.TabIndex = 2;
            this.lblFloor.Text = "Floor:";
            // 
            // numFloor
            // 
            this.numFloor.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numFloor.Location = new System.Drawing.Point(150, 73);
            this.numFloor.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numFloor.Name = "numFloor";
            this.numFloor.Size = new System.Drawing.Size(100, 25);
            this.numFloor.TabIndex = 3;
            this.numFloor.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblRoomType
            // 
            this.lblRoomType.AutoSize = true;
            this.lblRoomType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRoomType.Location = new System.Drawing.Point(30, 120);
            this.lblRoomType.Name = "lblRoomType";
            this.lblRoomType.Size = new System.Drawing.Size(81, 19);
            this.lblRoomType.TabIndex = 4;
            this.lblRoomType.Text = "Room Type:";
            // 
            // cmbRoomType
            // 
            this.cmbRoomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoomType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRoomType.FormattingEnabled = true;
            this.cmbRoomType.Location = new System.Drawing.Point(150, 117);
            this.cmbRoomType.Name = "cmbRoomType";
            this.cmbRoomType.Size = new System.Drawing.Size(200, 25);
            this.cmbRoomType.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(30, 165);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 19);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(150, 162);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 25);
            this.cmbStatus.TabIndex = 7;
            // 
            // lblBedConfig
            // 
            this.lblBedConfig.AutoSize = true;
            this.lblBedConfig.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBedConfig.Location = new System.Drawing.Point(30, 210);
            this.lblBedConfig.Name = "lblBedConfig";
            this.lblBedConfig.Size = new System.Drawing.Size(80, 19);
            this.lblBedConfig.TabIndex = 8;
            this.lblBedConfig.Text = "Bed Config:";
            // 
            // cmbBedConfig
            // 
            this.cmbBedConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBedConfig.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbBedConfig.FormattingEnabled = true;
            this.cmbBedConfig.Location = new System.Drawing.Point(150, 207);
            this.cmbBedConfig.Name = "cmbBedConfig";
            this.cmbBedConfig.Size = new System.Drawing.Size(200, 25);
            this.cmbBedConfig.TabIndex = 9;
            // 
            // lblViewType
            // 
            this.lblViewType.AutoSize = true;
            this.lblViewType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblViewType.Location = new System.Drawing.Point(30, 255);
            this.lblViewType.Name = "lblViewType";
            this.lblViewType.Size = new System.Drawing.Size(73, 19);
            this.lblViewType.TabIndex = 10;
            this.lblViewType.Text = "View Type:";
            // 
            // cmbViewType
            // 
            this.cmbViewType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbViewType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbViewType.FormattingEnabled = true;
            this.cmbViewType.Location = new System.Drawing.Point(150, 252);
            this.cmbViewType.Name = "cmbViewType";
            this.cmbViewType.Size = new System.Drawing.Size(200, 25);
            this.cmbViewType.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(150, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 35);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(260, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 35);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ManageRoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 390);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbViewType);
            this.Controls.Add(this.lblViewType);
            this.Controls.Add(this.cmbBedConfig);
            this.Controls.Add(this.lblBedConfig);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.cmbRoomType);
            this.Controls.Add(this.lblRoomType);
            this.Controls.Add(this.numFloor);
            this.Controls.Add(this.lblFloor);
            this.Controls.Add(this.txtRoomNumber);
            this.Controls.Add(this.lblRoomNumber);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ManageRoomForm";
            this.Text = "Manage Room";
            ((System.ComponentModel.ISupportInitialize)(this.numFloor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
