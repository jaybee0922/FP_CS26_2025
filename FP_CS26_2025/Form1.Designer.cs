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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.passwordInputField1 = new FP_CS26_2025.LoginFormDesign.passwordInputField();
            this.usernameInputField1 = new FP_CS26_2025.LoginFormDesign.usernameInputField();
            this.loginBtn1 = new FP_CS26_2025.LoginFormDesign.LoginBtn();
            this.loginFormContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginFormContainer1
            // 
            this.loginFormContainer1.BackColor = System.Drawing.Color.Transparent;
            this.loginFormContainer1.BorderRadius = 50;
            this.loginFormContainer1.Controls.Add(this.comboBox1);
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
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Super Admin",
            "Front Desk"});
            this.comboBox1.Location = new System.Drawing.Point(76, 251);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(432, 24);
            this.comboBox1.TabIndex = 3;
<<<<<<< HEAD
=======
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
>>>>>>> 77dd27b (Added some animation for button in front desk dashboard -Orpia)
            // 
            // passwordInputField1
            // 
            this.passwordInputField1.BackColor = System.Drawing.Color.White;
            this.passwordInputField1.BorderColor = System.Drawing.Color.Gray;
            this.passwordInputField1.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.passwordInputField1.BorderRadius = 20;
            this.passwordInputField1.BorderSize = 1;
<<<<<<< HEAD
            this.passwordInputField1.Icon = ((System.Drawing.Image)(resources.GetObject("passwordInputField1.Icon")));
=======
            this.passwordInputField1.Icon = null;
>>>>>>> 77dd27b (Added some animation for button in front desk dashboard -Orpia)
            this.passwordInputField1.Location = new System.Drawing.Point(76, 166);
            this.passwordInputField1.Name = "passwordInputField1";
            this.passwordInputField1.Padding = new System.Windows.Forms.Padding(8);
            this.passwordInputField1.PlaceholderText = "Enter password";
            this.passwordInputField1.Size = new System.Drawing.Size(432, 54);
            this.passwordInputField1.TabIndex = 2;
<<<<<<< HEAD
=======
            this.passwordInputField1.Load += new System.EventHandler(this.passwordFieldBox_Load);

>>>>>>> 77dd27b (Added some animation for button in front desk dashboard -Orpia)
            // 
            // usernameInputField1
            // 
            this.usernameInputField1.BackColor = System.Drawing.Color.White;
            this.usernameInputField1.BorderColor = System.Drawing.Color.Gray;
            this.usernameInputField1.BorderFocusColor = System.Drawing.Color.DeepSkyBlue;
            this.usernameInputField1.BorderRadius = 20;
            this.usernameInputField1.BorderSize = 1;
            this.usernameInputField1.Icon = ((System.Drawing.Image)(resources.GetObject("usernameInputField1.Icon")));
            this.usernameInputField1.Location = new System.Drawing.Point(76, 72);
            this.usernameInputField1.Name = "usernameInputField1";
            this.usernameInputField1.Padding = new System.Windows.Forms.Padding(8);
            this.usernameInputField1.PlaceholderText = "Enter username";
            this.usernameInputField1.Size = new System.Drawing.Size(432, 54);
            this.usernameInputField1.TabIndex = 1;
<<<<<<< HEAD
            // 
            // loginBtn1
            // 
            this.loginBtn1.BorderRadius = 15;
=======
            this.usernameInputField1.Load += new System.EventHandler(this.usernameFieldBox_Load);

            // 
            // loginBtn1
            // 
            this.loginBtn1.BackColor = System.Drawing.Color.Transparent;
            this.loginBtn1.BorderRadius = 15;
            this.loginBtn1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loginBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginBtn1.ForeColor = System.Drawing.Color.White;
>>>>>>> 77dd27b (Added some animation for button in front desk dashboard -Orpia)
            this.loginBtn1.Location = new System.Drawing.Point(76, 317);
            this.loginBtn1.Name = "loginBtn1";
            this.loginBtn1.Size = new System.Drawing.Size(432, 54);
            this.loginBtn1.TabIndex = 0;
            this.loginBtn1.Text = "Login";
            this.loginBtn1.UseVisualStyleBackColor = true;
<<<<<<< HEAD
=======
            this.loginBtn1.Click += new System.EventHandler(this.loginFormBtn_Click);

>>>>>>> 77dd27b (Added some animation for button in front desk dashboard -Orpia)
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
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

