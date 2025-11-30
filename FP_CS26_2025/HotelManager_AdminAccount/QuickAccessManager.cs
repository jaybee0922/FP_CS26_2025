using System;
using System.Drawing;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    public class QuickAccessManager : IDisposable
    {
        private Panel panelQuickAccess;

        public QuickAccessManager(Panel mainPanel)
        {
            CreateQuickAccessPanel(mainPanel);
        }

        private void CreateQuickAccessPanel(Panel mainPanel)
        {
            panelQuickAccess = new Panel
            {
                BackColor = Color.White,
                Location = new Point(25, 430),
                Size = new Size(600, 200),
                BorderStyle = BorderStyle.FixedSingle
            };

            AddQuickAccessTitle();
            CreateQuickAccessButtons();

            mainPanel.Controls.Add(panelQuickAccess);
        }

        private void AddQuickAccessTitle()
        {
            var lblQuickAccess = new Label
            {
                Text = "Quick Access Functions",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(15, 15),
                Size = new Size(200, 20),
                ForeColor = Color.FromArgb(51, 51, 76)
            };
            panelQuickAccess.Controls.Add(lblQuickAccess);
        }

        private void CreateQuickAccessButtons()
        {
            int buttonWidth = 160;
            int buttonHeight = 50;
            int horizontalSpacing = 20;
            int verticalSpacing = 20;

            int totalWidth = (buttonWidth * 3) + (horizontalSpacing * 2);
            int startX = (panelQuickAccess.Width - totalWidth) / 2;

            int row1Y = 50;
            int row2Y = row1Y + buttonHeight + verticalSpacing;

            // Column 1
            var btnManageRooms = CreateQuickAccessButton("Manage\nRooms", startX, row1Y, buttonWidth, buttonHeight);
            var btnManageGuests = CreateQuickAccessButton("Manage\nGuests", startX, row2Y, buttonWidth, buttonHeight);

            // Column 2
            var btnProcessPayments = CreateQuickAccessButton("Process\nPayments", startX + buttonWidth + horizontalSpacing, row1Y, buttonWidth, buttonHeight);
            var btnRoomCalendar = CreateQuickAccessButton("Room\nCalendar", startX + buttonWidth + horizontalSpacing, row2Y, buttonWidth, buttonHeight);

            // Column 3
            var btnAdjustPricing = CreateQuickAccessButton("Adjust\nPricing", startX + (buttonWidth + horizontalSpacing) * 2, row1Y, buttonWidth, buttonHeight);
            var btnManageStaff = CreateQuickAccessButton("Manage\nStaff", startX + (buttonWidth + horizontalSpacing) * 2, row2Y, buttonWidth, buttonHeight);

            panelQuickAccess.Controls.AddRange(new Control[] {
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
                Cursor = Cursors.Hand
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
            button.MouseUp += (s, e) => HandleButtonClick(button, text);

            return button;
        }

        private void HandleButtonClick(Button button, string text)
        {
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

            MessageBox.Show($"{text.Replace("\n", " ")} clicked!", "Quick Access",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void Dispose()
        {
            panelQuickAccess?.Dispose();
        }
    }
}