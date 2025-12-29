namespace FP_CS26_2025.ModernDesign
{
    partial class BookingModalForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.lblLastName = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.lblAdults = new System.Windows.Forms.Label();
            this.numAdults = new System.Windows.Forms.NumericUpDown();
            this.lblChildren = new System.Windows.Forms.Label();
            this.numChildren = new System.Windows.Forms.NumericUpDown();
            this.lblRooms = new System.Windows.Forms.Label();
            this.numRooms = new System.Windows.Forms.NumericUpDown();
            this.lblRoomType = new System.Windows.Forms.Label();
            this.cmbRoomType = new System.Windows.Forms.ComboBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnBookNow = new FP_CS26_2025.ModernDesign.ModernButton();
            this.pnlVisual = new System.Windows.Forms.Panel();
            this.lblRoomDesc = new System.Windows.Forms.Label();
            this.picRoom = new System.Windows.Forms.PictureBox();
            this.lblRoomSelectTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numAdults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChildren)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRooms)).BeginInit();
            this.pnlVisual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRoom)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(400, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(326, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reservations";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFirstName.ForeColor = System.Drawing.Color.Silver;
            this.lblFirstName.Location = new System.Drawing.Point(410, 110);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(80, 20);
            this.lblFirstName.TabIndex = 1;
            this.lblFirstName.Text = "First Name";
            // 
            // txtFirstName
            // 
            this.txtFirstName.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.txtFirstName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFirstName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFirstName.ForeColor = System.Drawing.Color.White;
            this.txtFirstName.Location = new System.Drawing.Point(410, 135);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(185, 30);
            this.txtFirstName.TabIndex = 2;
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLastName.ForeColor = System.Drawing.Color.Silver;
            this.lblLastName.Location = new System.Drawing.Point(615, 110);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(79, 20);
            this.lblLastName.TabIndex = 3;
            this.lblLastName.Text = "Last Name";
            // 
            // txtLastName
            // 
            this.txtLastName.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.txtLastName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLastName.ForeColor = System.Drawing.Color.White;
            this.txtLastName.Location = new System.Drawing.Point(615, 135);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(185, 30);
            this.txtLastName.TabIndex = 4;
            // 
            // lblAdults
            // 
            this.lblAdults.AutoSize = true;
            this.lblAdults.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAdults.ForeColor = System.Drawing.Color.Silver;
            this.lblAdults.Location = new System.Drawing.Point(410, 185);
            this.lblAdults.Name = "lblAdults";
            this.lblAdults.Size = new System.Drawing.Size(51, 20);
            this.lblAdults.TabIndex = 5;
            this.lblAdults.Text = "Adults";
            // 
            // numAdults
            // 
            this.numAdults.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.numAdults.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numAdults.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numAdults.ForeColor = System.Drawing.Color.White;
            this.numAdults.Location = new System.Drawing.Point(410, 210);
            this.numAdults.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numAdults.Name = "numAdults";
            this.numAdults.Size = new System.Drawing.Size(80, 30);
            this.numAdults.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblChildren
            // 
            this.lblChildren.AutoSize = true;
            this.lblChildren.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblChildren.ForeColor = System.Drawing.Color.Silver;
            this.lblChildren.Location = new System.Drawing.Point(510, 185);
            this.lblChildren.Name = "lblChildren";
            this.lblChildren.Size = new System.Drawing.Size(64, 20);
            this.lblChildren.TabIndex = 6;
            this.lblChildren.Text = "Children";
            // 
            // numChildren
            // 
            this.numChildren.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.numChildren.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numChildren.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numChildren.ForeColor = System.Drawing.Color.White;
            this.numChildren.Location = new System.Drawing.Point(510, 210);
            this.numChildren.Name = "numChildren";
            this.numChildren.Size = new System.Drawing.Size(80, 30);
            // 
            // lblRooms
            // 
            this.lblRooms.AutoSize = true;
            this.lblRooms.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRooms.ForeColor = System.Drawing.Color.Silver;
            this.lblRooms.Location = new System.Drawing.Point(615, 185);
            this.lblRooms.Name = "lblRooms";
            this.lblRooms.Size = new System.Drawing.Size(55, 20);
            this.lblRooms.TabIndex = 7;
            this.lblRooms.Text = "Rooms";
            // 
            // numRooms
            // 
            this.numRooms.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.numRooms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numRooms.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numRooms.ForeColor = System.Drawing.Color.White;
            this.numRooms.Location = new System.Drawing.Point(615, 210);
            this.numRooms.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numRooms.Name = "numRooms";
            this.numRooms.Size = new System.Drawing.Size(80, 30);
            this.numRooms.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblRoomType
            // 
            this.lblRoomType.AutoSize = true;
            this.lblRoomType.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRoomType.ForeColor = System.Drawing.Color.Silver;
            this.lblRoomType.Location = new System.Drawing.Point(410, 260);
            this.lblRoomType.Name = "lblRoomType";
            this.lblRoomType.Size = new System.Drawing.Size(126, 20);
            this.lblRoomType.TabIndex = 8;
            this.lblRoomType.Text = "Select Your Room";
            // 
            // cmbRoomType
            // 
            this.cmbRoomType.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.cmbRoomType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRoomType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRoomType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbRoomType.ForeColor = System.Drawing.Color.White;
            this.cmbRoomType.FormattingEnabled = true;
            this.cmbRoomType.Location = new System.Drawing.Point(410, 285);
            this.cmbRoomType.Name = "cmbRoomType";
            this.cmbRoomType.Size = new System.Drawing.Size(390, 31);
            this.cmbRoomType.TabIndex = 9;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.ForeColor = System.Drawing.Color.Silver;
            this.lblEmail.Location = new System.Drawing.Point(410, 335);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(103, 20);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "Email Address";
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.ForeColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(410, 360);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(390, 30);
            this.txtEmail.TabIndex = 11;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPhone.ForeColor = System.Drawing.Color.Silver;
            this.lblPhone.Location = new System.Drawing.Point(410, 410);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(108, 20);
            this.lblPhone.TabIndex = 12;
            this.lblPhone.Text = "Phone Number";
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.ForeColor = System.Drawing.Color.White;
            this.txtPhone.Location = new System.Drawing.Point(410, 435);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(390, 30);
            this.txtPhone.TabIndex = 13;
            // 
            // btnBookNow
            // 
            this.btnBookNow.BackColor = System.Drawing.Color.FromArgb(106, 176, 76);
            this.btnBookNow.BorderColor = System.Drawing.Color.Empty;
            this.btnBookNow.BorderRadius = 8;
            this.btnBookNow.FlatAppearance.BorderSize = 0;
            this.btnBookNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBookNow.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBookNow.ForeColor = System.Drawing.Color.White;
            this.btnBookNow.Location = new System.Drawing.Point(410, 500);
            this.btnBookNow.Name = "btnBookNow";
            this.btnBookNow.Size = new System.Drawing.Size(390, 50);
            this.btnBookNow.TabIndex = 14;
            this.btnBookNow.Text = "CONFIRM BOOKING";
            this.btnBookNow.UseVisualStyleBackColor = false;
            // 
            // pnlVisual
            // 
            this.pnlVisual.BackColor = System.Drawing.Color.FromArgb(30, 35, 50);
            this.pnlVisual.Controls.Add(this.lblRoomSelectTitle);
            this.pnlVisual.Controls.Add(this.lblRoomDesc);
            this.pnlVisual.Controls.Add(this.picRoom);
            this.pnlVisual.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlVisual.Location = new System.Drawing.Point(0, 0);
            this.pnlVisual.Name = "pnlVisual";
            this.pnlVisual.Size = new System.Drawing.Size(370, 580);
            this.pnlVisual.TabIndex = 15;
            // 
            // lblRoomDesc
            // 
            this.lblRoomDesc.Font = new System.Drawing.Font("Segoe UI Light", 11F);
            this.lblRoomDesc.ForeColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.lblRoomDesc.Location = new System.Drawing.Point(20, 335);
            this.lblRoomDesc.Name = "lblRoomDesc";
            this.lblRoomDesc.Size = new System.Drawing.Size(330, 200);
            this.lblRoomDesc.TabIndex = 1;
            this.lblRoomDesc.Text = "Please select a room type to see its unique features and luxurious amenities.";
            // 
            // picRoom
            // 
            this.picRoom.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.picRoom.Location = new System.Drawing.Point(0, 0);
            this.picRoom.Name = "picRoom";
            this.picRoom.Size = new System.Drawing.Size(370, 270);
            this.picRoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picRoom.TabIndex = 0;
            this.picRoom.TabStop = false;
            // 
            // lblRoomSelectTitle
            // 
            this.lblRoomSelectTitle.AutoSize = true;
            this.lblRoomSelectTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblRoomSelectTitle.ForeColor = System.Drawing.Color.White;
            this.lblRoomSelectTitle.Location = new System.Drawing.Point(20, 290);
            this.lblRoomSelectTitle.Name = "lblRoomSelectTitle";
            this.lblRoomSelectTitle.Size = new System.Drawing.Size(189, 37);
            this.lblRoomSelectTitle.TabIndex = 2;
            this.lblRoomSelectTitle.Text = "Room Details";
            // 
            // BookingModalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(25, 30, 50);
            this.ClientSize = new System.Drawing.Size(840, 580);
            this.Controls.Add(this.pnlVisual);
            this.Controls.Add(this.btnBookNow);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.cmbRoomType);
            this.Controls.Add(this.lblRoomType);
            this.Controls.Add(this.numRooms);
            this.Controls.Add(this.lblRooms);
            this.Controls.Add(this.numChildren);
            this.Controls.Add(this.lblChildren);
            this.Controls.Add(this.numAdults);
            this.Controls.Add(this.lblAdults);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BookingModalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "The Nexus Grand - Booking";
            ((System.ComponentModel.ISupportInitialize)(this.numAdults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChildren)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRooms)).EndInit();
            this.pnlVisual.ResumeLayout(false);
            this.pnlVisual.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAdults;
        private System.Windows.Forms.NumericUpDown numAdults;
        private System.Windows.Forms.Label lblChildren;
        private System.Windows.Forms.NumericUpDown numChildren;
        private System.Windows.Forms.Label lblRooms;
        private System.Windows.Forms.NumericUpDown numRooms;
        private System.Windows.Forms.Label lblRoomType;
        private System.Windows.Forms.ComboBox cmbRoomType;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private FP_CS26_2025.ModernDesign.ModernButton btnBookNow;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.Panel pnlVisual;
        private System.Windows.Forms.Label lblRoomDesc;
        private System.Windows.Forms.PictureBox picRoom;
        private System.Windows.Forms.Label lblRoomSelectTitle;
    }
}
