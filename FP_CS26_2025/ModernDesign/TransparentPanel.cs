using System.Windows.Forms;
using System.Drawing;

namespace FP_CS26_2025.ModernDesign
{
    // Inheritance
    public class TransparentPanel : Panel
    {
        public TransparentPanel()
        {
            // Set styles to enable transparency
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // WS_EX_TRANSPARENT = 0x20
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; 
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Do not paint background
        }
    }
}
