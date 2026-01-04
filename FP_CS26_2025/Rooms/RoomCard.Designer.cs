namespace FP_CS26_2025.Rooms
{
    partial class RoomCard
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
            this._mainPanel = new FP_CS26_2025.ModernDesign.ModernShadowPanel();
            this._roomImage = new System.Windows.Forms.PictureBox();
            this._lblCategory = new System.Windows.Forms.Label();
            this._lblName = new System.Windows.Forms.Label();
            this._lblDescription = new System.Windows.Forms.Label();
            this._btnLearnMore = new FP_CS26_2025.ModernDesign.ModernButton();
            ((System.ComponentModel.ISupportInitialize)(this._roomImage)).BeginInit();
            this._mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainPanel
            // 
            this._mainPanel.BackColor = System.Drawing.Color.White;
            this._mainPanel.BorderRadius = 15;
            this._mainPanel.Controls.Add(this._roomImage);
            this._mainPanel.Controls.Add(this._lblCategory);
            this._mainPanel.Controls.Add(this._lblName);
            this._mainPanel.Controls.Add(this._lblDescription);
            this._mainPanel.Controls.Add(this._btnLearnMore);
            this._mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPanel.Location = new System.Drawing.Point(0, 0);
            this._mainPanel.Name = "_mainPanel";
            this._mainPanel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 5);
            this._mainPanel.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this._mainPanel.ShadowDepth = 5;
            this._mainPanel.Size = new System.Drawing.Size(300, 350);
            this._mainPanel.TabIndex = 0;
            // 
            // _roomImage
            // 
            this._roomImage.Location = new System.Drawing.Point(5, 5);
            this._roomImage.Name = "_roomImage";
            this._roomImage.Size = new System.Drawing.Size(285, 180);
            this._roomImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._roomImage.TabIndex = 0;
            this._roomImage.TabStop = false;
            // 
            // _lblCategory
            // 
            this._lblCategory.AutoSize = true;
            this._lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblCategory.ForeColor = System.Drawing.Color.Gray;
            this._lblCategory.Location = new System.Drawing.Point(15, 200);
            this._lblCategory.Name = "_lblCategory";
            this._lblCategory.Size = new System.Drawing.Size(36, 15);
            this._lblCategory.TabIndex = 1;
            this._lblCategory.Text = "Hotel";
            // 
            // _lblName
            // 
            this._lblName.AutoSize = true;
            this._lblName.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblName.Location = new System.Drawing.Point(15, 220);
            this._lblName.MaximumSize = new System.Drawing.Size(260, 60);
            this._lblName.Name = "_lblName";
            this._lblName.Size = new System.Drawing.Size(142, 25);
            this._lblName.TabIndex = 2;
            this._lblName.Text = "Celebrity Suite";
            // 
            // _lblDescription
            // 
            this._lblDescription.AutoSize = false;
            this._lblDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblDescription.ForeColor = System.Drawing.Color.DimGray;
            this._lblDescription.Location = new System.Drawing.Point(15, 250);
            this._lblDescription.Name = "_lblDescription";
            this._lblDescription.Size = new System.Drawing.Size(270, 45);
            this._lblDescription.TabIndex = 4;
            this._lblDescription.Text = "Room description goes here...";
            // 
            // _btnLearnMore
            // 
            this._btnLearnMore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(88)))), ((int)(((byte)(118)))));
            this._btnLearnMore.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(88)))), ((int)(((byte)(118)))));
            this._btnLearnMore.BorderRadius = 15;
            this._btnLearnMore.FlatAppearance.BorderSize = 0;
            this._btnLearnMore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnLearnMore.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._btnLearnMore.ForeColor = System.Drawing.Color.White;
            this._btnLearnMore.Location = new System.Drawing.Point(15, 300);
            this._btnLearnMore.Name = "_btnLearnMore";
            this._btnLearnMore.Size = new System.Drawing.Size(270, 35);
            this._btnLearnMore.TabIndex = 3;
            this._btnLearnMore.Text = "LEARN MORE";
            this._btnLearnMore.UseVisualStyleBackColor = false;
            // 
            // RoomCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._mainPanel);
            this.Name = "RoomCard";
            this.Size = new System.Drawing.Size(300, 350);
            ((System.ComponentModel.ISupportInitialize)(this._roomImage)).EndInit();
            this._mainPanel.ResumeLayout(false);
            this._mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        private FP_CS26_2025.ModernDesign.ModernShadowPanel _mainPanel;
        private System.Windows.Forms.PictureBox _roomImage;
        private System.Windows.Forms.Label _lblCategory;
        private System.Windows.Forms.Label _lblName;
        private System.Windows.Forms.Label _lblDescription;
        private FP_CS26_2025.ModernDesign.ModernButton _btnLearnMore;
    }
}
