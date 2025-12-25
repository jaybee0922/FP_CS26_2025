using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace FP_CS26_2025
{
    // Ensure the designer treats this as a UserControl/Panel, not a generic Component
    [System.ComponentModel.DesignerCategory("UserControl")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Panel))]
    public class FrontDesk_SidebarManager : UserControl
    {
        private Label lblFrontDesk;
        private Button btnReservations, btnCheckIn, btnCheckOut, btnRoomAssignments, btnBilling, btnLogout;
        private Action<Button> navigationHandler;

        // Events for navigation
        public event EventHandler<Button> NavigationButtonClicked;
        public event EventHandler ReservationsClicked;
        public event EventHandler CheckInClicked;
        public event EventHandler CheckOutClicked;
        public event EventHandler RoomAssignmentsClicked;
        public event EventHandler BillingClicked;
        public event EventHandler LogoutClicked;

        public FrontDesk_SidebarManager()
        {
            InitializeComponent();
            CreateSidebar();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // Set up the sidebar panel properties
            this.Size = new Size(250, 800);
            this.BackColor = Color.FromArgb(0, 64, 64);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.ResumeLayout(false);
        }

        private void CreateSidebar()
        {
            this.Controls.Clear();

            lblFrontDesk = new Label
            {
                Text = "Front Desk",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 30),
                Size = new Size(210, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            int startY = 100;
            int gap = 10;
            int btnHeight = 45;

            btnReservations = CreateNavButton("Process Reservations", startY);
            btnCheckIn = CreateNavButton("Check-In Guests", startY + (btnHeight + gap) * 1);
            btnCheckOut = CreateNavButton("Check-Out Guests", startY + (btnHeight + gap) * 2);
            btnRoomAssignments = CreateNavButton("Room Assignments", startY + (btnHeight + gap) * 3);
            btnBilling = CreateNavButton("Billing", startY + (btnHeight + gap) * 4);

            btnLogout = CreateNavButton("Logout", 0); 
            btnLogout.BackColor = Color.FromArgb(192, 57, 43); 
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(231, 76, 60);
            btnLogout.Dock = DockStyle.Bottom;
            
            this.Controls.Add(lblFrontDesk);
            this.Controls.AddRange(new Control[] {
                btnReservations, btnCheckIn, btnCheckOut, btnRoomAssignments, btnBilling, btnLogout
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
                Size = new Size(230, 45),
                Location = new Point(10, top),
                Font = new Font("Segoe UI", 10.5f),
                Cursor = Cursors.Hand,
                Tag = text
            };

            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 80);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 100, 100);

            button.Click += (s, e) =>
            {
                if (button != btnLogout)
                {
                    navigationHandler?.Invoke(button);
                    NavigationButtonClicked?.Invoke(this, button);
                }

                switch (text)
                {
                    case "Process Reservations":
                        ReservationsClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Check-In Guests":
                        CheckInClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Check-Out Guests":
                        CheckOutClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Room Assignments":
                        RoomAssignmentsClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Billing":
                        BillingClicked?.Invoke(this, EventArgs.Empty);
                        break;
                    case "Logout":
                        LogoutClicked?.Invoke(this, EventArgs.Empty);
                        break;
                }
            };

            return button;
        }

        public void SetNavigationHandler(Action<Button> handler)
        {
            navigationHandler = handler;
        }

        public void SelectButton(Button button)
        {
            if (button == btnLogout) return;

            ResetButtonColors();
            if (button != null)
            {
                button.BackColor = Color.FromArgb(0, 128, 128);
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 140, 140);
            }
        }

        public void SelectButtonByText(string buttonText)
        {
            // Simple mapping for ease of use from dashboard
            if (buttonText == "Dashboard" || buttonText == "Reservations") SelectButton(btnReservations);
            else if (buttonText == "Check-In") SelectButton(btnCheckIn);
            else if (buttonText == "Check-Out") SelectButton(btnCheckOut);
            else if (buttonText == "Rooms") SelectButton(btnRoomAssignments);
            else if (buttonText == "Billing") SelectButton(btnBilling);
        }

        public void ResetButtonColors()
        {
             var buttons = new[] { btnReservations, btnCheckIn, btnCheckOut, btnRoomAssignments, btnBilling };
             foreach(var btn in buttons)
             {
                 if(btn != null)
                 {
                     btn.BackColor = Color.FromArgb(0, 64, 64);
                     btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 80, 80);
                 }
             }
        }

        [Category("Appearance")]
        [Description("Background color of navigation buttons")]
        public Color ButtonBackColor
        {
            get => btnReservations?.BackColor ?? Color.FromArgb(0, 64, 64);
            set => SetButtonProperty(button => button.BackColor = value);
        }

        [Category("Appearance")]
        [Description("Text color of navigation buttons")]
        public Color ButtonTextColor
        {
            get => btnReservations?.ForeColor ?? Color.White;
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
            get => btnReservations?.Enabled ?? true;
            set => SetButtonsEnabled(value);
        }

        private void SetButtonProperty(Action<Button> action)
        {
            var buttons = new[] { btnReservations, btnCheckIn, btnCheckOut, btnRoomAssignments, btnBilling };
            foreach (var button in buttons)
            {
                if (button != null) action(button);
            }
        }

        private void SetButtonsEnabled(bool enabled)
        {
            var buttons = new[] { btnReservations, btnCheckIn, btnCheckOut, btnRoomAssignments, btnBilling };
            foreach (var button in buttons)
            {
                if (button != null)
                {
                    button.Enabled = enabled;
                    button.BackColor = enabled ? Color.FromArgb(0, 64, 64) : Color.FromArgb(0, 40, 40);
                }
            }
        }

        [Category("Appearance")]
        public string SidebarTitle
        {
            get => lblFrontDesk?.Text ?? "Front Desk";
            set { if (lblFrontDesk != null) lblFrontDesk.Text = value; }
        }
    }
}
