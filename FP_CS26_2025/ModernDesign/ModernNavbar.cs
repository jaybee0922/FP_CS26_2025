using System;
using System.Drawing;
using System.Windows.Forms;
using FP_CS26_2025.Services;

namespace FP_CS26_2025.ModernDesign
{
    public partial class ModernNavbar : UserControl
    {
        private readonly INavigationService _navigationService;
        private string _activePage = "Home";

        private Button btnHome;
        private Button btnRooms;
        private Button btnLogin;

        public string ActivePage
        {
            get => _activePage;
            set
            {
                _activePage = value;
                UpdateActiveStyles();
            }
        }

        public ModernNavbar() : this(new NavigationService()) { }

        public ModernNavbar(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponents();
            this.BackColor = Color.Transparent;

            if (!DesignMode)
            {
                UpdateActiveStyles();
            }
        }

        private void InitializeComponents()
        {
            this.btnHome = CreateNavButton("HOME", 0);
            this.btnRooms = CreateNavButton("ROOMS", 133);
            this.btnLogin = CreateNavButton("LOGIN", 266);

            this.Controls.Add(btnHome);
            this.Controls.Add(btnRooms);
            this.Controls.Add(btnLogin);

            this.btnHome.Click += (s, e) => _navigationService.NavigateToHome(this.FindForm());
            this.btnRooms.Click += (s, e) => _navigationService.NavigateToRooms(this.FindForm());
            this.btnLogin.Click += (s, e) => _navigationService.NavigateToLogin(this.FindForm());

            this.Height = 50;
            this.Width = 450; // Increased to prevent clipping
        }

        private Button CreateNavButton(string text, int xOffset, bool isPrimary = false)
        {
            Button btn = new Button
            {
                Text = text,
                Location = new Point(xOffset, 0),
                Size = new Size(isPrimary ? 160 : 133, 43),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, isPrimary ? FontStyle.Bold : FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(20, 255, 255, 255); // Subtle white on click
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(10, 255, 255, 255); // Subtle white on hover
            
            return btn;
        }

        private void UpdateActiveStyles()
        {
            ResetButtonStyles(btnHome, "HOME");
            ResetButtonStyles(btnRooms, "ROOMS");
            ResetButtonStyles(btnLogin, "LOGIN");

            if (_activePage == "Home") SetActiveStyle(btnHome);
            else if (_activePage == "Rooms") SetActiveStyle(btnRooms);
            else if (_activePage == "Login") SetActiveStyle(btnLogin);
        }

        private void ResetButtonStyles(Button btn, string originalText)
        {
            btn.Font = new Font(btn.Font, FontStyle.Regular);
            btn.Text = originalText;
        }

        private void SetActiveStyle(Button btn)
        {
            btn.Font = new Font(btn.Font, FontStyle.Underline | FontStyle.Bold);
        }
    }
}
