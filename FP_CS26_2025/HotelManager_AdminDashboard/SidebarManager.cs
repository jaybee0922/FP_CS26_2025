using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Panel))]
    public class SidebarManager : Panel, IDisposable
    {
        private Label lblHotelManager;
        private Button btnDashboard, btnUserManagement, btnRoomRates, btnReports, btnSystemConfig;
        private Action<Button> navigationHandler;

        // Events for navigation
        public event EventHandler<Button> NavigationButtonClicked;
        public event EventHandler DashboardClicked;
        public event EventHandler UserManagementClicked;
        public event EventHandler RoomRatesClicked;
        public event EventHandler ReportsClicked;
        public event EventHandler SystemConfigClicked;

        public SidebarManager()
        {
            InitializeComponent();
            CreateSidebar();
            
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Set up the sidebar panel properties
            this.Size = new Size(220, 800);
            this.BackColor = Color.FromArgb(51, 51, 76);
            this.BorderStyle = BorderStyle.FixedSingle;

            this.ResumeLayout(false);
        }

        private void CreateSidebar()
        {
            lblHotelManager = new Label
            {
                Text = "Hotel Manager",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(13, 20),
                Size = new Size(180, 30)
            };


            // ********** Rename SIDEBAR MENU titles here ******************

            btnDashboard = CreateNavButton("Dashboard", 80);
            btnUserManagement = CreateNavButton("User management", 130);
            btnRoomRates = CreateNavButton("Room Rates and Policies", 180);
            btnReports = CreateNavButton("Reports", 230);
            btnSystemConfig = CreateNavButton("System Configuration", 280);

            this.Controls.AddRange(new Control[] {
                lblHotelManager, btnDashboard, btnUserManagement,
                btnRoomRates, btnReports, btnSystemConfig
            });
        }

        private Button CreateNavButton(string text, int top)
        {
            var button = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(51, 51, 76),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Size = new Size(200, 40),
                Location = new Point(10, top),
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand,
                Tag = text // Store button text for identification
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(61, 61, 86);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(71, 71, 96);

            button.Click += (s, e) =>
            {
                navigationHandler?.Invoke(button);
                NavigationButtonClicked?.Invoke(this, button);

                // Raise specific events
                switch (text)
                {
                    case "Dashboard":
                        DashboardClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "User Management":
                        UserManagementClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Room Rates and Policies":
                        RoomRatesClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Reports":
                        ReportsClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "System Configuration":
                        SystemConfigClicked?.Invoke(this, EventArgs.Empty);
                        break;
                }
            };

            return button;
        }

        // Public methods
        public void SetNavigationHandler(Action<Button> handler)
        {
            navigationHandler = handler;
        }

        public Button GetDashboardButton() => btnDashboard;

        public Button GetButtonByText(string buttonText)
        {
            var buttons = new[] { btnDashboard, btnUserManagement, btnRoomRates, btnReports, btnSystemConfig };
            foreach (var button in buttons)
            {
                if (button?.Text == buttonText)
                    return button;
            }
            return null;
        }

        public void SelectButton(Button button)
        {
            // Reset all buttons to default color
            ResetButtonColors();

            // Set selected button color
            if (button != null)
            {
                button.BackColor = Color.FromArgb(70, 130, 180);
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(80, 140, 190);
            }
        }

        public void SelectButtonByText(string buttonText)
        {
            var button = GetButtonByText(buttonText);
            SelectButton(button);
        }

        public void ResetButtonColors()
        {
            var buttons = new[] { btnDashboard, btnUserManagement, btnRoomRates, btnReports, btnSystemConfig };
            foreach (var button in buttons)
            {
                if (button != null)
                {
                    button.BackColor = Color.FromArgb(51, 51, 76);
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(61, 61, 86);
                }
            }
        }

        public void EnableAllButtons()
        {
            SetButtonsEnabled(true);
        }

        public void DisableAllButtons()
        {
            SetButtonsEnabled(false);
        }

        private void SetButtonsEnabled(bool enabled)
        {
            var buttons = new[] { btnDashboard, btnUserManagement, btnRoomRates, btnReports, btnSystemConfig };
            foreach (var button in buttons)
            {
                if (button != null)
                {
                    button.Enabled = enabled;
                    button.BackColor = enabled ? Color.FromArgb(51, 51, 76) : Color.FromArgb(30, 30, 46);
                }
            }
        }

        // Properties for external access
        [Category("Appearance")]
        [Description("Title text displayed in the sidebar")]
        public string SidebarTitle
        {
            get => lblHotelManager?.Text ?? "Hotel Manager";
            set
            {
                if (lblHotelManager != null)
                    lblHotelManager.Text = value;
            }
        }

        [Category("Appearance")]
        [Description("Background color of the sidebar")]
        public new Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                UpdateButtonColors();
            }
        }

        [Category("Appearance")]
        [Description("Background color of navigation buttons")]
        public Color ButtonBackColor
        {
            get => btnDashboard?.BackColor ?? Color.FromArgb(51, 51, 76);
            set => SetButtonProperty(button => button.BackColor = value);
        }

        [Category("Appearance")]
        [Description("Text color of navigation buttons")]
        public Color ButtonTextColor
        {
            get => btnDashboard?.ForeColor ?? Color.White;
            set => SetButtonProperty(button => button.ForeColor = value);
        }

        [Category("Appearance")]
        [Description("Hover background color of navigation buttons")]
        public Color ButtonHoverColor { get; set; } = Color.FromArgb(61, 61, 86);

        [Category("Appearance")]
        [Description("Selected button background color")]
        public Color SelectedButtonColor { get; set; } = Color.FromArgb(70, 130, 180);

        [Category("Behavior")]
        [Description("Indicates if navigation buttons are enabled")]
        public bool ButtonsEnabled
        {
            get => btnDashboard?.Enabled ?? true;
            set => SetButtonsEnabled(value);
        }

        [Category("Data")]
        [Description("Gets the currently selected button")]
        [Browsable(false)]
        public Button SelectedButton { get; private set; }

        // Helper method to set properties on all buttons
        private void SetButtonProperty(Action<Button> action)
        {
            var buttons = new[] { btnDashboard, btnUserManagement, btnRoomRates, btnReports, btnSystemConfig };
            foreach (var button in buttons)
            {
                if (button != null)
                {
                    action(button);
                }
            }
        }

        private void UpdateButtonColors()
        {
            // Update button hover colors based on current background
            SetButtonProperty(button =>
            {
                button.FlatAppearance.MouseOverBackColor = ButtonHoverColor;
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(
                    Math.Min(ButtonHoverColor.R + 10, 255),
                    Math.Min(ButtonHoverColor.G + 10, 255),
                    Math.Min(ButtonHoverColor.B + 10, 255)
                );
            });
        }

        // Hide inherited properties that don't make sense for this control
        [Browsable(false)]
        public new bool AutoSize => base.AutoSize;

        [Browsable(false)]
        public new AutoSizeMode AutoSizeMode => base.AutoSizeMode;

        [Browsable(false)]
        public override string Text => base.Text;

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            if (lblHotelManager != null)
            {
                // Center the title horizontally
                //lblHotelManager.Location = new Point(
                //    (this.Width - lblHotelManager.Width) / 2,
                //    20
                //);

                // Adjust button widths to fit sidebar
                int buttonWidth = this.Width - 20;
                SetButtonProperty(button => button.Width = buttonWidth);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lblHotelManager?.Dispose();
                btnDashboard?.Dispose();
                btnUserManagement?.Dispose();
                btnRoomRates?.Dispose();
                btnReports?.Dispose();
                btnSystemConfig?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}