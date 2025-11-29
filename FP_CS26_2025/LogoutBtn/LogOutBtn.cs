using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FP_CS26_2025.LogoutBtn
{
    public class LogOutBtn : Button
    {
        #region Private Fields
        private Color _hoverColor = Color.FromArgb(220, 53, 69);
        private Color _originalColor;
        private bool _isHovering;
        private string _confirmationMessage = "Are you sure you want to log out?";
        private string _confirmationTitle = "Logout Confirmation";
        private int _cornerRadius = 15;
        #endregion

        #region Public Properties
        [Category("Appearance")]
        [Description("The background color when mouse hovers over the button")]
        public Color HoverColor
        {
            get => _hoverColor;
            set
            {
                _hoverColor = value;
                if (_isHovering)
                    this.BackColor = _hoverColor;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The radius of the button's corners")]
        [DefaultValue(15)]
        public int CornerRadius
        {
            get => _cornerRadius;
            set
            {
                _cornerRadius = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("Behavior")]
        [Description("The message to show in confirmation dialog")]
        public string ConfirmationMessage
        {
            get => _confirmationMessage;
            set => _confirmationMessage = value;
        }

        [Category("Behavior")]
        [Description("The title of confirmation dialog")]
        public string ConfirmationTitle
        {
            get => _confirmationTitle;
            set => _confirmationTitle = value;
        }

        [Category("Behavior")]
        [Description("Whether to show confirmation dialog before logging out")]
        public bool RequireConfirmation { get; set; } = true;

        [Category("Behavior")]
        [Description("Action to execute when logout is confirmed")]
        public Action OnLogoutConfirmed { get; set; }
        #endregion

        #region Events
        [Category("Action")]
        [Description("Occurs when logout is successfully confirmed")]
        public event EventHandler LogoutConfirmed;
        #endregion

        #region Constructor
        public LogOutBtn()
        {
            InitializeButton();
            SubscribeEvents();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                    ControlStyles.UserPaint |
                    ControlStyles.DoubleBuffer |
                    ControlStyles.ResizeRedraw, true);
        }
        #endregion

        #region Private Methods
        private void InitializeButton()
        {
            // Basic button styling
            this.Text = "Log Out";
            this.BackColor = Color.FromArgb(220, 53, 69); // Bootstrap danger color
            this.ForeColor = Color.White;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(120, 40);
            this.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.Cursor = Cursors.Hand;
            this.Padding = new Padding(5);

            // Store original color
            _originalColor = this.BackColor;
        }

        private void SubscribeEvents()
        {
            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
            this.Click += OnClick;
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            _isHovering = true;
            this.BackColor = _hoverColor;
            Invalidate();
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            _isHovering = false;
            this.BackColor = _originalColor;
            Invalidate();
        }

        private void OnClick(object sender, EventArgs e)
        {
            PerformLogout();
        }

        private void PerformLogout()
        {
            if (RequireConfirmation)
            {
                var result = MessageBox.Show(
                    _confirmationMessage,
                    _confirmationTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2
                );

                if (result == DialogResult.Yes)
                {
                    ExecuteLogout();
                }
            }
            else
            {
                ExecuteLogout();
            }
        }

        private void ExecuteLogout()
        {
            // Execute custom action if provided
            OnLogoutConfirmed?.Invoke();

            // Raise the event
            LogoutConfirmed?.Invoke(this, EventArgs.Empty);

            // Additional logout logic can be added here
            // For example: Clear session, navigate to login page, etc.
        }

        private GraphicsPath GetRoundPath(RectangleF rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float diameter = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
        #endregion

        #region Overridden Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Create rounded rectangle path
            using (GraphicsPath path = GetRoundPath(ClientRectangle, _cornerRadius))
            {
                // Fill the background
                using (SolidBrush brush = new SolidBrush(BackColor))
                {
                    graphics.FillPath(brush, path);
                }

                // Draw the text
                TextFormatFlags flags = TextFormatFlags.HorizontalCenter |
                                      TextFormatFlags.VerticalCenter |
                                      TextFormatFlags.WordBreak;
                TextRenderer.DrawText(graphics, Text, Font, ClientRectangle, ForeColor, flags);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Programmatically trigger logout without confirmation
        /// </summary>
        public void ForceLogout()
        {
            ExecuteLogout();
        }

        /// <summary>
        /// Reset button to default appearance
        /// </summary>
        public void ResetAppearance()
        {
            this.BackColor = _originalColor;
            this.Text = "Log Out";
            _isHovering = false;
            Invalidate();
        }
        #endregion

        #region Disposal
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.MouseEnter -= OnMouseEnter;
                this.MouseLeave -= OnMouseLeave;
                this.Click -= OnClick;
            }
            base.Dispose(disposing);
        }
        #endregion
    }
}