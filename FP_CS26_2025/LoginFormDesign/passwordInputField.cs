using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FP_CS26_2025.LoginFormDesign
{
    public class passwordInputField : UserControl
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, string lParam);
        private const int EM_SETCUEBANNER = 0x1501;

        private TextBox innerTextBox = new TextBox();
        private PictureBox iconBox = new PictureBox();
        private bool isFocused = false;

        // Properties
        public string PlaceholderText { get; set; } = "Enter password";
        public int BorderRadius { get; set; } = 20;
        public Color BorderColor { get; set; } = Color.Gray;
        public Color BorderFocusColor { get; set; } = Color.DeepSkyBlue;
        public int BorderSize { get; set; } = 1;
        public Image Icon
        {
            get => iconBox.Image;
            set => iconBox.Image = value;
        }

        public passwordInputField()
        {
            this.BackColor = Color.White;
            this.Padding = new Padding(8);
            this.Size = new Size(200, 40);

            // Configure icon PictureBox
            iconBox.SizeMode = PictureBoxSizeMode.Zoom;
            iconBox.Size = new Size(24, 24);
            this.Controls.Add(iconBox);

            // Configure inner TextBox
            innerTextBox.BorderStyle = BorderStyle.None;
            innerTextBox.Font = new Font("Segoe UI", 10F);
            innerTextBox.UseSystemPasswordChar = true;
            this.Controls.Add(innerTextBox);

            // Focus events
            innerTextBox.Enter += (s, e) => { isFocused = true; this.Invalidate(); };
            innerTextBox.Leave += (s, e) => { isFocused = false; this.Invalidate(); };
            innerTextBox.TextChanged += (s, e) => this.Invalidate();

            // Set placeholder text on load
            this.Load += (s, e) =>
            {
                SendMessage(innerTextBox.Handle, EM_SETCUEBANNER, 0, PlaceholderText);
            };

            // Load lock icon from file
            try
            {
                Icon = Image.FromFile(@"C:\Users\Geoffrey\source\repos\FP_CS26_DEVV\lock.png");
            }
            catch
            {
                Icon = null;
            }

            // Handle resizing
            this.Resize += (s, e) => UpdateLayout();
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            // Vertically center the icon
            iconBox.Location = new Point(8, (this.Height - iconBox.Height) / 2);

            // Position the TextBox next to the icon with margin
            int iconRightMargin = iconBox.Right + 5;
            innerTextBox.Location = new Point(iconRightMargin, (this.Height - innerTextBox.Height) / 2);
            innerTextBox.Width = this.Width - iconRightMargin - 8;
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







