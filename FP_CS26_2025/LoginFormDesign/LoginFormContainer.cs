using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FP_CS26_2025.LoginFormDesign
{
    public class LoginFormContainer : Panel
    {
        public int BorderRadius { get; set; } = 50; // Rounded corners
        public Color PanelColor { get; set; } = Color.White;

        // Shadow settings
        public Color ShadowColor { get; set; } = Color.FromArgb(51, 100, 100, 111); // rgba(100,100,111,0.2)
        public int ShadowOffsetX { get; set; } = 0;
        public int ShadowOffsetY { get; set; } = 7;
        public int ShadowBlur { get; set; } = 29;

        private int shadowDepth = 7;
        public int ShadowDepth
        {
            get => shadowDepth;
            set
            {
                shadowDepth = value;
                ShadowOffsetY = value;
                ShadowBlur = value * 4; // scale blur
                this.Invalidate();
            }
        }

        public LoginFormContainer()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.None;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Do not call base.OnPaint to avoid artifacts
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Slightly expand shadow rectangle to reduce lines
            RectangleF shadowRect = new RectangleF(
                ShadowOffsetX - ShadowBlur * 0.6f,
                ShadowOffsetY - ShadowBlur * 0.6f,
                this.Width + ShadowBlur * 1.2f,
                this.Height + ShadowBlur * 1.2f
            );

            using (GraphicsPath shadowPath = GetRoundedPath(shadowRect, BorderRadius + ShadowBlur / 2f))
            using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
            {
                shadowBrush.CenterColor = ShadowColor;
                shadowBrush.SurroundColors = new Color[] { Color.Transparent };
                shadowBrush.CenterPoint = new PointF(
                    shadowRect.Left + shadowRect.Width / 2f,
                    shadowRect.Top + shadowRect.Height / 2f
                );

                e.Graphics.FillPath(shadowBrush, shadowPath);
            }

            // Main panel
            RectangleF panelRect = new RectangleF(0, 0, this.Width - ShadowOffsetX, this.Height - ShadowOffsetY);
            using (GraphicsPath panelPath = GetRoundedPath(panelRect, BorderRadius))
            using (SolidBrush panelBrush = new SolidBrush(PanelColor))
            {
                e.Graphics.FillPath(panelBrush, panelPath);
            }
        }

        private GraphicsPath GetRoundedPath(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float d = radius * 2f;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}





