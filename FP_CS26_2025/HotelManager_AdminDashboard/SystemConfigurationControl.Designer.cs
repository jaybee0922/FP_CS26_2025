using System.Windows.Forms;
using System.Drawing;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    partial class SystemConfigurationControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControlConfig = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.tabPolicies = new System.Windows.Forms.TabPage();
            this.tabDatabase = new System.Windows.Forms.TabPage();
            
            // General Settings Controls
            this.lblHotelName = new System.Windows.Forms.Label();
            this.txtHotelName = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.txtCopyright = new System.Windows.Forms.TextBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            
            // Policy Controls
            this.lblPolicyConfig = new System.Windows.Forms.Label();
            this.txtPolicyConfig = new System.Windows.Forms.TextBox();
            this.btnSavePolicies = new System.Windows.Forms.Button();

            // Database Settings Controls
            this.lblBackupPath = new System.Windows.Forms.Label();
            this.txtBackupPath = new System.Windows.Forms.TextBox();
            this.btnBrowseBackup = new System.Windows.Forms.Button();
            this.btnBackupNow = new System.Windows.Forms.Button();

            this.tabControlConfig.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.tabPolicies.SuspendLayout();
            this.tabDatabase.SuspendLayout();
            this.tabDatabase.SuspendLayout();
            this.SuspendLayout();

            // 
            // tabControlConfig
            // 
            this.tabControlConfig.Controls.Add(this.tabGeneral);
            this.tabControlConfig.Controls.Add(this.tabPolicies);
            this.tabControlConfig.Controls.Add(this.tabDatabase);
            this.tabControlConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlConfig.Location = new System.Drawing.Point(0, 0);
            this.tabControlConfig.Name = "tabControlConfig";
            this.tabControlConfig.SelectedIndex = 0;
            this.tabControlConfig.Size = new System.Drawing.Size(1000, 600);
            this.tabControlConfig.TabIndex = 0;

            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.btnSaveSettings);
            this.tabGeneral.Controls.Add(this.txtEmail);
            this.tabGeneral.Controls.Add(this.lblEmail);
            this.tabGeneral.Controls.Add(this.txtCopyright);
            this.tabGeneral.Controls.Add(this.lblCopyright);
            this.tabGeneral.Controls.Add(this.txtPhone);
            this.tabGeneral.Controls.Add(this.lblPhone);
            this.tabGeneral.Controls.Add(this.txtAddress);
            this.tabGeneral.Controls.Add(this.lblAddress);
            this.tabGeneral.Controls.Add(this.txtHotelName);
            this.tabGeneral.Controls.Add(this.lblHotelName);
            this.tabGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Padding = new System.Windows.Forms.Padding(30);
            this.tabGeneral.Size = new System.Drawing.Size(992, 574);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General Configuration";
            this.tabGeneral.UseVisualStyleBackColor = true;

            // 
            // lblHotelName
            // 
            this.lblHotelName.AutoSize = true;
            this.lblHotelName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHotelName.Location = new System.Drawing.Point(30, 30);
            this.lblHotelName.Name = "lblHotelName";
            this.lblHotelName.Size = new System.Drawing.Size(91, 19);
            this.lblHotelName.TabIndex = 0;
            this.lblHotelName.Text = "Hotel Name";

            // 
            // txtHotelName
            // 
            this.txtHotelName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHotelName.Location = new System.Drawing.Point(30, 55);
            this.txtHotelName.Name = "txtHotelName";
            this.txtHotelName.Size = new System.Drawing.Size(400, 25);
            this.txtHotelName.TabIndex = 1;

            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAddress.Location = new System.Drawing.Point(30, 95);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(63, 19);
            this.lblAddress.TabIndex = 2;
            this.lblAddress.Text = "Address";

            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAddress.Location = new System.Drawing.Point(30, 120);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(400, 80);
            this.txtAddress.TabIndex = 3;

            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPhone.Location = new System.Drawing.Point(30, 215);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(110, 19);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Phone Number";

            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPhone.Location = new System.Drawing.Point(30, 240);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(400, 25);
            this.txtPhone.TabIndex = 5;

            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Location = new System.Drawing.Point(30, 280);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(45, 19);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email";

            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(30, 305);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(400, 25);
            this.txtEmail.TabIndex = 7;

            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCopyright.Location = new System.Drawing.Point(30, 345);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(76, 19);
            this.lblCopyright.TabIndex = 8;
            this.lblCopyright.Text = "Copyright";

            // 
            // txtCopyright
            // 
            this.txtCopyright.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCopyright.Location = new System.Drawing.Point(30, 370);
            this.txtCopyright.Name = "txtCopyright";
            this.txtCopyright.Size = new System.Drawing.Size(400, 25);
            this.txtCopyright.TabIndex = 9;



            // 
            // btnSaveSettings
            // 
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnSaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveSettings.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveSettings.ForeColor = System.Drawing.Color.White;
            this.btnSaveSettings.Location = new System.Drawing.Point(30, 420);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(150, 40);
            this.btnSaveSettings.TabIndex = 12;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = false;

            // 
            // tabPolicies
            // 
            this.tabPolicies.Controls.Add(this.btnSavePolicies);
            this.tabPolicies.Controls.Add(this.txtPolicyConfig);
            this.tabPolicies.Controls.Add(this.lblPolicyConfig);
            this.tabPolicies.Location = new System.Drawing.Point(4, 22);
            this.tabPolicies.Name = "tabPolicies";
            this.tabPolicies.Padding = new System.Windows.Forms.Padding(30);
            this.tabPolicies.Size = new System.Drawing.Size(992, 574);
            this.tabPolicies.TabIndex = 2;
            this.tabPolicies.Text = "Policies";
            this.tabPolicies.UseVisualStyleBackColor = true;

            // 
            // lblPolicyConfig
            // 
            this.lblPolicyConfig.AutoSize = true;
            this.lblPolicyConfig.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPolicyConfig.Location = new System.Drawing.Point(30, 30);
            this.lblPolicyConfig.Name = "lblPolicyConfig";
            this.lblPolicyConfig.Size = new System.Drawing.Size(320, 19);
            this.lblPolicyConfig.TabIndex = 0;
            this.lblPolicyConfig.Text = "Hotel Policies (Check-in time, Do's and Don'ts):";

            // 
            // txtPolicyConfig
            // 
            this.txtPolicyConfig.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPolicyConfig.Location = new System.Drawing.Point(30, 60);
            this.txtPolicyConfig.Multiline = true;
            this.txtPolicyConfig.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPolicyConfig.Name = "txtPolicyConfig";
            this.txtPolicyConfig.Size = new System.Drawing.Size(600, 300);
            this.txtPolicyConfig.TabIndex = 1;

            // 
            // btnSavePolicies
            // 
            this.btnSavePolicies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.btnSavePolicies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePolicies.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSavePolicies.ForeColor = System.Drawing.Color.White;
            this.btnSavePolicies.Location = new System.Drawing.Point(30, 380);
            this.btnSavePolicies.Name = "btnSavePolicies";
            this.btnSavePolicies.Size = new System.Drawing.Size(150, 40);
            this.btnSavePolicies.TabIndex = 2;
            this.btnSavePolicies.Text = "Save Policies";
            this.btnSavePolicies.UseVisualStyleBackColor = false;

            // 
            // tabDatabase
            // 
            this.tabDatabase.Controls.Add(this.btnBackupNow);
            this.tabDatabase.Controls.Add(this.btnBrowseBackup);
            this.tabDatabase.Controls.Add(this.txtBackupPath);
            this.tabDatabase.Controls.Add(this.lblBackupPath);
            this.tabDatabase.Location = new System.Drawing.Point(4, 22);
            this.tabDatabase.Name = "tabDatabase";
            this.tabDatabase.Padding = new System.Windows.Forms.Padding(30);
            this.tabDatabase.Size = new System.Drawing.Size(992, 574);
            this.tabDatabase.TabIndex = 1;
            this.tabDatabase.Text = "Database Maintenance";
            this.tabDatabase.UseVisualStyleBackColor = true;

            // 
            // lblBackupPath
            // 
            this.lblBackupPath.AutoSize = true;
            this.lblBackupPath.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBackupPath.Location = new System.Drawing.Point(30, 30);
            this.lblBackupPath.Name = "lblBackupPath";
            this.lblBackupPath.Size = new System.Drawing.Size(123, 19);
            this.lblBackupPath.TabIndex = 0;
            this.lblBackupPath.Text = "Backup Directory";

            // 
            // txtBackupPath
            // 
            this.txtBackupPath.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBackupPath.Location = new System.Drawing.Point(30, 55);
            this.txtBackupPath.Name = "txtBackupPath";
            this.txtBackupPath.ReadOnly = true;
            this.txtBackupPath.Size = new System.Drawing.Size(500, 25);
            this.txtBackupPath.TabIndex = 1;

            // 
            // btnBrowseBackup
            // 
            this.btnBrowseBackup.BackColor = System.Drawing.Color.Gray;
            this.btnBrowseBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseBackup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowseBackup.ForeColor = System.Drawing.Color.White;
            this.btnBrowseBackup.Location = new System.Drawing.Point(540, 53);
            this.btnBrowseBackup.Name = "btnBrowseBackup";
            this.btnBrowseBackup.Size = new System.Drawing.Size(80, 29);
            this.btnBrowseBackup.TabIndex = 2;
            this.btnBrowseBackup.Text = "Browse...";
            this.btnBrowseBackup.UseVisualStyleBackColor = false;

            // 
            // btnBackupNow
            // 
            this.btnBackupNow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.btnBackupNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupNow.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnBackupNow.ForeColor = System.Drawing.Color.White;
            this.btnBackupNow.Location = new System.Drawing.Point(30, 100);
            this.btnBackupNow.Name = "btnBackupNow";
            this.btnBackupNow.Size = new System.Drawing.Size(200, 40);
            this.btnBackupNow.TabIndex = 3;
            this.btnBackupNow.Text = "Backup Database Now";
            this.btnBackupNow.UseVisualStyleBackColor = false;

            // 
            // SystemConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tabControlConfig);
            this.Name = "SystemConfigurationControl";
            this.Size = new System.Drawing.Size(1000, 600);
            this.tabControlConfig.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.tabGeneral.PerformLayout();
            this.tabPolicies.ResumeLayout(false);
            this.tabPolicies.PerformLayout();
            this.tabDatabase.ResumeLayout(false);
            this.tabDatabase.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControlConfig;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.TabPage tabPolicies;
        private System.Windows.Forms.TabPage tabDatabase;
        
        // General
        private System.Windows.Forms.Label lblHotelName;
        private System.Windows.Forms.TextBox txtHotelName;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.TextBox txtCopyright;
        private System.Windows.Forms.Button btnSaveSettings;

        // Policies
        private System.Windows.Forms.Label lblPolicyConfig;
        private System.Windows.Forms.TextBox txtPolicyConfig;
        private System.Windows.Forms.Button btnSavePolicies;

        // Database
        private System.Windows.Forms.Label lblBackupPath;
        private System.Windows.Forms.TextBox txtBackupPath;
        private System.Windows.Forms.Button btnBrowseBackup;
        private System.Windows.Forms.Button btnBackupNow;
    }
}
