using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FP_CS26_2025.LoginFormDesign
{
    public class LoginBtn : Button
    {
        // ✅ Add BorderRadius property
        public int BorderRadius { get; set; } = 15;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create rounded rectangle path
            GraphicsPath path = new GraphicsPath();
            int radius = BorderRadius;
            int diameter = radius * 2;

            path.AddArc(0, 0, diameter, diameter, 180, 90);
            path.AddArc(this.Width - diameter - 1, 0, diameter, diameter, 270, 90);
            path.AddArc(this.Width - diameter - 1, this.Height - diameter - 1, diameter, diameter, 0, 90);
            path.AddArc(0, this.Height - diameter - 1, diameter, diameter, 90, 90);
            path.CloseFigure();

            // Set button region to make rounded corners visible
            this.Region = new Region(path);

            // Fill gradient
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(0, 170, 255), // #00aaff
                Color.FromArgb(0, 102, 255), // #0066ff
                135f))
            {
                e.Graphics.FillPath(brush, path);
            }

            // Draw centered text
            TextRenderer.DrawText(
                e.Graphics,
                this.Text,
                this.Font,
                this.ClientRectangle,
                Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }
    }
}





