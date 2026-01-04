using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FP_CS26_2025.LoginFormDesign
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class UsernameInputField : UserControl
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int EM_SETCUEBANNER = 0x1501;
        private const int EM_SETMARGINS = 0xD3;
        private const int EC_LEFTMARGIN = 0x1;

        private TextBox innerTextBox = new TextBox();
        private bool isFocused = false;

        // Properties
        public string PlaceholderText { get; set; } = "Enter username";
        public int BorderRadius { get; set; } = 20;
        public Color BorderColor { get; set; } = Color.Gray;
        public Color BorderFocusColor { get; set; } = Color.DeepSkyBlue;
        public int BorderSize { get; set; } = 1;
        public int TextLeftMargin { get; set; } = 20; // Increased from 12 to 20 to match password field

        public UsernameInputField()
        {
            this.BackColor = Color.White;
            this.Padding = new Padding(0);
            this.Size = new Size(250, 45);

            // Configure inner TextBox
            innerTextBox.BorderStyle = BorderStyle.None;
            innerTextBox.Font = new Font("Segoe UI", 10F);
            innerTextBox.BackColor = Color.White;
            this.Controls.Add(innerTextBox);

            // Focus events
            innerTextBox.Enter += (s, e) => { isFocused = true; this.Invalidate(); };
            innerTextBox.Leave += (s, e) => { isFocused = false; this.Invalidate(); };

            // Handle resizing
            this.Resize += (s, e) => UpdateLayout();

            // Load event
            this.Load += (s, e) =>
            {
                // Set left margin for the textbox
                SendMessage(innerTextBox.Handle, EM_SETMARGINS, EC_LEFTMARGIN, TextLeftMargin << 16);

                // Set placeholder
                SendMessage(innerTextBox.Handle, EM_SETCUEBANNER, 0, PlaceholderText);

                UpdateLayout();
            };
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

        private void UpdateLayout()
        {
            innerTextBox.Height = this.Height - 10;
            innerTextBox.Location = new Point(5 + TextLeftMargin, (this.Height - innerTextBox.Height) / 2);
            innerTextBox.Width = this.Width - 10 - TextLeftMargin; // Adjusted width calculation
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
                var rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                using (var path = GetRoundedRect(rect, BorderRadius))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}