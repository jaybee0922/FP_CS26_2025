using System;
using System.Drawing;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025
{
    public class GuestListPanel : BaseFrontDeskPanel
    {
        public GuestListPanel() : base() { InitializeComponents(); }
        public GuestListPanel(FrontDeskController controller) : base(controller, "Guest List")
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            var lblPlaceholder = new Label 
            { 
                Text = "Manage guest information and history.", 
                Location = new Point(30, 80), 
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 12)
            };
            this.Controls.Add(lblPlaceholder);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GuestListPanel
            // 
            this.Name = "GuestListPanel";
            this.Size = new System.Drawing.Size(1033, 666);
            this.ResumeLayout(false);

        }
    }
}
