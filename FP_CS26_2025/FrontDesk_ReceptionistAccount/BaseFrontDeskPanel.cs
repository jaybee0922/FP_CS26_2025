using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025
{
    public class BaseFrontDeskPanel : UserControl
    {
        protected readonly FrontDeskController _controller;
        protected Label lblTitle;

        // Parameterless constructor for Designer support
        public BaseFrontDeskPanel()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(245, 247, 251);
        }

        public BaseFrontDeskPanel(FrontDeskController controller, string title) : this()
        {
            _controller = controller;
            
            lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(41, 128, 185),
                Location = new Point(30, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);
        }
    }
}
