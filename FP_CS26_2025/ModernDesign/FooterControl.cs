using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FP_CS26_2025.ModernDesign
{
    public partial class FooterControl : UserControl
    {
        private Label lblAddress;
        private Label lblContact;
        private Label lblCopyright;

        public FooterControl()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent; // Ensure it blends with gradient
        }

        private void InitializeComponent()
        {
            this.lblAddress = new Label();
            this.lblContact = new Label();
            this.lblCopyright = new Label();
            this.SuspendLayout();

            // lblAddress
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblAddress.ForeColor = Color.White;
            this.lblAddress.Location = new Point(0, 0);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new Size(100, 19);
            this.lblAddress.TabIndex = 0;
            this.lblAddress.Text = "Address";

            // lblContact
            this.lblContact.AutoSize = true;
            this.lblContact.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblContact.ForeColor = Color.White;
            this.lblContact.Location = new Point(0, 25);
            this.lblContact.Name = "lblContact";
            this.lblContact.Size = new Size(100, 19);
            this.lblContact.TabIndex = 1;
            this.lblContact.Text = "Contact";

            // lblCopyright
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            this.lblCopyright.ForeColor = Color.FromArgb(200, 200, 200);
            this.lblCopyright.Location = new Point(0, 50);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new Size(100, 15);
            this.lblCopyright.TabIndex = 2;
            this.lblCopyright.Text = "Copyright";

            // FooterControl
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.lblContact);
            this.Controls.Add(this.lblCopyright);
            this.Name = "FooterControl";
            this.Size = new Size(400, 80);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public void UpdateInfo(string address, string contact, string copyright)
        {
            lblAddress.Text = address;
            lblContact.Text = contact;
            lblCopyright.Text = copyright;
        }
    }
}
