using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FP_CS26_2025.ModernDesign
{
    public class ModernShadowPanel : Panel
    {
        private int _shadowDepth = 5;
        private Color _shadowColor = Color.FromArgb(200, 200, 200); // Soft gray shadow
        private int _borderRadius = 15;

        public int ShadowDepth
        {
            get => _shadowDepth;
            set { _shadowDepth = value; Invalidate(); }
        }

        public Color ShadowColor
        {
            get => _shadowColor;
            set { _shadowColor = value; Invalidate(); }
        }

        public int BorderRadius
        {
            get => _borderRadius;
            set { _borderRadius = value; Invalidate(); }
        }

        public ModernShadowPanel()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.Padding = new Padding(10); // Default padding
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw Shadow
            Rectangle shadowRect = new Rectangle(
                _shadowDepth, 
                _shadowDepth, 
                this.Width - _shadowDepth * 2, 
                this.Height - _shadowDepth * 2);

            using (GraphicsPath shadowPath = GetRoundedPath(shadowRect, _borderRadius))
            using (PathGradientBrush shadowBrush = new PathGradientBrush(shadowPath))
            {
                shadowBrush.CenterColor = _shadowColor;
                shadowBrush.SurroundColors = new Color[] { Color.Transparent };
                e.Graphics.FillPath(shadowBrush, shadowPath);
            }

            // Draw Main Panel (on top of shadow)
            Rectangle rect = new Rectangle(0, 0, this.Width - _shadowDepth, this.Height - _shadowDepth);
            using (GraphicsPath path = GetRoundedPath(rect, _borderRadius))
            using (SolidBrush brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            if (rect.Width <= diameter || rect.Height <= diameter)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
