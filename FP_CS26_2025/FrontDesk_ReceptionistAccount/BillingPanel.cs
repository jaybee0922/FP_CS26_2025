using System;
using System.Windows.Forms;
using FP_CS26_2025.FrontDesk_MVC;

namespace FP_CS26_2025
{
    public class BillingPanel : BaseFrontDeskPanel
    {
        public BillingPanel() : base() {}
        public BillingPanel(FrontDeskController controller) : base(controller, "Billing History") { }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BillingPanel
            // 
            this.Name = "BillingPanel";
            this.Size = new System.Drawing.Size(1033, 666);
            this.ResumeLayout(false);

        }
    }
}
