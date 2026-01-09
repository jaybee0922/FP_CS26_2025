namespace FP_CS26_2025.ModernDesign
{
    partial class BookingReceiptForm
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
            this.lblHotelName = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblLine = new System.Windows.Forms.Label();
            this.lblDetailsHeader = new System.Windows.Forms.Label();
            this.lblGuestInfo = new System.Windows.Forms.Label();
            this.lblRoomInfo = new System.Windows.Forms.Label();
            this.lblDatesInfo = new System.Windows.Forms.Label();
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.lblReceiptDiscount = new System.Windows.Forms.Label();
            this.picRoomReceipt = new System.Windows.Forms.PictureBox();
            this.lblPoliciesHeader = new System.Windows.Forms.Label();
            this.lblPoliciesText = new System.Windows.Forms.Label();
            this.lblStatusMsg = new System.Windows.Forms.Label();
            this.lblStaffContact = new System.Windows.Forms.Label();
            this.btnClose = new FP_CS26_2025.ModernDesign.ModernButton();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHotelName
            // 
            this.lblHotelName.AutoSize = true;
            this.lblHotelName.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblHotelName.ForeColor = System.Drawing.Color.White;
            this.lblHotelName.Location = new System.Drawing.Point(50, 20);
            this.lblHotelName.Name = "lblHotelName";
            this.lblHotelName.Size = new System.Drawing.Size(325, 46);
            this.lblHotelName.TabIndex = 0;
            this.lblHotelName.Text = "THE NEXUS GRAND";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAddress.ForeColor = System.Drawing.Color.Silver;
            this.lblAddress.Location = new System.Drawing.Point(55, 66);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(380, 20);
            this.lblAddress.TabIndex = 1;
            this.lblAddress.Text = "123 Coastal Road, Bay Area, Metro Manila, Philippines";
            // 
            // lblLine
            // 
            this.lblLine.BackColor = System.Drawing.Color.FromArgb(106, 176, 76);
            this.lblLine.Location = new System.Drawing.Point(50, 100);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(460, 2);
            this.lblLine.TabIndex = 2;
            // 
            // lblDetailsHeader
            // 
            this.lblDetailsHeader.AutoSize = true;
            this.lblDetailsHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailsHeader.ForeColor = System.Drawing.Color.White;
            this.lblDetailsHeader.Location = new System.Drawing.Point(50, 320);
            this.lblDetailsHeader.Name = "lblDetailsHeader";
            this.lblDetailsHeader.Size = new System.Drawing.Size(193, 28);
            this.lblDetailsHeader.TabIndex = 3;
            this.lblDetailsHeader.Text = "Reservation Details";
            // 
            // picRoomReceipt
            // 
            this.picRoomReceipt.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.picRoomReceipt.Location = new System.Drawing.Point(50, 115);
            this.picRoomReceipt.Name = "picRoomReceipt";
            this.picRoomReceipt.Size = new System.Drawing.Size(460, 190);
            this.picRoomReceipt.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picRoomReceipt.TabIndex = 14;
            this.picRoomReceipt.TabStop = false;
            // 
            // lblGuestInfo
            // 
            this.lblGuestInfo.AutoSize = true;
            this.lblGuestInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGuestInfo.ForeColor = System.Drawing.Color.LightGray;
            this.lblGuestInfo.Location = new System.Drawing.Point(55, 360);
            this.lblGuestInfo.Name = "lblGuestInfo";
            this.lblGuestInfo.Size = new System.Drawing.Size(107, 23);
            this.lblGuestInfo.TabIndex = 4;
            this.lblGuestInfo.Text = "Guest: Name";
            // 
            // lblRoomInfo
            // 
            this.lblRoomInfo.AutoSize = true;
            this.lblRoomInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRoomInfo.ForeColor = System.Drawing.Color.LightGray;
            this.lblRoomInfo.Location = new System.Drawing.Point(55, 390);
            this.lblRoomInfo.Name = "lblRoomInfo";
            this.lblRoomInfo.Size = new System.Drawing.Size(223, 23);
            this.lblRoomInfo.TabIndex = 5;
            this.lblRoomInfo.Text = "Room: 1 x Suite - 2 Adults";
            // 
            // lblDatesInfo
            // 
            this.lblDatesInfo.AutoSize = true;
            this.lblDatesInfo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDatesInfo.ForeColor = System.Drawing.Color.LightGray;
            this.lblDatesInfo.Location = new System.Drawing.Point(55, 420);
            this.lblDatesInfo.Name = "lblDatesInfo";
            this.lblDatesInfo.Size = new System.Drawing.Size(262, 23);
            this.lblDatesInfo.TabIndex = 6;
            this.lblDatesInfo.Text = "Dates: 2025-12-25 to 2025-12-27";
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalPrice.ForeColor = System.Drawing.Color.White;
            this.lblTotalPrice.Location = new System.Drawing.Point(50, 460);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(400, 32);
            this.lblTotalPrice.TabIndex = 7;
            this.lblTotalPrice.Text = "Total Price: P0.00";
            // 
            // lblReceiptDiscount
            // 
            this.lblReceiptDiscount.AutoSize = true;
            this.lblReceiptDiscount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblReceiptDiscount.ForeColor = System.Drawing.Color.FromArgb(150, 200, 150);
            this.lblReceiptDiscount.Location = new System.Drawing.Point(55, 295);
            this.lblReceiptDiscount.Name = "lblReceiptDiscount";
            this.lblReceiptDiscount.Size = new System.Drawing.Size(125, 20);
            this.lblReceiptDiscount.TabIndex = 11;
            this.lblReceiptDiscount.Text = "(Promo Applied)";
            this.lblReceiptDiscount.Visible = false;
            this.lblReceiptDiscount.Location = new System.Drawing.Point(55, 495);
            // 
            // lblPoliciesHeader
            // 
            this.lblPoliciesHeader.AutoSize = true;
            this.lblPoliciesHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPoliciesHeader.ForeColor = System.Drawing.Color.White;
            this.lblPoliciesHeader.Location = new System.Drawing.Point(50, 535);
            this.lblPoliciesHeader.Name = "lblPoliciesHeader";
            this.lblPoliciesHeader.Size = new System.Drawing.Size(134, 25);
            this.lblPoliciesHeader.TabIndex = 12;
            this.lblPoliciesHeader.Text = "Hotel Policies";
            // 
            // lblPoliciesText
            // 
            this.lblPoliciesText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPoliciesText.ForeColor = System.Drawing.Color.Silver;
            this.lblPoliciesText.Location = new System.Drawing.Point(50, 565);
            this.lblPoliciesText.Name = "lblPoliciesText";
            this.lblPoliciesText.Size = new System.Drawing.Size(460, 130);
            this.lblPoliciesText.TabIndex = 13;
            this.lblPoliciesText.Text = "Policies Content...";
            // 
            // lblStatusMsg
            // 
            this.lblStatusMsg.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStatusMsg.ForeColor = System.Drawing.Color.Gold;
            this.lblStatusMsg.Location = new System.Drawing.Point(50, 710);
            this.lblStatusMsg.Name = "lblStatusMsg";
            this.lblStatusMsg.Size = new System.Drawing.Size(460, 45);
            this.lblStatusMsg.TabIndex = 8;
            this.lblStatusMsg.Text = "Your booking is a request. We will contact you within 10-12 hours to confirm.";
            // 
            // lblStaffContact
            // 
            this.lblStaffContact.AutoSize = true;
            this.lblStaffContact.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStaffContact.ForeColor = System.Drawing.Color.Silver;
            this.lblStaffContact.Location = new System.Drawing.Point(50, 760);
            this.lblStaffContact.Name = "lblStaffContact";
            this.lblStaffContact.Size = new System.Drawing.Size(220, 20);
            this.lblStaffContact.TabIndex = 9;
            this.lblStaffContact.Text = "Staff Inquiry: +63 912 345 6789";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(40, 45, 60);
            this.btnClose.BorderColor = System.Drawing.Color.Empty;
            this.btnClose.BorderRadius = 5;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(50, 810);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(460, 45);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // panelContent
            // 
            this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContent.Controls.Add(this.picRoomReceipt);
            this.panelContent.Controls.Add(this.lblPoliciesText);
            this.panelContent.Controls.Add(this.lblPoliciesHeader);
            this.panelContent.Controls.Add(this.lblReceiptDiscount);
            this.panelContent.Controls.Add(this.lblHotelName);
            this.panelContent.Controls.Add(this.btnClose);
            this.panelContent.Controls.Add(this.lblAddress);
            this.panelContent.Controls.Add(this.lblStaffContact);
            this.panelContent.Controls.Add(this.lblLine);
            this.panelContent.Controls.Add(this.lblStatusMsg);
            this.panelContent.Controls.Add(this.lblDetailsHeader);
            this.panelContent.Controls.Add(this.lblTotalPrice);
            this.panelContent.Controls.Add(this.lblGuestInfo);
            this.panelContent.Controls.Add(this.lblDatesInfo);
            this.panelContent.Controls.Add(this.lblRoomInfo);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(560, 880);
            this.panelContent.TabIndex = 11;
            // 
            // BookingReceiptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(25, 30, 50);
            this.ClientSize = new System.Drawing.Size(560, 880);
            this.Controls.Add(this.panelContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BookingReceiptForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Booking Receipt";
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblHotelName;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label lblDetailsHeader;
        private System.Windows.Forms.Label lblGuestInfo;
        private System.Windows.Forms.Label lblRoomInfo;
        private System.Windows.Forms.Label lblDatesInfo;
        private System.Windows.Forms.Label lblTotalPrice;
        private System.Windows.Forms.Label lblReceiptDiscount;
        private System.Windows.Forms.PictureBox picRoomReceipt;
        private System.Windows.Forms.Label lblPoliciesHeader;
        private System.Windows.Forms.Label lblPoliciesText;
        private System.Windows.Forms.Label lblStatusMsg;
        private System.Windows.Forms.Label lblStaffContact;
        private FP_CS26_2025.ModernDesign.ModernButton btnClose;
        private System.Windows.Forms.Panel panelContent;
    }
}
