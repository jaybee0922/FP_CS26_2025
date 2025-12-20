using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace FP_CS26_2025
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Panel))]
    public class FrontDesk_SidebarManager : Panel
    {
        private Label lblFrontDesk;
        private Button btnDashboard, btnCheckIn, btnCheckOut, btnRoomsCalendar, btnGuestList, btnBilling;
        private Action<Button> navigationHandler;

        // Events for navigation
        public event EventHandler<Button> NavigationButtonClicked;
        public event EventHandler DashboardClicked;
        public event EventHandler CheckInClicked;
        public event EventHandler CheckOutClicked;
        public event EventHandler RoomsCalendarClicked;
        public event EventHandler GuestListClicked;
        public event EventHandler BillingClicked;

        public FrontDesk_SidebarManager()
        {
            InitializeComponent();
            CreateSidebar();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Set up the sidebar panel properties
            this.Size = new Size(220, 800);
            this.BackColor = Color.FromArgb(0, 64, 64);
            this.BorderStyle = BorderStyle.FixedSingle;

            this.ResumeLayout(false);
        }

        private void CreateSidebar()
        {
            lblFrontDesk = new Label
            {
                Text = "Front Desk",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(13, 20),
                Size = new Size(180, 30)
            };

            btnDashboard = CreateNavButton("Dashboard", 80);
            btnCheckIn = CreateNavButton("Check-In", 130);
            btnCheckOut = CreateNavButton("Check-Out", 180);
            btnRoomsCalendar = CreateNavButton("Rooms & Calendar", 230);
            btnGuestList = CreateNavButton("Guest List", 280);
            btnBilling = CreateNavButton("Billing", 330);

            this.Controls.AddRange(new Control[] {
                lblFrontDesk, btnDashboard, btnCheckIn,
                btnCheckOut, btnRoomsCalendar, btnGuestList, btnBilling
            });
        }

        private Button CreateNavButton(string text, int top)
        {
            var button = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(0, 64, 64),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft,
                Size = new Size(200, 40),
                Location = new Point(10, top),
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand,
                Tag = text // Store button text for identification
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 80);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 100, 100);

            button.Click += (s, e) =>
            {
                navigationHandler?.Invoke(button);
                NavigationButtonClicked?.Invoke(this, button);

                // Raise specific eventss
                switch (text)
                {
                    case "Dashboard":
                        DashboardClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Check-In":
                        CheckInClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Check-Out":
                        CheckOutClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Rooms & Calendar":
                        RoomsCalendarClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Guest List":
                        GuestListClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Billing":
                        BillingClicked?.Invoke(this, EventArgs.Empty);
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
            var buttons = new[] { btnDashboard, btnCheckIn, btnCheckOut, btnRoomsCalendar, btnGuestList, btnBilling };
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
                button.BackColor = Color.FromArgb(0, 128, 128);
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 140, 140);
            }
        }

        public void SelectButtonByText(string buttonText)
        {
            var button = GetButtonByText(buttonText);
            SelectButton(button);
        }

        public void ResetButtonColors()
        {
            var buttons = new[] { btnDashboard, btnCheckIn, btnCheckOut, btnRoomsCalendar, btnGuestList, btnBilling };
            foreach (var button in buttons)
            {
                if (button != null)
                {
                    button.BackColor = Color.FromArgb(0, 64, 64);
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 80);
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
            var buttons = new[] { btnDashboard, btnCheckIn, btnCheckOut, btnRoomsCalendar, btnGuestList, btnBilling };
            foreach (var button in buttons)
            {
                if (button != null)
                {
                    button.Enabled = enabled;
                    button.BackColor = enabled ? Color.FromArgb(0, 64, 64) : Color.FromArgb(0, 40, 40);
                }
            }
        }

        // Properties for external access
        [Category("Appearance")]
        [Description("Title text displayed in the sidebar")]
        public string SidebarTitle
        {
            get => lblFrontDesk?.Text ?? "Front Desk";
            set
            {
                if (lblFrontDesk != null)
                    lblFrontDesk.Text = value;
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
            get => btnDashboard?.BackColor ?? Color.FromArgb(0, 64, 64);
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
        public Color ButtonHoverColor { get; set; } = Color.FromArgb(0, 80, 80);

        [Category("Appearance")]
        [Description("Selected button background color")]
        public Color SelectedButtonColor { get; set; } = Color.FromArgb(0, 128, 128);

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
            var buttons = new[] { btnDashboard, btnCheckIn, btnCheckOut, btnRoomsCalendar, btnGuestList, btnBilling };
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
            if (lblFrontDesk != null)
            {
                
                int buttonWidth = this.Width - 20;
                SetButtonProperty(button => button.Width = buttonWidth);
            }
        }

    }
}
