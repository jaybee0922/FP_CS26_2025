namespace FP_CS26_2025
{
    partial class Form1
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
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.loginFormContainer1 = new FP_CS26_2025.LoginFormDesign.LoginFormContainer();
            this.roleComboBox1 = new FP_CS26_2025.LoginFormDesign.roleComboBox();
            this.usernameInputField2 = new FP_CS26_2025.LoginFormDesign.usernameInputField();
            this.passwordInputField2 = new FP_CS26_2025.LoginFormDesign.passwordInputField();
            this.loginBtn1 = new FP_CS26_2025.LoginFormDesign.LoginBtn();
            this.mainLayout.SuspendLayout();
            this.loginFormContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.BackColor = System.Drawing.Color.Transparent;
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.loginFormContainer1, 0, 0);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.RowCount = 1;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Size = new System.Drawing.Size(1000, 700);
            this.mainLayout.TabIndex = 1;
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
            this.loginFormContainer1.Location = new System.Drawing.Point(205, 129);
            this.loginFormContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loginFormContainer1.Name = "loginFormContainer1";
            this.loginFormContainer1.PanelColor = System.Drawing.Color.White;
            this.loginFormContainer1.ShadowBlur = 28;
            this.loginFormContainer1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(111)))));
            this.loginFormContainer1.ShadowDepth = 7;
            this.loginFormContainer1.ShadowOffsetX = 0;
            this.loginFormContainer1.ShadowOffsetY = 7;
            this.loginFormContainer1.Size = new System.Drawing.Size(589, 500);
            this.loginFormContainer1.TabIndex = 0;
            // 
            // roleComboBox1
            // 
            this.roleComboBox1.BackColor = System.Drawing.Color.White;
            this.roleComboBox1.BorderColor = System.Drawing.Color.Gray;
            this.roleComboBox1.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.roleComboBox1.BorderRadius = 20;
            this.roleComboBox1.BorderSize = 1;
            this.roleComboBox1.Location = new System.Drawing.Point(76, 280);
            this.roleComboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.roleComboBox1.Name = "roleComboBox1";
            this.roleComboBox1.SelectedIndex = 0;
            this.roleComboBox1.SelectedItem = "Super Admin";
            this.roleComboBox1.SelectedText = "";
            this.roleComboBox1.Size = new System.Drawing.Size(432, 46);
            this.roleComboBox1.TabIndex = 7;
            this.roleComboBox1.TextLeftMargin = 12;
            this.roleComboBox1.Load += new System.EventHandler(this.roleComboBox1_Load);
            // 
            // usernameInputField2
            // 
            this.usernameInputField2.BackColor = System.Drawing.Color.White;
            this.usernameInputField2.BorderColor = System.Drawing.Color.Gray;
            this.usernameInputField2.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.usernameInputField2.BorderRadius = 20;
            this.usernameInputField2.BorderSize = 1;
            this.usernameInputField2.Location = new System.Drawing.Point(76, 110);
            this.usernameInputField2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.usernameInputField2.Name = "usernameInputField2";
            this.usernameInputField2.PlaceholderText = "Enter username";
            this.usernameInputField2.Size = new System.Drawing.Size(432, 50);
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
            this.passwordInputField2.Location = new System.Drawing.Point(76, 195);
            this.passwordInputField2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.passwordInputField2.Name = "passwordInputField2";
            this.passwordInputField2.PlaceholderText = "Enter password";
            this.passwordInputField2.Size = new System.Drawing.Size(432, 50);
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
            this.loginBtn1.Location = new System.Drawing.Point(76, 360);
            this.loginBtn1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.loginBtn1.Name = "loginBtn1";
            this.loginBtn1.Size = new System.Drawing.Size(432, 54);
            this.loginBtn1.TabIndex = 0;
            this.loginBtn1.Text = "Login";
            this.loginBtn1.UseVisualStyleBackColor = true;
            this.loginBtn1.Click += new System.EventHandler(this.loginFormBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.mainLayout);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotel Management System - Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainLayout.ResumeLayout(false);
            this.loginFormContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LoginFormDesign.LoginFormContainer loginFormContainer1;
        private LoginFormDesign.LoginBtn loginBtn1;
        private LoginFormDesign.passwordInputField passwordInputField2;
        private LoginFormDesign.usernameInputField usernameInputField2;
        private LoginFormDesign.roleComboBox roleComboBox1;
        private System.Windows.Forms.TableLayoutPanel mainLayout;
    }
}