namespace FP_CS26_2025.Rooms
{
    partial class RoomGalleryView
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
            this.roomTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.paginationContainer = new System.Windows.Forms.Panel();
            this.paginationControl1 = new FP_CS26_2025.Rooms.PaginationControl();
            this.paginationContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // roomTableLayoutPanel
            // 
            this.roomTableLayoutPanel.ColumnCount = 3;
            this.roomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.roomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.roomTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.roomTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.roomTableLayoutPanel.Name = "roomTableLayoutPanel";
            this.roomTableLayoutPanel.RowCount = 1;
            this.roomTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.roomTableLayoutPanel.Size = new System.Drawing.Size(1100, 450);
            this.roomTableLayoutPanel.TabIndex = 0;
            // 
            // paginationContainer
            // 
            this.paginationContainer.Controls.Add(this.paginationControl1);
            this.paginationContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.paginationContainer.Location = new System.Drawing.Point(0, 450);
            this.paginationContainer.Name = "paginationContainer";
            this.paginationContainer.Size = new System.Drawing.Size(1100, 100);
            this.paginationContainer.TabIndex = 1;
            // 
            // paginationControl1
            // 
            this.paginationControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.paginationControl1.BackColor = System.Drawing.Color.Transparent;
            this.paginationControl1.CurrentPage = 1;
            this.paginationControl1.Location = new System.Drawing.Point(400, 30);
            this.paginationControl1.Name = "paginationControl1";
            this.paginationControl1.Size = new System.Drawing.Size(300, 40);
            this.paginationControl1.TabIndex = 0;
            this.paginationControl1.TotalPages = 1;
            this.paginationControl1.PageChanged += new System.EventHandler<int>(this.paginationControl1_PageChanged);
            // 
            // RoomGalleryView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.roomTableLayoutPanel);
            this.Controls.Add(this.paginationContainer);
            this.Name = "RoomGalleryView";
            this.Size = new System.Drawing.Size(1100, 550);
            this.paginationContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel roomTableLayoutPanel;
        private PaginationControl paginationControl1;
        private System.Windows.Forms.Panel paginationContainer;
    }
}
