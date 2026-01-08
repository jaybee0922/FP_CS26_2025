namespace FP_CS26_2025
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainBackgroundPanel = new FP_CS26_2025.ModernDesign.GradientPanel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.modernNavbar = new FP_CS26_2025.ModernDesign.ModernNavbar();
            this.loginFormContainer1 = new FP_CS26_2025.LoginFormDesign.LoginFormContainer();
            this.roleComboBox1 = new FP_CS26_2025.LoginFormDesign.RoleComboBox();
            this.usernameInputField2 = new FP_CS26_2025.LoginFormDesign.UsernameInputField();
            this.passwordInputField2 = new FP_CS26_2025.LoginFormDesign.PasswordInputField();
            this.loginBtn1 = new FP_CS26_2025.LoginFormDesign.LoginBtn();
            this.mainBackgroundPanel.SuspendLayout();
            this.loginFormContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainBackgroundPanel
            // 
            this.mainBackgroundPanel.Angle = 60F;
            this.mainBackgroundPanel.ColorBottom = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.mainBackgroundPanel.ColorTop = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(88)))), ((int)(((byte)(118)))));
            this.mainBackgroundPanel.Controls.Add(this.lblLogo);
            this.mainBackgroundPanel.Controls.Add(this.lblSubtitle);
            this.mainBackgroundPanel.Controls.Add(this.lblCopyright);
            this.mainBackgroundPanel.Controls.Add(this.modernNavbar);
            this.mainBackgroundPanel.Controls.Add(this.loginFormContainer1);
            this.mainBackgroundPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainBackgroundPanel.Location = new System.Drawing.Point(0, 0);
            this.mainBackgroundPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.mainBackgroundPanel.Name = "mainBackgroundPanel";
            this.mainBackgroundPanel.Size = new System.Drawing.Size(1200, 700);
            this.mainBackgroundPanel.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.BackColor = System.Drawing.Color.Transparent;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.White;
            this.lblLogo.Location = new System.Drawing.Point(50, 30);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(320, 45);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "THE NEXUS GRAND";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI Semilight", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblSubtitle.Location = new System.Drawing.Point(52, 74);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(306, 25);
            this.lblSubtitle.TabIndex = 9;
            this.lblSubtitle.Text = "EXPERIENCE LUXURY AT ITS FINEST";
            // 
            // lblCopyright
            // 
            this.lblCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.lblCopyright.Location = new System.Drawing.Point(38, 666);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(86, 15);
            this.lblCopyright.TabIndex = 8;
            this.lblCopyright.Text = "Copyright 2025";
            // 
            // modernNavbar
            // 
            this.modernNavbar.ActivePage = "Login";
            this.modernNavbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modernNavbar.BackColor = System.Drawing.Color.Transparent;
            this.modernNavbar.Location = new System.Drawing.Point(700, 55);
            this.modernNavbar.Margin = new System.Windows.Forms.Padding(2);
            this.modernNavbar.Name = "modernNavbar";
            this.modernNavbar.Size = new System.Drawing.Size(450, 50);
            this.modernNavbar.TabIndex = 1;
            // 
            // loginFormContainer1
            // 
            this.loginFormContainer1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loginFormContainer1.BackColor = System.Drawing.Color.Transparent;
            this.loginFormContainer1.BorderRadius = 50;
            this.loginFormContainer1.Controls.Add(this.roleComboBox1);
            this.loginFormContainer1.Controls.Add(this.usernameInputField2);
            this.loginFormContainer1.Controls.Add(this.passwordInputField2);
            this.loginFormContainer1.Controls.Add(this.loginBtn1);
            this.loginFormContainer1.Location = new System.Drawing.Point(379, 147);
            this.loginFormContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loginFormContainer1.Name = "loginFormContainer1";
            this.loginFormContainer1.PanelColor = System.Drawing.Color.White;
            this.loginFormContainer1.ShadowBlur = 28;
            this.loginFormContainer1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(111)))));
            this.loginFormContainer1.ShadowDepth = 7;
            this.loginFormContainer1.ShadowOffsetX = 0;
            this.loginFormContainer1.ShadowOffsetY = 7;
            this.loginFormContainer1.Size = new System.Drawing.Size(442, 406);
            this.loginFormContainer1.TabIndex = 0;
            this.loginFormContainer1.Title = "LOGIN";
            this.loginFormContainer1.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(52)))), ((int)(((byte)(71)))));
            this.loginFormContainer1.TitleFont = new System.Drawing.Font("Segoe UI Semibold", 22F);
            // 
            // roleComboBox1
            // 
            this.roleComboBox1.BackColor = System.Drawing.Color.White;
            this.roleComboBox1.BorderColor = System.Drawing.Color.Gray;
            this.roleComboBox1.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.roleComboBox1.BorderRadius = 20;
            this.roleComboBox1.BorderSize = 1;
            this.roleComboBox1.Location = new System.Drawing.Point(57, 228);
            this.roleComboBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.roleComboBox1.Name = "roleComboBox1";
            this.roleComboBox1.SelectedIndex = 0;
            this.roleComboBox1.SelectedItem = "Super Admin";
            this.roleComboBox1.SelectedText = "";
            this.roleComboBox1.Size = new System.Drawing.Size(324, 37);
            this.roleComboBox1.TabIndex = 7;
            this.roleComboBox1.TextLeftMargin = 12;
            // 
            // usernameInputField2
            // 
            this.usernameInputField2.BackColor = System.Drawing.Color.White;
            this.usernameInputField2.BorderColor = System.Drawing.Color.Gray;
            this.usernameInputField2.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.usernameInputField2.BorderRadius = 20;
            this.usernameInputField2.BorderSize = 1;
            this.usernameInputField2.Location = new System.Drawing.Point(57, 89);
            this.usernameInputField2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.usernameInputField2.Name = "usernameInputField2";
            this.usernameInputField2.PlaceholderText = "Enter username";
            this.usernameInputField2.Size = new System.Drawing.Size(324, 41);
            this.usernameInputField2.TabIndex = 6;
            this.usernameInputField2.TextLeftMargin = 20;
            // 
            // passwordInputField2
            // 
            this.passwordInputField2.BackColor = System.Drawing.Color.White;
            this.passwordInputField2.BorderColor = System.Drawing.Color.Gray;
            this.passwordInputField2.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.passwordInputField2.BorderRadius = 20;
            this.passwordInputField2.BorderSize = 1;
            this.passwordInputField2.IconRightMargin = 12;
            this.passwordInputField2.Location = new System.Drawing.Point(57, 158);
            this.passwordInputField2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.passwordInputField2.Name = "passwordInputField2";
            this.passwordInputField2.PlaceholderText = "Enter password";
            this.passwordInputField2.Size = new System.Drawing.Size(324, 41);
            this.passwordInputField2.TabIndex = 5;
            this.passwordInputField2.TextLeftMargin = 20;
            // 
            // loginBtn1
            // 
            this.loginBtn1.BackColor = System.Drawing.Color.Transparent;
            this.loginBtn1.BorderRadius = 15;
            this.loginBtn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginBtn1.ForeColor = System.Drawing.Color.White;
            this.loginBtn1.Location = new System.Drawing.Point(57, 292);
            this.loginBtn1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loginBtn1.Name = "loginBtn1";
            this.loginBtn1.Size = new System.Drawing.Size(324, 44);
            this.loginBtn1.TabIndex = 0;
            this.loginBtn1.Text = "Login";
            this.loginBtn1.UseVisualStyleBackColor = true;
            this.loginBtn1.Click += new System.EventHandler(this.loginFormBtn_Click);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.mainBackgroundPanel);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotel Management System - Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.mainBackgroundPanel.ResumeLayout(false);
            this.mainBackgroundPanel.PerformLayout();
            this.loginFormContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private FP_CS26_2025.ModernDesign.GradientPanel mainBackgroundPanel;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblSubtitle;
        private FP_CS26_2025.ModernDesign.ModernNavbar modernNavbar;
        private System.Windows.Forms.Label lblCopyright;
        private LoginFormDesign.LoginFormContainer loginFormContainer1;
        private LoginFormDesign.LoginBtn loginBtn1;
        private LoginFormDesign.PasswordInputField passwordInputField2;
        private LoginFormDesign.UsernameInputField usernameInputField2;
        private LoginFormDesign.RoleComboBox roleComboBox1;
    }
}
