using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FP_CS26_2025.LoginFormDesign
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class LoginBtn : Button
    {
        // ✅ Add BorderRadius property
        public int BorderRadius { get; set; } = 15;

        // Bounce Animation
        private float _scale = 1f;
        private float _bounceProgress = 0f;
        private bool _isBouncing = false;

        // Ripple
        private bool _isRippleActive = false;
        private float _rippleSize = 0f;
        private float _rippleOpacity = 0.4f;
        private Point _rippleCenter;

        // Shadow Hover
        private float _shadowOpacity = 0f;

        // Timer
        private Timer _animationTimer;

        public LoginBtn()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;
            this.DoubleBuffered = true;

            this.Cursor = Cursors.Hand;   // 👉 Makes mouse cursor a hand on hover

            _animationTimer = new Timer();
            _animationTimer.Interval = 15;
            _animationTimer.Tick += Animate;
        }

        // Hover shadow
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _shadowOpacity = 1f; // fade shadow in
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _shadowOpacity = 0f; // fade shadow out
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            // --- Bounce Animation Setup ---
            _bounceProgress = 0f;
            _isBouncing = true;

            // --- Ripple Setup ---
            _rippleCenter = PointToClient(Cursor.Position);
            _rippleSize = 0f;
            _rippleOpacity = 0.4f;
            _isRippleActive = true;

            _animationTimer.Start();
        }

        private void Animate(object sender, EventArgs e)
        {
            bool stillAnimating = false;

            // --- Bounce Animation ---
            if (_isBouncing)
            {
                _bounceProgress += 0.12f;
                if (_bounceProgress >= 1f)
                {
                    _bounceProgress = 1f;
                    _isBouncing = false;
                }

                _scale = 1f + (float)(-Math.Sin(_bounceProgress * Math.PI) * 0.12f);
                stillAnimating = true;
            }

            // --- Ripple Animation ---
            if (_isRippleActive)
            {
                _rippleSize += 12f;         // speed outward
                _rippleOpacity -= 0.02f;     // fade out

                if (_rippleOpacity <= 0f)
                {
                    _isRippleActive = false;
                }
                stillAnimating = true;
            }

            if (!stillAnimating)
                _animationTimer.Stop();

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // --- Hover Shadow Behind Button ---
            if (_shadowOpacity > 0f)
            {
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb((int)(_shadowOpacity * 80), 0, 0, 0)))
                {
                    Rectangle shadowRect = new Rectangle(4, 4, Width - 8, Height - 8);
                    e.Graphics.FillRectangle(shadowBrush, shadowRect);
                }
            }

            // --- Bounce Transform ---
            float offsetX = (Width - Width * _scale) / 2;
            float offsetY = (Height - Height * _scale) / 2;
            e.Graphics.TranslateTransform(offsetX, offsetY);
            e.Graphics.ScaleTransform(_scale, _scale);

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
                Color.FromArgb(99, 102, 241), // Indigo
                Color.FromArgb(79, 70, 229),  // Deeper Indigo
                45f))
            {
                e.Graphics.FillPath(brush, path);
            }

            // --- Ripple Effect ---
            if (_isRippleActive)
            {
                using (SolidBrush rippleBrush = new SolidBrush(
                    Color.FromArgb((int)(_rippleOpacity * 255), Color.White)))
                {
                    float maxRadius = Math.Max(Width, Height) * 1.2f;
                    float radius = _rippleSize;

                    e.Graphics.SetClip(path);

                    e.Graphics.FillEllipse(
                        rippleBrush,
                        _rippleCenter.X - radius / 2,
                        _rippleCenter.Y - radius / 2,
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
