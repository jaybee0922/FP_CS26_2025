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
            this.loginFormContainer1 = new FP_CS26_2025.LoginFormDesign.LoginFormContainer();
            this.passwordInputField1 = new FP_CS26_2025.LoginFormDesign.passwordInputField();
            this.usernameInputField1 = new FP_CS26_2025.LoginFormDesign.usernameInputField();
            this.loginBtn1 = new FP_CS26_2025.LoginFormDesign.LoginBtn();
            this.roleComboBox1 = new FP_CS26_2025.LoginFormDesign.roleComboBox();
            this.loginFormContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginFormContainer1
            // 
            this.loginFormContainer1.BackColor = System.Drawing.Color.Transparent;
            this.loginFormContainer1.BorderRadius = 50;
            this.loginFormContainer1.Controls.Add(this.roleComboBox1);
            this.loginFormContainer1.Controls.Add(this.passwordInputField1);
            this.loginFormContainer1.Controls.Add(this.usernameInputField1);
            this.loginFormContainer1.Controls.Add(this.loginBtn1);
            this.loginFormContainer1.Location = new System.Drawing.Point(148, 48);
            this.loginFormContainer1.Name = "loginFormContainer1";
            this.loginFormContainer1.PanelColor = System.Drawing.Color.White;
            this.loginFormContainer1.ShadowBlur = 28;
            this.loginFormContainer1.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(111)))));
            this.loginFormContainer1.ShadowDepth = 7;
            this.loginFormContainer1.ShadowOffsetX = 0;
            this.loginFormContainer1.ShadowOffsetY = 7;
            this.loginFormContainer1.Size = new System.Drawing.Size(590, 440);
            this.loginFormContainer1.TabIndex = 0;
            // 
            // passwordInputField1
            // 
            this.passwordInputField1.BackColor = System.Drawing.Color.White;
            this.passwordInputField1.BorderColor = System.Drawing.Color.Gray;
            this.passwordInputField1.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.passwordInputField1.BorderRadius = 20;
            this.passwordInputField1.BorderSize = 1;
            this.passwordInputField1.IconRightMargin = 12;
            this.passwordInputField1.Location = new System.Drawing.Point(76, 165);
            this.passwordInputField1.Name = "passwordInputField1";
            this.passwordInputField1.Padding = new System.Windows.Forms.Padding(8);
            this.passwordInputField1.PlaceholderText = "Enter password";
            this.passwordInputField1.Size = new System.Drawing.Size(432, 45);
            this.passwordInputField1.TabIndex = 4;
            this.passwordInputField1.TextLeftMargin = 8;
            // 
            // usernameInputField1
            // 
            this.usernameInputField1.BackColor = System.Drawing.Color.White;
            this.usernameInputField1.BorderColor = System.Drawing.Color.Gray;
            this.usernameInputField1.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.usernameInputField1.BorderRadius = 20;
            this.usernameInputField1.BorderSize = 1;
            this.usernameInputField1.Location = new System.Drawing.Point(76, 100);
            this.usernameInputField1.Name = "usernameInputField1";
            this.usernameInputField1.Padding = new System.Windows.Forms.Padding(8);
            this.usernameInputField1.PlaceholderText = "Enter username";
            this.usernameInputField1.Size = new System.Drawing.Size(432, 45);
            this.usernameInputField1.TabIndex = 1;
            this.usernameInputField1.TextLeftMargin = 12;
            // 
            // loginBtn1
            // 
            this.loginBtn1.BackColor = System.Drawing.Color.Transparent;
            this.loginBtn1.BorderRadius = 15;
            this.loginBtn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginBtn1.ForeColor = System.Drawing.Color.White;
            this.loginBtn1.Location = new System.Drawing.Point(76, 317);
            this.loginBtn1.Name = "loginBtn1";
            this.loginBtn1.Size = new System.Drawing.Size(432, 54);
            this.loginBtn1.TabIndex = 0;
            this.loginBtn1.Text = "Login";
            this.loginBtn1.UseVisualStyleBackColor = true;
            this.loginBtn1.Click += new System.EventHandler(this.loginFormBtn_Click);
            // 
            // roleComboBox1
            // 
            this.roleComboBox1.BackColor = System.Drawing.Color.White;
            this.roleComboBox1.BorderColor = System.Drawing.Color.Gray;
            this.roleComboBox1.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.roleComboBox1.BorderRadius = 20;
            this.roleComboBox1.BorderSize = 1;
            this.roleComboBox1.Location = new System.Drawing.Point(76, 251);
            this.roleComboBox1.Name = "roleComboBox1";
            this.roleComboBox1.SelectedIndex = 0;
            this.roleComboBox1.SelectedItem = "Super Admin";
            this.roleComboBox1.SelectedText = "";
            this.roleComboBox1.Size = new System.Drawing.Size(432, 45);
            this.roleComboBox1.TabIndex = 5;
            this.roleComboBox1.TextLeftMargin = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 546);
            this.Controls.Add(this.loginFormContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.loginFormContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LoginFormDesign.LoginFormContainer loginFormContainer1;
        private LoginFormDesign.LoginBtn loginBtn1;
        private LoginFormDesign.usernameInputField usernameInputField1;
        private LoginFormDesign.passwordInputField passwordInputField1;
        private LoginFormDesign.roleComboBox roleComboBox1;
    }
}