using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FP_CS26_2025.LoginFormDesign
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class RoleComboBox : UserControl
    {
        private ComboBox innerComboBox = new ComboBox();
        private bool isFocused = false;

        // Properties
        public int BorderRadius { get; set; } = 20;
        public Color BorderColor { get; set; } = Color.Gray;
        public Color BorderFocusColor { get; set; } = Color.DeepSkyBlue;
        public int BorderSize { get; set; } = 1;
        public int TextLeftMargin { get; set; } = 12;

        // Expose inner ComboBox properties
        public object SelectedItem
        {
            get => innerComboBox.SelectedItem;
            set => innerComboBox.SelectedItem = value;
        }

        public int SelectedIndex
        {
            get => innerComboBox.SelectedIndex;
            set => innerComboBox.SelectedIndex = value;
        }

        public string SelectedText
        {
            get => innerComboBox.SelectedText;
            set => innerComboBox.SelectedText = value;
        }

        public ComboBox.ObjectCollection Items => innerComboBox.Items;

        public event EventHandler SelectedIndexChanged
        {
            add { innerComboBox.SelectedIndexChanged += value; }
            remove { innerComboBox.SelectedIndexChanged -= value; }
        }

        public RoleComboBox()
        {
            this.BackColor = Color.White;
            this.Padding = new Padding(0);
            this.Size = new Size(250, 45);

            // Configure inner ComboBox
            innerComboBox.FlatStyle = FlatStyle.Flat;
            innerComboBox.Font = new Font("Segoe UI", 10F);
            innerComboBox.BackColor = Color.White;
            innerComboBox.ForeColor = Color.FromArgb(64, 64, 64);
            innerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Add default items
            innerComboBox.Items.AddRange(new object[] { "Super Admin", "Front Desk" });
            innerComboBox.SelectedIndex = 0; // Select first item by default

            // Remove border from inner comboBox
            innerComboBox.FlatStyle = FlatStyle.Flat;

            this.Controls.Add(innerComboBox);

            // Focus events
            innerComboBox.Enter += (s, e) => { isFocused = true; this.Invalidate(); };
            innerComboBox.Leave += (s, e) => { isFocused = false; this.Invalidate(); };
            innerComboBox.DropDown += (s, e) => { isFocused = true; this.Invalidate(); };
            innerComboBox.DropDownClosed += (s, e) => { isFocused = false; this.Invalidate(); };

            // Handle resizing
            this.Resize += (s, e) => UpdateLayout();
            this.SizeChanged += (s, e) => UpdateLayout();

            // Initial layout
            this.Load += (s, e) => UpdateLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            innerComboBox.Height = this.Height - 12;
            innerComboBox.Location = new Point(TextLeftMargin + 5, (this.Height - innerComboBox.Height) / 2);
            innerComboBox.Width = this.Width - (TextLeftMargin * 2) - 10;

            // Customize the dropdown appearance
            innerComboBox.DropDownHeight = 120; // Set fixed dropdown height
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

            // Draw dropdown arrow
            using (var arrowBrush = new SolidBrush(Color.Gray))
            {
                int arrowSize = 8;
                int arrowX = this.Width - TextLeftMargin - arrowSize;
                int arrowY = (this.Height - arrowSize) / 2;

                Point[] arrowPoints = {
                    new Point(arrowX, arrowY),
                    new Point(arrowX + arrowSize, arrowY),
                    new Point(arrowX + arrowSize / 2, arrowY + arrowSize)
                };

                e.Graphics.FillPolygon(arrowBrush, arrowPoints);
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