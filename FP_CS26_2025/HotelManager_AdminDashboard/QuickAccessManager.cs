using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace FP_CS26_2025.HotelManager_AdminDashboard
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Panel))]
    public class QuickAccessManager : Panel, IDisposable
    {
        private Label lblQuickAccess;

        // Quick access buttons
        private Button btnManageRooms;
        private Button btnManageGuests;
        private Button btnProcessPayments;
        private Button btnRoomCalendar;
        private Button btnAdjustPricing;
        private Button btnManageStaff;

        // Events for button clicks
        public event EventHandler<string> QuickAccessButtonClicked;
        public event EventHandler ManageRoomsClicked;
        public event EventHandler ManageGuestsClicked;
        public event EventHandler ProcessPaymentsClicked;
        public event EventHandler RoomCalendarClicked;
        public event EventHandler AdjustPricingClicked;
        public event EventHandler ManageStaffClicked;

        public QuickAccessManager()
        {
            InitializeComponent();
            CreateQuickAccessPanel();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Set up the main panel properties
            this.Size = new Size(600, 250); // Increased height to accommodate margin
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.ResumeLayout(false);
        }

        private void CreateQuickAccessPanel()
        {
            AddQuickAccessTitle();
            CreateQuickAccessButtons();
        }

        private void AddQuickAccessTitle()
        {
            lblQuickAccess = new Label
            {
                Text = "Quick Access Functions",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(15, 15),
                Size = new Size(200, 25),
                ForeColor = Color.FromArgb(51, 51, 76)
            };
            this.Controls.Add(lblQuickAccess);
        }

        private void CreateQuickAccessButtons()
        {
            int buttonWidth = 160;
            int buttonHeight = 50;
            int horizontalSpacing = 20;
            int verticalSpacing = 20;

            // Increased margin between title and buttons
            int titleBottomMargin = 25; // Added margin space

            int totalWidth = (buttonWidth * 3) + (horizontalSpacing * 2);
            int startX = (this.Width - totalWidth) / 2;

            // Start buttons lower to create margin from title
            int row1Y = lblQuickAccess.Location.Y + lblQuickAccess.Height + titleBottomMargin;
            int row2Y = row1Y + buttonHeight + verticalSpacing;

            // Column 1
            btnManageRooms = CreateQuickAccessButton("Manage\nRooms", startX, row1Y, buttonWidth, buttonHeight);
            btnManageGuests = CreateQuickAccessButton("Manage\nGuests", startX, row2Y, buttonWidth, buttonHeight);

            // Column 2
            btnProcessPayments = CreateQuickAccessButton("Process\nPayments", startX + buttonWidth + horizontalSpacing, row1Y, buttonWidth, buttonHeight);
            btnRoomCalendar = CreateQuickAccessButton("Room\nCalendar", startX + buttonWidth + horizontalSpacing, row2Y, buttonWidth, buttonHeight);

            // Column 3
            btnAdjustPricing = CreateQuickAccessButton("Adjust\nPricing", startX + (buttonWidth + horizontalSpacing) * 2, row1Y, buttonWidth, buttonHeight);
            btnManageStaff = CreateQuickAccessButton("Manage\nStaff", startX + (buttonWidth + horizontalSpacing) * 2, row2Y, buttonWidth, buttonHeight);

            this.Controls.AddRange(new Control[] {
                btnManageRooms, btnProcessPayments, btnAdjustPricing,
                btnManageGuests, btnRoomCalendar, btnManageStaff
            });
        }

        private Button CreateQuickAccessButton(string text, int x, int y, int width, int height)
        {
            var button = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 51, 76),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(width, height),
                Location = new Point(x, y),
                Font = new Font("Segoe UI", 8, FontStyle.Regular),
                Cursor = Cursors.Hand,
                Tag = text // Store the button text for identification
            };

            button.FlatAppearance.BorderColor = Color.FromArgb(70, 130, 180);
            button.FlatAppearance.BorderSize = 2;

            // Animation effects
            button.MouseEnter += (s, e) =>
            {
                button.BackColor = Color.FromArgb(70, 130, 180);
                button.ForeColor = Color.White;
                button.FlatAppearance.BorderColor = Color.FromArgb(90, 150, 200);
            };

            button.MouseLeave += (s, e) =>
            {
                button.BackColor = Color.White;
                button.ForeColor = Color.FromArgb(51, 51, 76);
                button.FlatAppearance.BorderColor = Color.FromArgb(70, 130, 180);
            };

            button.MouseDown += (s, e) => button.BackColor = Color.FromArgb(50, 110, 160);
            button.MouseUp += (s, e) => HandleButtonClick(button);

            return button;
        }

        private void HandleButtonClick(Button button)
        {
            string buttonText = button.Tag as string ?? button.Text;

            // Visual feedback
            button.BackColor = Color.FromArgb(70, 130, 180);

            var timer = new Timer { Interval = 150 };
            timer.Tick += (timerSender, timerE) =>
            {
                button.BackColor = Color.White;
                button.ForeColor = Color.FromArgb(51, 51, 76);
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();

            // Raise events
            QuickAccessButtonClicked?.Invoke(this, buttonText);

            // Raise specific events based on button
            switch (buttonText.Replace("\n", " "))
            {
                case "Manage Rooms":
                    ManageRoomsClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "Manage Guests":
                    ManageGuestsClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "Process Payments":
                    ProcessPaymentsClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "Room Calendar":
                    RoomCalendarClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "Adjust Pricing":
                    AdjustPricingClicked?.Invoke(this, EventArgs.Empty);
                    break;
                case "Manage Staff":
                    ManageStaffClicked?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        // Public methods
        public void LoadSampleData()
        {
            // This method is for consistency with other managers
            // Quick access doesn't need sample data, but method is provided for interface consistency
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
            var buttons = new[] { btnManageRooms, btnManageGuests, btnProcessPayments, btnRoomCalendar, btnAdjustPricing, btnManageStaff };

            foreach (var button in buttons)
            {
                if (button != null)
                {
                    button.Enabled = enabled;
                    button.BackColor = enabled ? Color.White : Color.LightGray;
                }
            }
        }

        // Properties for external access
        [Category("Appearance")]
        [Description("Title text for the quick access panel")]
        public string PanelTitle
        {
            get => lblQuickAccess?.Text ?? "Quick Access Functions";
            set
            {
                if (lblQuickAccess != null)
                    lblQuickAccess.Text = value;
            }
        }

        [Category("Appearance")]
        [Description("Margin between title and buttons in pixels")]
        public int TitleBottomMargin { get; set; } = 25;

        [Category("Appearance")]
        [Description("Background color of the buttons")]
        public Color ButtonBackColor
        {
            get => btnManageRooms?.BackColor ?? Color.White;
            set => SetButtonProperty(button => button.BackColor = value);
        }

        [Category("Appearance")]
        [Description("Border color of the buttons")]
        public Color ButtonBorderColor
        {
            get => btnManageRooms?.FlatAppearance.BorderColor ?? Color.FromArgb(70, 130, 180);
            set => SetButtonProperty(button => button.FlatAppearance.BorderColor = value);
        }

        [Category("Appearance")]
        [Description("Text color of the buttons")]
        public Color ButtonTextColor
        {
            get => btnManageRooms?.ForeColor ?? Color.FromArgb(51, 51, 76);
            set => SetButtonProperty(button => button.ForeColor = value);
        }

        [Category("Appearance")]
        [Description("Hover background color of the buttons")]
        public Color ButtonHoverColor { get; set; } = Color.FromArgb(70, 130, 180);

        [Category("Appearance")]
        [Description("Hover text color of the buttons")]
        public Color ButtonHoverTextColor { get; set; } = Color.White;

        [Category("Behavior")]
        [Description("Indicates if buttons are enabled")]
        public bool ButtonsEnabled
        {
            get => btnManageRooms?.Enabled ?? true;
            set => SetButtonsEnabled(value);
        }

        // Helper method to set properties on all buttons
        private void SetButtonProperty(Action<Button> action)
        {
            var buttons = new[] { btnManageRooms, btnManageGuests, btnProcessPayments, btnRoomCalendar, btnAdjustPricing, btnManageStaff };

            foreach (var button in buttons)
            {
                if (button != null)
                {
                    action(button);
                }
            }
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
            RepositionButtons();
        }

        private void RepositionButtons()
        {
            if (btnManageRooms == null || lblQuickAccess == null) return;

            int buttonWidth = 160;
            int buttonHeight = 50;
            int horizontalSpacing = 20;
            int verticalSpacing = 20;

            int totalWidth = (buttonWidth * 3) + (horizontalSpacing * 2);
            int startX = (this.Width - totalWidth) / 2;

            // Use the TitleBottomMargin property for consistent spacing
            int row1Y = lblQuickAccess.Location.Y + lblQuickAccess.Height + TitleBottomMargin;
            int row2Y = row1Y + buttonHeight + verticalSpacing;

            // Reposition all buttons
            btnManageRooms.Location = new Point(startX, row1Y);
            btnManageGuests.Location = new Point(startX, row2Y);

            btnProcessPayments.Location = new Point(startX + buttonWidth + horizontalSpacing, row1Y);
            btnRoomCalendar.Location = new Point(startX + buttonWidth + horizontalSpacing, row2Y);

            btnAdjustPricing.Location = new Point(startX + (buttonWidth + horizontalSpacing) * 2, row1Y);
            btnManageStaff.Location = new Point(startX + (buttonWidth + horizontalSpacing) * 2, row2Y);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                lblQuickAccess?.Dispose();
                btnManageRooms?.Dispose();
                btnManageGuests?.Dispose();
                btnProcessPayments?.Dispose();
                btnRoomCalendar?.Dispose();
                btnAdjustPricing?.Dispose();
                btnManageStaff?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}