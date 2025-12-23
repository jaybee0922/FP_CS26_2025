using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FP_CS26_2025.ModernDesign
{
    public class GradientPanel : Panel
    {
        // Encapsulation: Properties for gradient colors and angle
        public Color ColorTop { get; set; } = Color.FromArgb(43, 88, 118); 
        public Color ColorBottom { get; set; } = Color.FromArgb(78, 67, 118); 
        public float Angle { get; set; } = 45F;

        protected override void OnPaint(PaintEventArgs e)
        {
            // Use LinearGradientBrush for the background
            using (LinearGradientBrush lgb = new LinearGradientBrush(this.ClientRectangle, this.ColorTop, this.ColorBottom, this.Angle))
            {
                e.Graphics.FillRectangle(lgb, this.ClientRectangle);
            }
            base.OnPaint(e);
        }
        
        protected override void OnPaintBackground(PaintEventArgs e) 
        {
            // Handled in OnPaint for smoother resizing in some cases, 
            // but standard is OnPaintBackground. We can do it here too.
             using (LinearGradientBrush lgb = new LinearGradientBrush(this.ClientRectangle, this.ColorTop, this.ColorBottom, this.Angle))
            {
                e.Graphics.FillRectangle(lgb, this.ClientRectangle);
            }
        }
    }
}
