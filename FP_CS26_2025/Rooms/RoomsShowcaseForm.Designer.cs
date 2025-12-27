namespace FP_CS26_2025.Rooms
{
    partial class RoomsShowcaseForm
    {
        private System.ComponentModel.IContainer components = null;
        private RoomGalleryView roomGalleryView1;

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
            this.roomGalleryView1 = new FP_CS26_2025.Rooms.RoomGalleryView();
            this.SuspendLayout();
            // 
            // roomGalleryView1
            // 
            this.roomGalleryView1.BackColor = System.Drawing.Color.White;
            this.roomGalleryView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomGalleryView1.Location = new System.Drawing.Point(0, 0);
            this.roomGalleryView1.Name = "roomGalleryView1";
            this.roomGalleryView1.Size = new System.Drawing.Size(1100, 600);
            this.roomGalleryView1.TabIndex = 0;
            // 
            // RoomsShowcaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 600);
            this.Controls.Add(this.roomGalleryView1);
            this.Name = "RoomsShowcaseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotel Rooms Gallery - Modern UI Showcase";
            this.ResumeLayout(false);

        }
    }
}
