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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.loginFormContainer1 = new FP_CS26_2025.LoginFormDesign.LoginFormContainer();
            this.loginBtn1 = new FP_CS26_2025.LoginFormDesign.LoginBtn();
            this.usernameInputField1 = new FP_CS26_2025.LoginFormDesign.usernameInputField();
            this.loginFormContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginFormContainer1
            // 
            this.loginFormContainer1.BackColor = System.Drawing.Color.Transparent;
            this.loginFormContainer1.BorderRadius = 50;
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
            // loginBtn1
            // 
            this.loginBtn1.BorderRadius = 15;
            this.loginBtn1.Location = new System.Drawing.Point(76, 318);
            this.loginBtn1.Name = "loginBtn1";
            this.loginBtn1.Size = new System.Drawing.Size(432, 52);
            this.loginBtn1.TabIndex = 0;
            this.loginBtn1.Text = "Login";
            this.loginBtn1.UseVisualStyleBackColor = true;
            // 
            // usernameInputField1
            // 
            this.usernameInputField1.BackColor = System.Drawing.Color.White;
            this.usernameInputField1.BorderColor = System.Drawing.Color.Gray;
            this.usernameInputField1.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.usernameInputField1.BorderRadius = 20;
            this.usernameInputField1.BorderSize = 1;
            this.usernameInputField1.Icon = ((System.Drawing.Image)(resources.GetObject("usernameInputField1.Icon")));
            this.usernameInputField1.Location = new System.Drawing.Point(76, 74);
            this.usernameInputField1.Name = "usernameInputField1";
            this.usernameInputField1.Padding = new System.Windows.Forms.Padding(8);
            this.usernameInputField1.PlaceholderText = "Enter username";
            this.usernameInputField1.Size = new System.Drawing.Size(432, 40);
            this.usernameInputField1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 546);
            this.Controls.Add(this.loginFormContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.loginFormContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private LoginFormDesign.LoginFormContainer loginFormContainer1;
        private LoginFormDesign.LoginBtn loginBtn1;
        private LoginFormDesign.usernameInputField usernameInputField1;
    }
}

