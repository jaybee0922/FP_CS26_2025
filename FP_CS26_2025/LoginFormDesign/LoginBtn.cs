using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FP_CS26_2025.LoginFormDesign
{
    public class LoginBtn : Button
    {
        // ✅ Add BorderRadius property
        public int BorderRadius { get; set; } = 15;

        // Bounce Animation
        private float scale = 1f;
        private float bounceProgress = 0f;
        private bool bouncing = false;

        // Ripple
        private bool rippleActive = false;
        private float rippleSize = 0f;
        private float rippleOpacity = 0.4f;
        private Point rippleCenter;

        // Shadow Hover
        private float shadowOpacity = 0f;

        // Timer
        private Timer animationTimer;

        public LoginBtn()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;
            this.DoubleBuffered = true;

            this.Cursor = Cursors.Hand;   // 👉 Makes mouse cursor a hand on hover

            animationTimer = new Timer();
            animationTimer.Interval = 15;
            animationTimer.Tick += Animate;
        }

        // Hover shadow
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            shadowOpacity = 1f; // fade shadow in
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            shadowOpacity = 0f; // fade shadow out
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            // --- Bounce Animation Setup ---
            bounceProgress = 0f;
            bouncing = true;

            // --- Ripple Setup ---
            rippleCenter = PointToClient(Cursor.Position);
            rippleSize = 0f;
            rippleOpacity = 0.4f;
            rippleActive = true;

            animationTimer.Start();
        }

        private void Animate(object sender, EventArgs e)
        {
            bool stillAnimating = false;

            // --- Bounce Animation ---
            if (bouncing)
            {
                bounceProgress += 0.12f;
                if (bounceProgress >= 1f)
                {
                    bounceProgress = 1f;
                    bouncing = false;
                }

                scale = 1f + (float)(-Math.Sin(bounceProgress * Math.PI) * 0.12f);
                stillAnimating = true;
            }

            // --- Ripple Animation ---
            if (rippleActive)
            {
                rippleSize += 12f;         // speed outward
                rippleOpacity -= 0.02f;     // fade out

                if (rippleOpacity <= 0f)
                {
                    rippleActive = false;
                }
                stillAnimating = true;
            }

            if (!stillAnimating)
                animationTimer.Stop();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // --- Hover Shadow Behind Button ---
            if (shadowOpacity > 0f)
            {
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb((int)(shadowOpacity * 80), 0, 0, 0)))
                {
                    Rectangle shadowRect = new Rectangle(4, 4, Width - 8, Height - 8);
                    e.Graphics.FillRectangle(shadowBrush, shadowRect);
                }
            }

            // --- Bounce Transform ---
            float offsetX = (Width - Width * scale) / 2;
            float offsetY = (Height - Height * scale) / 2;
            e.Graphics.TranslateTransform(offsetX, offsetY);
            e.Graphics.ScaleTransform(scale, scale);

            // --- Shape Path ---
            GraphicsPath path = new GraphicsPath();
            int r = BorderRadius;
            int d = r * 2;

            path.AddArc(0, 0, d, d, 180, 90);
            path.AddArc(Width - d - 1, 0, d, d, 270, 90);
            path.AddArc(Width - d - 1, Height - d - 1, d, d, 0, 90);
            path.AddArc(0, Height - d - 1, d, d, 90, 90);
            path.CloseFigure();

            Region = new Region(path);

            // --- Gradient Fill ---
            using (LinearGradientBrush brush = new LinearGradientBrush(
                ClientRectangle,
                Color.FromArgb(0, 170, 255),
                Color.FromArgb(0, 102, 255),
                135f))
            {
                e.Graphics.FillPath(brush, path);
            }

            // --- Ripple Effect ---
            if (rippleActive)
            {
                using (SolidBrush rippleBrush = new SolidBrush(
                    Color.FromArgb((int)(rippleOpacity * 255), Color.White)))
                {
                    float maxRadius = Math.Max(Width, Height) * 1.2f;
                    float radius = rippleSize;

                    e.Graphics.SetClip(path);

                    e.Graphics.FillEllipse(
                        rippleBrush,
                        rippleCenter.X - radius / 2,
                        rippleCenter.Y - radius / 2,
                        radius,
                        radius
                    );

                    e.Graphics.ResetClip();
                }
            }

            // --- Text ---
            TextRenderer.DrawText(
                e.Graphics,
                Text,
                Font,
                ClientRectangle,
                Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            );
        }
    }
}
