using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FP_CS26_2025.FrontDesk_ReceptionistAccount
{
    public class process_reservation : Label
    {
        public int BorderRadius { get; set; } = 15;
        private Image icon;
        private Color normalBackColor = Color.White;
        private Color hoverBackColor = Color.FromArgb(230, 240, 255);

        // Default size
        protected override Size DefaultSize => new Size(250, 50);

        public process_reservation()
        {
            this.AutoSize = false;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.Padding = new Padding(10, 0, 0, 0);
            this.ForeColor = Color.FromArgb(0, 102, 255);
            this.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.BackColor = normalBackColor;

            // Load icon
            try
            {
                icon = Image.FromFile(@"C:\Users\Geoffrey\source\repos\FP_CS26_2025\process_reservation.png");
            }
            catch
            {
                icon = null;
            }

            // Hover effect
            this.MouseEnter += (s, e) => this.BackColor = hoverBackColor;
            this.MouseLeave += (s, e) => this.BackColor = normalBackColor;

            this.Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Rounded rectangle background
            GraphicsPath path = new GraphicsPath();
            int diameter = BorderRadius * 2;
            path.AddArc(0, 0, diameter, diameter, 180, 90);
            path.AddArc(this.Width - diameter - 1, 0, diameter, diameter, 270, 90);
            path.AddArc(this.Width - diameter - 1, this.Height - diameter - 1, diameter, diameter, 0, 90);
            path.AddArc(0, this.Height - diameter - 1, diameter, diameter, 90, 90);
            path.CloseFigure();

            using (SolidBrush brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            // Draw icon
            int iconWidth = 70;
            int iconHeight = 32;
            int iconPadding = 10;
            if (icon != null)
            {
                e.Graphics.DrawImage(icon, new Rectangle(iconPadding, (this.Height - iconHeight) / 2, iconWidth, iconHeight));
            }

            // Draw text with smaller spacing
            int textOffset = icon != null ? iconWidth + iconPadding : 10; // ✅ reduced spacing
            Rectangle textRect = new Rectangle(textOffset, 0, this.Width - textOffset, this.Height);

            TextRenderer.DrawText(
                e.Graphics,
                this.Text,
                this.Font,
                textRect,
                this.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Left
            );
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }
    }
}