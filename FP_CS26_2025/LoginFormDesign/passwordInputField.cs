using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FP_CS26_2025.LoginFormDesign
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class PasswordInputField : UserControl
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int EM_SETCUEBANNER = 0x1501;
        private const int EM_SETMARGINS = 0xD3;
        private const int EC_LEFTMARGIN = 0x1;

        private TextBox innerTextBox = new TextBox();
        private PictureBox toggleBox = new PictureBox();

        private bool isFocused = false;
        private bool isPasswordHidden = true;

        public string PlaceholderText { get; set; } = "Enter password";
        public int BorderRadius { get; set; } = 20;
        public Color BorderColor { get; set; } = Color.Gray;
        public Color BorderFocusColor { get; set; } = Color.DeepSkyBlue;
        public int BorderSize { get; set; } = 1;
        public int TextLeftMargin { get; set; } = 20;
        public int IconRightMargin { get; set; } = 12; // New property for icon right margin

        private Image openEyesIcon;
        private Image closeEyesIcon;

        public PasswordInputField()
        {
            this.BackColor = Color.White;
            this.Padding = new Padding(0);
            this.Size = new Size(250, 45);

            // TextBox
            innerTextBox.BorderStyle = BorderStyle.None;
            innerTextBox.Font = new Font("Segoe UI", 10F);
            innerTextBox.UseSystemPasswordChar = true;
            innerTextBox.BackColor = Color.White;
            this.Controls.Add(innerTextBox);

            // Eye toggle
            toggleBox.SizeMode = PictureBoxSizeMode.Zoom;
            toggleBox.Size = new Size(24, 24);
            toggleBox.Cursor = Cursors.Hand;
            toggleBox.BackColor = Color.Transparent;
            toggleBox.Click += TogglePassword;
            this.Controls.Add(toggleBox);

            innerTextBox.Enter += (s, e) => { isFocused = true; this.Invalidate(); };
            innerTextBox.Leave += (s, e) => { isFocused = false; this.Invalidate(); };

            this.Load += (s, e) =>
            {
                // Set left margin for the textbox
                SendMessage(innerTextBox.Handle, EM_SETMARGINS, EC_LEFTMARGIN, TextLeftMargin << 16);

                // Set placeholder text
                SendMessage(innerTextBox.Handle, EM_SETCUEBANNER, 0, PlaceholderText);

                // Load icons from Assets
                string basePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Assets\\");
                try
                {
                    if (System.IO.File.Exists(basePath + "open-eyes.png"))
                        openEyesIcon = Image.FromFile(basePath + "open-eyes.png");
                    if (System.IO.File.Exists(basePath + "hide-eyes.png"))
                        closeEyesIcon = Image.FromFile(basePath + "hide-eyes.png");
                }
                catch (Exception ex)
                {
                    // Handle image loading errors silently
                    System.Diagnostics.Debug.WriteLine("Icon loading failed: " + ex.Message);
                }

                toggleBox.Image = isPasswordHidden ? openEyesIcon : closeEyesIcon;

                // Force layout update after loading
                UpdateLayout();
            };

            this.Resize += (s, e) => UpdateLayout();
            this.SizeChanged += (s, e) => UpdateLayout();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            // Re-apply margins and placeholder when handle is recreated
            if (innerTextBox.IsHandleCreated)
            {
                SendMessage(innerTextBox.Handle, EM_SETMARGINS, EC_LEFTMARGIN, TextLeftMargin << 16);
                SendMessage(innerTextBox.Handle, EM_SETCUEBANNER, 0, PlaceholderText);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateLayout(); // Ensure layout is updated when control is loaded
        }

        private void TogglePassword(object sender, EventArgs e)
        {
            isPasswordHidden = !isPasswordHidden;
            innerTextBox.UseSystemPasswordChar = isPasswordHidden;
            toggleBox.Image = isPasswordHidden ? openEyesIcon : closeEyesIcon;
        }

        private void UpdateLayout()
        {
            // Calculate available space for textbox (accounting for icon and margins)
            int iconTotalWidth = toggleBox.Width + IconRightMargin;

            innerTextBox.Height = this.Height - 10;
            innerTextBox.Location = new Point(5 + TextLeftMargin, (this.Height - innerTextBox.Height) / 2);
            innerTextBox.Width = this.Width - iconTotalWidth - 5 - TextLeftMargin; // 5 is left padding

            // Position icon with right margin
            toggleBox.Location = new Point(
                this.Width - toggleBox.Width - IconRightMargin,
                (this.Height - toggleBox.Height) / 2
            );

            // Force refresh
            this.Invalidate();
        }

        public override string Text
        {
            get => innerTextBox.Text;
            set => innerTextBox.Text = value;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(isFocused ? BorderFocusColor : BorderColor, BorderSize))
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                using (GraphicsPath path = GetRoundedRect(rect, BorderRadius))
                    e.Graphics.DrawPath(pen, path);
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            Rectangle arc = new Rectangle(rect.X, rect.Y, d, d);

            path.AddArc(arc, 180, 90); // top-left
            arc.X = rect.Right - d; arc.Y = rect.Top;
            path.AddArc(arc, 270, 90); // top-right
            arc.X = rect.Right - d; arc.Y = rect.Bottom - d;
            path.AddArc(arc, 0, 90); // bottom-right
            arc.X = rect.Left; arc.Y = rect.Bottom - d;
            path.AddArc(arc, 90, 90); // bottom-left

            path.CloseFigure();
            return path;
        }
    }
}