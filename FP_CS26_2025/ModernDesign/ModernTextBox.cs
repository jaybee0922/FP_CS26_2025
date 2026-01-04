using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FP_CS26_2025.ModernDesign
{
    public class ModernTextBox : UserControl
    {
        private TextBox textBox;
        private Color borderColor = Color.Gray;
        private Color borderFocusColor = Color.HotPink;
        private int borderSize = 2;
        private bool underlinedStyle = true; // Modern style default
        private bool isFocused = false;
        private string placeholderText = "";
        private Color placeholderColor = Color.DarkGray;
        private Color textColor = Color.Black;

        public event EventHandler TextChange;

        public ModernTextBox()
        {
            this.DoubleBuffered = true;
            this.Padding = new Padding(10, 7, 10, 7);
            this.BackColor = Color.White;
            
            textBox = new TextBox();
            textBox.BorderStyle = BorderStyle.None;
            textBox.Dock = DockStyle.Fill;
            textBox.BackColor = this.BackColor;
            textBox.ForeColor = this.ForeColor;
            
            textBox.Enter += (s, e) => { isFocused = true; this.Invalidate(); RemovePlaceholder(); };
            textBox.Leave += (s, e) => { isFocused = false; this.Invalidate(); SetPlaceholder(); };
            textBox.TextChanged += (s, e) => { if(!_isPlaceholderActive) TextChange?.Invoke(s, e); };

            this.Controls.Add(textBox);
        }

        // Properties
        public Color BorderColor { get => borderColor; set { borderColor = value; Invalidate(); } }
        public Color BorderFocusColor { get => borderFocusColor; set { borderFocusColor = value; Invalidate(); } }
        public bool UnderlinedStyle { get => underlinedStyle; set { underlinedStyle = value; Invalidate(); } }
        
        public override string Text 
        { 
            get => _isPlaceholderActive ? "" : textBox.Text; 
            set 
            { 
                textBox.Text = value; 
                RemovePlaceholder(); 
            } 
        }

        public string PlaceholderText
        {
            get => placeholderText;
            set
            {
                placeholderText = value;
                SetPlaceholder();
            }
        }

        private bool _isPlaceholderActive = false;
        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(textBox.Text) && !string.IsNullOrWhiteSpace(placeholderText))
            {
                _isPlaceholderActive = true;
                textBox.Text = placeholderText;
                textBox.ForeColor = placeholderColor;
            }
        }

        private void RemovePlaceholder()
        {
             if (_isPlaceholderActive)
            {
                _isPlaceholderActive = false;
                textBox.Text = "";
                textBox.ForeColor = textColor;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            // Draw border
            using (Pen penBorder = new Pen(isFocused ? borderFocusColor : borderColor, borderSize))
            {
                penBorder.Alignment = PenAlignment.Inset;
                if (underlinedStyle) // Line Style
                    graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                else // Rect Style
                    graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode) UpdateControlHeight();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }

        private void UpdateControlHeight()
        {
             if (textBox.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                textBox.Multiline = true;
                textBox.MinimumSize = new Size(0, txtHeight);
                textBox.Multiline = false;
                this.Height = textBox.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }
    }
}
