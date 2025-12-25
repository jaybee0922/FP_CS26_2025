using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025
{
    // Abstraction & Inheritance: Base class provides common behavior and enforces contract
    public abstract class BaseFrontDeskPanel : UserControl
    {
        protected FrontDeskController _controller;
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

        // Polymorphism: Child classes MUST implement data refresh logic
        public abstract void RefreshData();

        // Polymorphism: Child classes SHOULD implement search logic if applicable
        public virtual void PerformSearch(string query)
        {
            // Default implementation does nothing
        }
    }
}
